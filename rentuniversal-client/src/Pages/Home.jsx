// src/Pages/Home.jsx
import { useEffect, useState } from "react";
import { useCart } from "../Context/CartContext";
import { getItems } from "../api";

function Home({ selectedCategory, searchTerm }) {
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

    // Først filtrerer vi på kategori, derefter på søgetekst
    let visibleItems = items;

    if (selectedCategory) {
        visibleItems = visibleItems.filter(
            (item) => item.category === selectedCategory
        );
    }

    if (searchTerm && searchTerm.trim() !== "") {
        const q = searchTerm.toLowerCase();
        visibleItems = visibleItems.filter((item) =>
            item.name?.toLowerCase().includes(q)
        );
    }

    // ... resten af din komponent (render-delen) kan være som du allerede har
    return (
        <div style={{ padding: "20px" }}>
            <h1>Tilgængelige produkter</h1>

            {items.length === 0 && <p>Indlæser...</p>}

            {items.length > 0 && visibleItems.length === 0 && (
                <p>Ingen produkter matcher din søgning.</p>
            )}

            <div className="item-grid">
                {visibleItems.map((item) => (
                    <div key={item.id} className="item-card">
                        <div className="item-thumb">
                            {/* her har du allerede billedlogik osv. */}
                            {/* ... */}
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
                                Pris: {item.value} kr.
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
