import { useState } from "react";
import { useCart } from "../Context/CartContext.jsx";
import { useUser } from "../Context/UserContext.jsx";
import { useNavigate } from "react-router-dom";
import { rentItem } from "../api.js";

export default function Cart() {
    const { cartItems, clearCart } = useCart();
    const { user } = useUser();
    const navigate = useNavigate();

    const validCartItems = Array.isArray(cartItems) ? cartItems : [];
    const totalValue = validCartItems.reduce((sum, item) => sum + (item.value || 0), 0);

    const [showContract, setShowContract] = useState(false);

    function openContract() {
        if (!user) {
            alert("Du skal være logget ind for at leje dine varer!");
            navigate("/login");
            return;
        }
        setShowContract(true);
    }

    async function confirmContract() {
        try {
            for (const item of cartItems) {
                await rentItem(user.id, item.id);
            }

            clearCart();
            setShowContract(false);
            navigate("/udlejning");
            alert("Udlejning gennemført!");
        } catch (error) {
            console.error("Checkout failed:", error);
            alert("Fejl! Prøv igen senere.");
        }
    }

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
                            <p>Værdi: {item.value} kr</p>
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
                <p><strong>Total værdi:</strong> {totalValue} kr</p>

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
                    Bekræft Udlejning
                </button>
            </div>

            {showContract && (
                <div style={modalOverlayStyle}>
                    <div style={modalStyle}>
                        <h3>📄 Lejekontrakt</h3>

                        <p>Du accepterer hermed følgende vilkår:</p>
                        <ul>
                            <li>Udstyret skal returneres i samme stand</li>
                            <li>Du hæfter for eventuelle skader</li>
                            <li>Forsinket aflevering kan medføre ekstra omkostninger</li>
                        </ul>

                        <div style={{ marginTop: "15px", textAlign: "right" }}>
                            <button onClick={() => setShowContract(false)} style={cancelButtonStyle}>Annuller</button>
                            <button onClick={confirmContract} style={confirmButtonStyle}>Accepter & Lej</button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}

// ───────── Modal Style ─────────

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

const modalStyle = {
    background: "white",
    padding: "20px 30px",
    borderRadius: "8px",
    minWidth: "350px",
    animation: "popup 0.25s ease-out",
};

const cancelButtonStyle = {
    background: "gray",
    border: "none",
    padding: "8px 15px",
    marginRight: "10px",
    borderRadius: "6px",
    cursor: "pointer",
    color: "white"
};

const confirmButtonStyle = {
    background: "#28a745",
    border: "none",
    padding: "8px 15px",
    borderRadius: "6px",
    cursor: "pointer",
    color: "white"
};
