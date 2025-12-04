import React, { useEffect, useState } from "react";
import { useUser } from "../Context/UserContext";
import { getUserRentals } from "../api";


export default function Udlejning() {
    const { user } = useUser();
    const [rentals, setRentals] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!user) return;

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

        loadRentals();
    }, [user]);

    if (!user) return <p>Du skal være logget ind for at se dine udlejninger.</p>;
    if (loading) return <p>Henter dine udlejninger...</p>;

    if (rentals.length === 0) {
        return (
            <div>
                <h2>Ingen aktive udlejninger</h2>
                <p>Du har ikke udlejet noget lige nu.</p>
            </div>
        );
    }

    return (
        <div style={{ padding: "20px" }}>
            <h1>Mine Udlejninger</h1>
            <div style={{
                display: "grid",
                gap: "20px",
                gridTemplateColumns: "repeat(auto-fill, minmax(250px, 1fr))"
            }}>
                {rentals.map((r) => (
                    <div key={r.id} style={{
                        background: "#222",
                        color: "white",
                        padding: "20px",
                        borderRadius: "10px"
                    }}>
                        <h3>{r.item?.name || "Ukendt vare"}</h3>
                        <p>Pris: {r.item?.value} kr.</p>
                        <p>Udlejet: {new Date(r.startDate).toLocaleDateString()}</p>
                        <button disabled style={{
                            marginTop: "10px",
                            background: "gray",
                            border: "none",
                            padding: "8px 12px",
                            borderRadius: "5px"
                        }}>
                            Aflever (snart tilgængelig)
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}
