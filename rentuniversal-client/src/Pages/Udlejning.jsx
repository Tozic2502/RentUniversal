import React, { useEffect, useState } from "react";
import { useUser } from "../Context/UserContext";
import { getUserRentals, returnRental } from "../api";
import { jsPDF } from "jspdf";

export default function Udlejning() {
    const { user } = useUser();
    const [rentals, setRentals] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!user) return;
        loadRentals();
    }, [user]);

    async function loadRentals() {
        try {
            const data = await getUserRentals(user.id);
            const active = data.filter(r => r.endDate === null);
            setRentals(active);
        } catch (error) {
            console.error("Failed to load rentals", error);
        } finally {
            setLoading(false);
        }
    }

    function generatePDFReceipt(rentItem) {
        const doc = new jsPDF();

        doc.setFontSize(18);
        doc.text("Kvittering for aflevering", 15, 20);

        doc.setFontSize(12);
        doc.text(`Bruger: ${user.name} (${user.email})`, 15, 35);
        doc.text(`Produkt: ${rentItem.item?.name}`, 15, 45);
        doc.text(`Pris: ${rentItem.item?.value} kr.`, 15, 55);
        doc.text(`Udlejet d.: ${new Date(rentItem.startDate).toLocaleDateString()}`, 15, 65);
        doc.text(`Afleveret d.: ${new Date().toLocaleDateString()}`, 15, 75);

        doc.text("Tak for at benytte vores service!", 15, 95);

        doc.save(`Kvittering_${rentItem.id}.pdf`);
    }

    async function handleReturn(rentalId) {
        const rentItem = rentals.find(r => r.id === rentalId);
        try {
            await returnRental(rentalId);

            generatePDFReceipt(rentItem);

            alert("Varen er afleveret!");
            loadRentals();
        } catch (err) {
            alert("Fejl ved aflevering af varen.");
            console.error(err);
        }
    }

    if (!user) return <p>Du skal være logget ind for at se dine udlejninger.</p>;
    if (loading) return <p>Henter dine udlejninger...</p>;

    if (rentals.length === 0) {
        return (
            <div className="rental-empty-container">
                <h2>Ingen aktive udlejninger</h2>
                <p>Du har ikke udlejet noget lige nu.</p>
            </div>
        );
    }

    return (
        <div className="rental-container">
            <h1>Mine aktive udlejninger</h1>

            <div className="rental-grid">
                {rentals.map(r => (
                    <div key={r.id} className="item-card">
                        <h3>{r.item.name}</h3>

                        <p>Kategori: {r.item.category}</p>
                        <p>Stand: {r.item.condition}</p>
                        <p>Værdi: {r.item.value} kr</p>

                        <p>Pris pr. dag: {r.item.pricePerDay} kr</p>
                        <p>Depositum: {r.item.deposit} kr</p>

                        {r.endDate ? (
                            <p>Returneret ✔</p>
                        ) : (
                            <button onClick={() => handleReturn(r.id)}>
                                Aflever vare
                            </button>
                        )}
                    </div>
                ))}

            </div>
        </div>
    );
}