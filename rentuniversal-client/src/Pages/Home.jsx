
import { useEffect, useState } from "react";
import { useCart } from "../Context/CartContext";
import { getItems } from "../api";

const API_BASE_URL = "http://localhost:8080"; 

function buildImageUrl(path) {
    if (!path) return "";
    if (path.startsWith("http")) return path;
    return `${API_BASE_URL}${path.startsWith("/") ? "" : "/"}${path}`;
}

function Home({ selectedCategory }) {
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

    const visibleItems = selectedCategory
        ? items.filter((item) => item.category === selectedCategory)
        : items;

    return (
        <div style={{ padding: "20px" }}>
            <h1>Tilgaengelige produkter</h1>

            {items.length === 0 && <p>Indlæser...</p>}
            {items.length > 0 && visibleItems.length === 0 && (
                <p>Ingen produkter i den valgte kategori.</p>
            )}

            <div className="item-grid">
                {visibleItems.map((item) => {
                    const hasImages = item.imageUrls && item.imageUrls.length > 0;
                    const imgSrc = hasImages ? buildImageUrl(item.imageUrls[0]) : null;

                    return (
                        <div key={item.id} className="item-card">
                            <div className="item-thumb">
                                
                                    <img
                                        src={imgSrc}
                                        
                                        className="item-thumb-img"
                                    />
                                
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
                                    Pris pr. dag: {item.pricePerDay ?? 0} kr
                                </p>
                                <p className="item-meta">
                                    Depositum: {item.deposit ?? 0} kr
                                </p>
                            </div>

                            <button
                                className="item-button"
                                onClick={() => addToCart(item)}
                            >
                                Tilføj til kurv
                            </button>
                        </div>
                    );
                })}
            </div>
        </div>
    );
}

export default Home;
