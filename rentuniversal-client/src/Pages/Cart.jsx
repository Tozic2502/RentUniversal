import { useContext } from "react";
import { CartContext } from "../context/CartContext";

export default function Cart() {
    const { cart, removeFromCart } = useContext(CartContext);

    return (
        <div>
            <h1>Your Cart</h1>

            {cart.length === 0 ? (
                <p>No items in cart</p>
            ) : (
                cart.map((item, index) => (
                    <div key={index} style={{ padding: "10px 0" }}>
                        <strong>{item.name}</strong> – {item.price} DKK/day
                        <button style={{ marginLeft: "10px" }} onClick={() => removeFromCart(index)}>
                            Remove
                        </button>
                    </div>
                ))
            )}
        </div>
    );
}
