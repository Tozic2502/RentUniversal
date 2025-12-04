import { useEffect } from "react";
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


    async function handleCheckout() {
        if (!user) {
            alert("Du skal være logget ind for at leje dine varer!");
            navigate("/login");
            return;
        }

        try {
            for (const item of cartItems) {
                await rentItem(user.id, item.id);
            }

            alert("Udlejning gennemført!");
            clearCart();
            navigate("/udlejning");

        } catch (error) {
            console.error("Checkout failed:", error);
            alert("Fejl! Prøv igen senere.");
        }
    }

    if (validCartItems.length === 0)
    {
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
                    <li key={item.id}
                        style={{
                            marginBottom: "10px",
                            padding: "10px",
                            border: "1px solid #ccc",
                            borderRadius: "6px"
                        }}
                    >
                        <strong>{item.name}</strong>
                        <p>Værdi: {item.value} kr</p>
                    </li>
                ))}
            </ul>

            <div style={{ marginTop: "20px", textAlign: "right" }}>
                <p><strong>Total værdi:</strong> {totalValue} kr</p>

                <button
                    onClick={handleCheckout}
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
        </div>
    );
}
