import { useEffect, useState } from "react";
import { useCart } from "../Context/CartContext";
import { useUser } from "../Context/UserContext";
import { getItems } from "../api";
import { useNavigate } from "react-router-dom";

function Home() {
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
                            Tilføj til kurv
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Home;
