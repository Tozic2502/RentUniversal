import { useEffect, useState } from "react";
import { useCart } from "../Context/CartContext";
import { useUser } from "../Context/UserContext";
import { getItems } from "../api";
import { useNavigate } from "react-router-dom";

function Home({ selectedCategory, searchTerm }) {
    const [items, setItems] = useState([]);
    const { addToCart } = useCart();
    const { user } = useUser();
    const navigate = useNavigate();


    useEffect(() => {
        async function load() {
            try {
                const data = await getItems();

                const availableItems = data.filter(item => item.isAvailable === true);

                setItems(availableItems);
            } catch (error) {
                console.error(error);
            }
        }


        load();
    }, []);
    async function handleAddToCart(item) {
        if (!user) {
            alert("Du skal være logget ind for at tilføje i kurven!");
            navigate("/login");
            return;
        }

        addToCart(item);
       
    }

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
                                Pris pr. dag: {item.pricePerDay} kr
                            </p>

                            <p className="item-deposit">
                                Depositum: {item.deposit} kr
                            </p>


                        </div>

                        <button
                            onClick={() => handleAddToCart(item)}
                            style={{
                                background: "#007bff",
                                color: "white",
                                border: "none",
                                padding: "8px 12px",
                                borderRadius: "4px",
                                cursor: "pointer"
                            }}
                        >
                            Tilfoej til kurv
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Home;