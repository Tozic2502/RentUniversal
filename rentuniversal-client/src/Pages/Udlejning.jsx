import React, { useEffect, useState } from "react";
import { useUser } from "../Context/UserContext";
import { getUserRentals, returnRental } from "../api";


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

    async function handleReturn(rentalId) {
        try {
            await returnRental(rentalId);
            alert("Varen er afleveret!");
            loadRentals(); // Refresh list after return
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
                {rentals.map((rent) => (
                    <div key={rent.id} className="rental-card">
                        <h3>{rent.item?.name || "Ukendt vare"}</h3>

                        <p><strong>Pris:</strong> {rent.item?.value} kr.</p>
                        <p><strong>Udlejet d.:</strong> {new Date(rent.startDate).toLocaleDateString()}</p>

                        <button
                            className="return-btn"
                            onClick={() => handleReturn(rent.id)}
                        >
                            Aflever
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}
