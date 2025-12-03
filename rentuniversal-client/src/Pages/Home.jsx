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
        <div style={{ padding: "20px" }}>
            <h1>Tilgængelige produkter</h1>

            {items.length === 0 && <p>Indlæser...</p>}

            <div className="item-grid">
                {items.map((item) => (
                    <div key={item.id} className="item-card">
                        {/* “Billede” – simpelt placeholder med første bogstav i navnet */}
                        <div className="item-thumb">
                            <span>{item.name?.charAt(0) ?? "?"}</span>
                        </div>

                        <div className="item-info">
                            <h3>{item.name}</h3>

                            <p className="item-meta">
                                <strong>Kategori:</strong> {item.category}
                            </p>
                            <p className="item-meta">
                                <strong>Stand:</strong> {item.condition}
                            </p>

                            <p className="item-price">
                                Værdi: {item.value} kr.
                            </p>
                        </div>

                        <button
                            className="item-button"
                            onClick={() => addToCart(item)}
                        >
                            Læg i kurv
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Home;
