import { useEffect, useState } from "react";
import { useCart } from "../Context/CartContext";
import { getItems } from "../api";

const API_BASE_URL = "http://localhost:8080"; 

// Helper function to build a full image URL from a given path
function buildImageUrl(path) {
    if (!path) return ""; // Return an empty string if the path is not provided
    if (path.startsWith("http")) return path; // Return the path if it's already a full URL
    return `${API_BASE_URL}${path.startsWith("/") ? "" : "/"}${path}`; // Construct the full URL
}

// Home component displays a list of items and allows adding them to the cart
function Home({ selectedCategory }) {
    const [items, setItems] = useState([]); // State to store the list of items
    const { addToCart } = useCart(); // Access the addToCart function from the CartContext

    // useEffect to load items when the component mounts
    useEffect(() => {
        async function load() {
            try {
                const data = await getItems(); // Fetch items from the API
                setItems(data); // Update the state with the fetched items
            } catch (err) {
                console.error("Failed to load items", err); // Log an error if fetching fails
            }
        }

        load(); // Call the load function
    }, []); // Empty dependency array ensures this runs only once

    // Filter items based on the selected category, if provided
    const visibleItems = selectedCategory
        ? items.filter((item) => item.category === selectedCategory)
        : items;

    return (
        <div style={{ padding: "20px" }}>
            <h1>Tilg&aelig;ngelige produkter</h1>

            {/* Display a loading message if no items are available */}
            {items.length === 0 && <p>Indl&aelig;ser...</p>}
            {/* Display a message if no items match the selected category */}
            {items.length > 0 && visibleItems.length === 0 && (
                <p>Ingen produkter i den valgte kategori.</p>
            )}

            <div className="item-grid">
                {/* Render a card for each visible item */}
                {visibleItems.map((item) => {
                    const hasImages = item.imageUrls && item.imageUrls.length > 0; // Check if the item has images
                    const imgSrc = hasImages ? buildImageUrl(item.imageUrls[0]) : null; // Get the first image URL

                    return (
                        <div key={item.id} className="item-card">
                            <div className="item-thumb">
                                {/* Display the item's thumbnail image */}
                                <img
                                    src={imgSrc}
                                    className="item-thumb-img"
                                />
                            </div>

                            <div className="item-info">
                                {/* Display item details */}
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

                            {/* Button to add the item to the cart */}
                            <button
                                className="item-button"
                                onClick={() => addToCart(item)}
                            >
                                Tilf&oslash;j til kurv
                            </button>
                        </div>
                    );
                })}
            </div>
        </div>
    );
}

export default Home;
