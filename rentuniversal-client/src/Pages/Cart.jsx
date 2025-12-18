import { useState } from "react";
import { useCart } from "../Context/CartContext.jsx";
import { useUser } from "../Context/UserContext.jsx";
import { useNavigate } from "react-router-dom";
import { rentItem } from "../api.js";

export default function Cart() {
    // Access cart items and clearCart function from CartContext
    const { cartItems, clearCart } = useCart();
    // Access user information from UserContext
    const { user } = useUser();
    // Hook for navigation between routes
    const navigate = useNavigate();

    // Ensure cartItems is always an array
    const validCartItems = Array.isArray(cartItems) ? cartItems : [];
   
    // State to control the visibility of the rental contract modal
    const [showContract, setShowContract] = useState(false);

    // Calculate today's and tomorrow's dates in ISO format
    const today = new Date().toISOString().split("T")[0];
    const tomorrow = new Date(Date.now() + 86400000).toISOString().split("T")[0];

    // State for start and end dates of the rental period
    const [startDate, setStartDate] = useState(today);
    const [endDate, setEndDate] = useState(tomorrow);

    // Validate that the end date is after the start date
    const isValid = new Date(endDate) > new Date(startDate);

    // Calculate the estimated number of rental days
    const estimatedDays =
        Math.ceil((new Date(endDate) - new Date(startDate)) / (1000 * 60 * 60 * 24));

    // Calculate the estimated total cost based on the first item's price per day
    const estimatedTotal = estimatedDays * (cartItems[0]?.pricePerDay || 0);

    // Function to open the rental contract modal
    function openContract() {
        if (!user) {
            // Alert the user to log in if not authenticated
            alert("Du skal være logget ind for at leje dine varer!");
            navigate("/login");
            return;
        }
        setShowContract(true);
    }

    // Function to confirm the rental contract and process the rental
    async function confirmContract() {
        try {
            // Iterate over cart items and call the rentItem API for each
            for (const item of cartItems) {
                await rentItem(user.id, item.id, startDate, endDate);
;
            }

            // Clear the cart, close the modal, and navigate to the rental page
            clearCart();
            setShowContract(false);
            navigate("/udlejning");
            alert("Udlejning gennemført!");
        } catch (error) {
            // Handle errors during the rental process
            console.error("Checkout failed:", error);
            alert("Fejl! Prøv igen senere.");
        }
    }

    // Render a message if the cart is empty
    if (validCartItems.length === 0) {
        return (
            <div style={{ padding: "20px", textAlign: "center" }}>
                <h2>Din kurv er tom 🛒</h2>
                <button
                    style={{
                        marginTop: "10px",
                        padding: "10px 15px",
                        background: "#007bff",
                        color: "white",
                        border: "none",
                        borderRadius: "6px",
                        cursor: "pointer"
                    }}
                    onClick={() => navigate("/")}
                >
                    Gå til Home
                </button>
            </div>
        );
    }

    // Render the cart items and rental contract modal
    return (
        <div style={{ padding: "20px" }}>
            <h2>Din kurv 🛒</h2>

            <ul style={{ listStyle: "none", padding: 0 }}>
                {cartItems.map((item) => (
                    <li
                        key={item.id}
                        style={{
                            marginBottom: "10px",
                            padding: "10px",
                            border: "1px solid #ccc",
                            borderRadius: "6px",
                            display: "flex",
                            justifyContent: "space-between",
                            alignItems: "center"
                        }}
                    >
                        <div>
                            <strong>{item.name}</strong>
                            <p>Depositum: {item.deposit} kr</p>
                            <p>Pris pr. dag: {item.pricePerDay} kr</p>
                        </div>

                        <button
                            onClick={() => clearCart(item.id)}
                            style={{
                                background: "crimson",
                                color: "white",
                                border: "none",
                                padding: "6px 10px",
                                borderRadius: "4px",
                                cursor: "pointer"
                            }}
                        >
                            Fjern
                        </button>
                    </li>
                ))}
            </ul>

            <div style={{ marginTop: "20px", textAlign: "right" }}>
                <p><strong>Total Beloeb:</strong> {estimatedTotal} kr</p>

                <button
                    onClick={openContract}
                    style={{
                        background: "#28a745",
                        color: "white",
                        border: "none",
                        padding: "10px 20px",
                        borderRadius: "6px",
                        cursor: "pointer"
                    }}
                >
                    Bekraeft Udlejning
                </button>
            </div>

            {showContract && (
                <div style={modalOverlayStyle}>
                    <div style={modalStyle}>
                        <h3>📄 Lejekontrakt</h3>

                        <label>Startdato:</label>
                        <input
                            type="date"
                            value={startDate}
                            min={today}
                            onChange={(e) => setStartDate(e.target.value)}
                        />

                        <label>Slutdato:</label>
                        <input
                            type="date"
                            value={endDate}
                            min={startDate}
                            onChange={(e) => setEndDate(e.target.value)}
                        />

                        <p style={{ marginTop: "10px" }}>
                            Pris pr. dag: {cartItems[0]?.pricePerDay} kr
                            <br />
                            Estimeret total: {estimatedTotal} kr
                        </p>

                        <p>Du accepterer hermed følgende vilkår:</p>
                        <ul>
                            <li>Udstyret skal returneres i samme stand</li>
                            <li>Du hæfter for eventuelle skader</li>
                            <li>Forsinket aflevering kan medføre ekstra omkostninger</li>
                        </ul>

                        <div style={{ marginTop: "15px", textAlign: "right" }}>
                            <button onClick={() => setShowContract(false)} style={cancelButtonStyle}>Annuller</button>
                            <button onClick={confirmContract} style={confirmButtonStyle} disabled={!isValid}>
                                Accepter & Lej
                            </button>
                        </div>
                    </div>

                </div>
            )}
        </div>
    );
}

// ───────── Modal Style ─────────

// Styles for the modal overlay
const modalOverlayStyle = {
    position: "fixed",
    top: 0,
    left: 0,
    width: "100vw",
    height: "100vh",
    background: "rgba(0,0,0,0.6)",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    animation: "fadeIn 0.3s",
    zIndex: 999,
};

// Styles for the modal content
const modalStyle = {
    background: "white",
    padding: "20px 30px",
    borderRadius: "8px",
    minWidth: "350px",
    animation: "popup 0.25s ease-out",
};

// Styles for the cancel button in the modal
const cancelButtonStyle = {
    background: "gray",
    border: "none",
    padding: "8px 15px",
    marginRight: "10px",
    borderRadius: "6px",
    cursor: "pointer",
    color: "white"
};

// Styles for the confirm button in the modal
const confirmButtonStyle = {
    background: "#28a745",
    border: "none",
    padding: "8px 15px",
    borderRadius: "6px",
    cursor: "pointer",
    color: "white"
};