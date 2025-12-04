import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getItems, rentItem } from "../api.js";
import { useUser } from "../Context/UserContext.jsx";

export default function Rental() {
    const [items, setItems] = useState([]);
    const { user } = useUser();
    const navigate = useNavigate();

    useEffect(() => {
        loadItems();
    }, []);

    async function loadItems() {
        try {
            const data = await getItems();

            // Filter only available items
            const availableItems = data.filter(item => item.isAvailable === true);

            setItems(availableItems);
        } catch (error) {
            console.error("Error loading items:", error);
        }
    }

    async function handleRent(itemId) {
        if (!user) {
            alert("Du skal være logget ind for at leje!");
            navigate("/login");
            return;
        }

        try {
            await rentItem(user.id, itemId);
            alert("Du har lejet varen!");
            loadItems(); // Refresh list
        } catch (error) {
            console.error("Error renting item:", error);
            alert("Noget gik galt ved udlejningen.");
        }
    }

    return (
        <div style={{ padding: "20px" }}>
            <h2>Udlejning</h2>

            {items.length === 0 ? (
                <p>Ingen ledige varer lige nu.</p>
            ) : (
                <div style={{
                    display: "grid",
                    gridTemplateColumns: "repeat(auto-fill, minmax(250px, 1fr))",
                    gap: "15px"
                }}>
                    {items.map(item => (
                        <div key={item.id} style={{
                            border: "1px solid #ccc",
                            borderRadius: "8px",
                            padding: "15px",
                            textAlign: "center"
                        }}>
                            <h3>{item.name}</h3>
                            <p>Kategori: {item.category}</p>
                            <p>Stand: {item.condition}</p>
                            <p>Værdi: {item.value} kr</p>

                            <button
                                onClick={() => handleRent(item.id)}
                                style={{
                                    background: "#007bff",
                                    color: "white",
                                    border: "none",
                                    padding: "8px 12px",
                                    borderRadius: "4px",
                                    cursor: "pointer"
                                }}
                            >
                                Lej
                            </button>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}
