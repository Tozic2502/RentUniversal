import { useEffect, useState } from "react";
import { useCart } from "../Context/CartContext";

function Home() {
    const [items, setItems] = useState([]);
    const { addToCart } = useCart();

    useEffect(() => {
        async function load() {
            try {
                const res = await fetch("http://localhost:5282/api/items");
                const data = await res.json();
                setItems(data);
            } catch (error) {
                console.error("Failed to load items", error);
            }
        }
        load();
    }, []);

    return (
        <div>
            <h1>Available Items</h1>

            {items.length === 0 && <p>Loading...</p>}

            <div style={{ display: "flex", gap: "20px", flexWrap: "wrap" }}>
                {items.map(item => (
                    <div key={item.id} style={{ border: "1px solid #ccc", padding: 10 }}>
                        <h3>{item.name}</h3>
                        <p>{item.description}</p>
                        <p><b>Price: {item.price} kr.</b></p>
                        <button onClick={() => addToCart(item)}>Add to Cart</button>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Home;
