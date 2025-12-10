import { useEffect, useState } from "react";
import { getUserRentals } from "../api";
import { useUser } from "../Context/UserContext.jsx"; 

function Rental() {
    const [rentals, setRentals] = useState([]);

    const { user } = useUser();

    useEffect(() => {
        if (!user) return; // Prevent fetching if not logged in

        async function loadItems() {
            try {
                const data = await getAllItems();

                const availableItems = data.filter(item => item.isAvailable === true);

                setItems(availableItems);
            } catch (error) {
                console.error(error);
            }
        }


        loadRentals();
    }, [user]);
    async function rentItem(itemId) {
        if (!user) {
            alert("Du skal være logget ind for at leje!");
            navigate("/login");
            return;
        }

        const response = await fetch("http://localhost:8080/api/rentals", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                userId: user.id,
                itemId: itemId
            })
        });

        if (!response.ok) {
            alert("Der opstod en fejl ved udlejning.");
            return;
        }

        alert("Tillykke! Du har lejet varen!");
        loadItems(); // Refresh list to hide the rented item
    }


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
                        <button onClick={() => rentItem(item.id)}>
                            Rent
                        </button>

                    </div>
                ))}
            </div>
        </div>
    );
}

export default Rental;