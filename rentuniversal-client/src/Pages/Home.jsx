import { useEffect, useState } from "react";
import { useCart } from "../Context/CartContext";
import { getItems } from "../api"; 

function Home() {
    const [items, setItems] = useState([]);
    const { addToCart } = useCart();

    useEffect(() => {
        async function load() {
            try {
                const data = await getItems(); 
                setItems(data);
            } catch (err) {
                console.error("Failed to load items", err);
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
