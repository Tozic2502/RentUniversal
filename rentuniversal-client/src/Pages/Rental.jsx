import { useEffect, useState } from "react";
import { getUserRentals } from "../api";

function Rental() {
    const [rentals, setRentals] = useState([]);

    useEffect(() => {
        async function loadRentals() {
            try {
                const data = await getUserRentals("1"); // TODO: Skift til login user
                setRentals(data);
            } catch (err) {
                console.error("Failed to load rentals", err);
            }
        }
        loadRentals();
    }, []);

    return (
        <div style={{ padding: "20px" }}>
            <h1>Dine udlejninger</h1>

            {rentals.length === 0 && <p>Ingen aktive udlejninger.</p>}

            <div>
                {rentals.map(r => (
                    <div
                        key={r.id}
                        style={{
                            border: "1px solid #ddd",
                            padding: "15px",
                            borderRadius: "8px",
                            marginBottom: "15px",
                            background: "white"
                        }}
                    >
                        <h3>Item ID: {r.itemId}</h3>
                        <p><strong>Start:</strong> {new Date(r.startDate).toLocaleString()}</p>
                        <p><strong>Slut:</strong> {r.endDate ? new Date(r.endDate).toLocaleString() : "I gang"}</p>

                        <p><strong>Start stand:</strong> {r.startCondition}</p>
                        <p><strong>Slut stand:</strong> {r.returnCondition ?? "Ikke afleveret"}</p>

                        <p><strong>Pris:</strong> {r.price} kr.</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Rental;
