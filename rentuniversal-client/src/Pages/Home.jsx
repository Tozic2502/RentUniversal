// src/Pages/Home.jsx
import { useEffect, useState } from "react";
import { useCart } from "../Context/CartContext";
import { getItems } from "../api";

function Home({ selectedCategory }) {
    const [items, setItems] = useState([]);
    const { addToCart } = useCart();

    // Kontaktboks state
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [message, setMessage] = useState("");
    const [status, setStatus] = useState("");

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

    // Filtrer efter valgt kategori 
    const visibleItems =
        selectedCategory
            ? items.filter((item) => item.category === selectedCategory)
            : items;

    async function handleSendMessage(e) { 
        e.preventDefault();
        setStatus("");

        try {
            const response = await fetch("http://localhost:8080/api/Contact", { 
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    name,
                    email,
                    message
                })
            });

            if (!response.ok) {
                setStatus("Der skete en fejl. Prøv igen.");
                return;
            }

            setStatus("Tak for din besked");
            setMessage("");
        } catch (err) {
            console.error("Failed to send message", err);
            setStatus("Der skete en fejl. Prøv igen.");
        }
    }

    return (
        <div className="home-layout">
            {/* Venstre side: produkter */}
            <div className="home-left" style={{ padding: "20px" }}>
                <h1>Tilgængelige produkter</h1>

                {items.length === 0 && <p>Indlæser...</p>}

                {items.length > 0 && visibleItems.length === 0 && (
                    <p>Ingen produkter i den valgte kategori.</p>
                )}

                <div className="item-grid">
                    {visibleItems.map((item) => (
                        <div key={item.id} className="item-card">
                            {/* Simpelt ?billede? med første bogstav i navnet */}
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

            {/* Højre side: kontakt-boks */}
            <aside className="contact-box">
                <h2>Kontakt os</h2>
                <p className="contact-text">
                    Har du spørgsmål eller efterspørgsel til udlejning?
                    Skriv en besked her:
                </p>

                <form onSubmit={handleSendMessage} className="contact-form">
                    <label>
                        Navn
                        <input
                            type="text"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </label>

                    <label>
                        Email
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </label>

                    <label>
                        Besked
                        <textarea
                            rows={4}
                            value={message}
                            onChange={(e) => setMessage(e.target.value)}
                            required
                        />
                    </label>

                    <button type="submit" className="primary-btn contact-btn">
                        Send besked
                    </button>

                    {status && (
                        <p className="contact-status">
                            {status}
                        </p>
                    )}
                </form>
            </aside>
        </div>
    );
}

export default Home;
