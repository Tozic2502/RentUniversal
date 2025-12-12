import React, { useEffect, useState } from "react";
import { useUser } from "../Context/UserContext.jsx";
import { getUserRentals, returnRental } from "../api.js";
import { jsPDF } from "jspdf";

export default function Udlejning() {
    const { user } = useUser();
    const [rentals, setRentals] = useState([]);
    const [loading, setLoading] = useState(true);
    const [returningIds, setReturningIds] = useState(new Set());
    const [error, setError] = useState(null);

    useEffect(() => {
        if (!user) {
            setRentals([]);
            setLoading(false);
            return;
        }
        loadRentals();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [user]);

    async function loadRentals() {
        setLoading(true);
        setError(null);

        try {
            const data = await getUserRentals(user.id);

            const active = data.filter(r => {
                if (!r.endDate || r.endDate === "" || r.endDate === null) return true;

                const endDate = new Date(r.endDate);
                const now = new Date();

                return endDate > now;  // treat future endDate as active
            });


            setRentals(active);
        } catch (err) {
            console.error("Failed to load rentals", err);
            setError("Kunne ikke hente dine udlejninger.");
        } finally {
            setLoading(false);
        }
    }


    function generatePDFReceipt(rentItem) {
        const doc = new jsPDF();

        doc.setFontSize(18);
        doc.text("Kvittering for aflevering", 15, 20);

        doc.setFontSize(12);
        doc.text(`Bruger: ${user?.name ?? "Ukendt"} (${user?.email ?? "-"})`, 15, 35);

        const itemName = rentItem?.item?.name ?? "Ukendt vare";
        const value = rentItem?.item?.value ?? rentItem?.price ?? 0;
        doc.text(`Produkt: ${itemName}`, 15, 45);
        doc.text(`Pris pr. dag: ${rentItem?.item?.pricePerDay ?? rentItem?.pricePerDay ?? 0} kr.`, 15, 55);
        doc.text(`Depositum: ${rentItem?.item?.deposit ?? 0} kr.`, 15, 65);

        doc.text(`Udlejet d.: ${rentItem?.startDate ? new Date(rentItem.startDate).toLocaleDateString() : "-"}`, 15, 80);
        doc.text(`Afleveret d.: ${new Date().toLocaleDateString()}`, 15, 90);

        // If TotalPrice exists show it
        if (rentItem?.totalPrice || rentItem?.totalPrice === 0) {
            doc.text(`Total pris: ${rentItem.totalPrice} kr.`, 15, 100);
        }

        doc.text("Tak for at benytte vores service!", 15, 120);

        try {
            doc.save(`Kvittering_${rentItem?.id ?? "udlejning"}.pdf`);
        } catch (err) {
            console.error("PDF save failed", err);
        }
    }

    async function handleReturn(rentalId) {
        // find rental snapshot (may be null-safe)
        const rentItem = rentals.find(r => r.id === rentalId);

        if (!rentItem) {
            alert("Kunne ikke finde udlejningen.");
            return;
        }

        if (!confirm("Er du sikker på, at du vil aflevere denne vare?")) return;

        try {
            // mark as returning for UI
            setReturningIds(prev => new Set(prev).add(rentalId));

            await returnRental(rentalId);

            // Generate receipt (use the snapshot we had before return)
            generatePDFReceipt(rentItem);

            alert("Varen er afleveret!");
            await loadRentals();
        } catch (err) {
            console.error("Return failed", err);
            alert("Fejl ved aflevering af varen. Prøv igen.");
        } finally {
            // remove id from returning set
            setReturningIds(prev => {
                const copy = new Set(prev);
                copy.delete(rentalId);
                return copy;
            });
        }
    }

    // Helpers
    const isReturned = (r) => {
        if (!r.endDate) return false;
        const end = new Date(r.endDate);
        const now = new Date();
        return end <= now; // returned if the endDate has passed
    };


    if (!user) {
        return <p>Du skal være logget ind for at se dine udlejninger.</p>;
    }

    if (loading) {
        return <p>Henter dine udlejninger...</p>;
    }

    if (error) {
        return (
            <div style={{ padding: 20 }}>
                <p style={{ color: "crimson" }}>{error}</p>
                <button onClick={loadRentals} style={{ padding: "8px 12px" }}>
                    Prøv igen
                </button>
            </div>
        );
    }

    if (!rentals || rentals.length === 0) {
        return (
            <div className="rental-empty-container" style={{ padding: 20 }}>
                <h2>Ingen aktive udlejninger</h2>
                <p>Du har ikke udlejet noget lige nu.</p>
            </div>
        );
    }

    return (
        <div className="rental-container" style={{ padding: 20 }}>
            <h1>Mine aktive udlejninger</h1>

            <div className="rental-grid" style={{
                display: "grid",
                gridTemplateColumns: "repeat(auto-fill, minmax(260px, 1fr))",
                gap: 16,
                marginTop: 16
            }}>
                {rentals.map(r => (
                    <div key={r.id} className="item-card" style={{
                        border: "1px solid #ddd",
                        borderRadius: 8,
                        padding: 12,
                        background: "#fff"
                    }}>
                        <h3 style={{ marginTop: 0 }}>{r.item?.name ?? "Ukendt vare"}</h3>

                        <p><strong>Kategori:</strong> {r.item?.category ?? "-"}</p>
                        <p><strong>Stand:</strong> {r.item?.condition ?? "-"}</p>
                        <p><strong>Værdi:</strong> {r.item?.value ?? r.item?.value ?? 0} kr</p>

                        <p><strong>Pris pr. dag:</strong> {r.item?.pricePerDay ?? r.pricePerDay ?? 0} kr</p>
                        <p><strong>Depositum:</strong> {r.item?.deposit ?? 0} kr</p>

                        <p><strong>Udlejet d.:</strong> {r.startDate ? new Date(r.startDate).toLocaleDateString() : "Ukendt"}</p>

                        {isReturned(r) ? (
                            <p style={{ color: "green", fontWeight: "600" }}>Returneret ✔</p>
                        ) : (
                            <div style={{ marginTop: 10 }}>
                                <button
                                    onClick={() => handleReturn(r.id)}
                                    disabled={returningIds.has(r.id)}
                                    style={{
                                        background: returningIds.has(r.id) ? "#aaa" : "#28a745",
                                        color: "white",
                                        border: "none",
                                        padding: "8px 12px",
                                        borderRadius: 6,
                                        cursor: returningIds.has(r.id) ? "not-allowed" : "pointer"
                                    }}
                                >
                                    {returningIds.has(r.id) ? "Behandler..." : "Aflever vare"}
                                </button>
                            </div>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
}
