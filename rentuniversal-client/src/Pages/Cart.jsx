import { useCart } from "../Context/CartContext";

function Cart() {
    const { cart, removeFromCart, clearCart } = useCart();


    // Total price
    const total = cart.reduce((sum, item) => sum + item.value, 0);

    async function handleCheckout() {
        try {
            // 1. For hver item i kurven → lav en rental i backend
            for (const item of cart) {
                await fetch("http://localhost:8080/api/rentals/start", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        userId: "1",       // TODO: replace with login user
                        itemId: item.id,
                        startCondition: item.condition
                    })
                });
            }

            alert("Udlejning gennemført!");

            // 2. Tøm kurven
            clearCart();

            // 3. Redirect til udlejning
            window.location.href = "/udlejning";

        } catch (err) {
            console.error("Checkout failed", err);
            alert("Der skete en fejl under udlejningen.");
        }
    }

    return (
        <div style={{ padding: "20px" }}>
            <h1>Din kurv</h1>

            {cart.length === 0 && <p>Din kurv er tom.</p>}

            <div>
                {cart.map((item) => (
                    <div
                        key={item.id}
                        style={{
                            border: "1px solid #ddd",
                            padding: "10px",
                            marginBottom: "10px"
                        }}
                    >
                        <h3>{item.name}</h3>
                        <p>Kategori: {item.category}</p>
                        <p>Stand: {item.condition}</p>
                        <p>Pris: {item.value} kr.</p>

                        <button
                            onClick={() => removeFromCart(item.id)}
                            style={{ background: "red", color: "white", padding: "5px 10px" }}
                        >
                            Fjern
                        </button>
                    </div>
                ))}
            </div>

            {cart.length > 0 && (
                <><h2>Total pris: {total} kr.</h2>
                    <button
                    style={{
                        marginTop: "20px",
                        padding: "10px 20px",
                        background: "#0066ff",
                        color: "white",
                        fontSize: "18px",
                        border: "none",
                        cursor: "pointer"
                    }}
                    onClick={handleCheckout}
                >
                    Bekræft udlejning
                </button></>
            )}
            

        </div>
    );
}

export default Cart;
