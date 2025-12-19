import { useCart } from "../Context/CartContext";

const API_BASE_URL = "http://localhost:8080";

/**
 * Builds a full image URL from a relative backend path
 * Example:
 *  "/uploads/items/123/img.jpg"
 *  ? "http://localhost:8080/uploads/items/123/img.jpg"
 */
function buildImageUrl(path) {
    if (!path) return "";
    if (path.startsWith("http")) return path;
    return `${API_BASE_URL}${path.startsWith("/") ? "" : "/"}${path}`;
}

/**
 * Home
 * Displays all available items.
 * Items are filtered by:
 * - selectedCategory (from sidebar)
 * - searchTerm (from sidebar search input)
 */
function Home({ items, selectedCategory, searchTerm }) {
    const { addToCart } = useCart();

    // Filter items by category AND search term
    const visibleItems = items.filter(item => {
        const matchesCategory =
            !selectedCategory || item.category === selectedCategory;

        const matchesSearch =
            item.name
                ?.toLowerCase()
                .includes(searchTerm.toLowerCase());

        return matchesCategory && matchesSearch;
    });

    {/*
  Renders the home page displaying available products.
  The view handles loading states, empty result states,
  and presents items in a responsive grid layout.
*/}
    return (
        <div style={{ padding: "20px" }}>
            <h1>Tilgaengelige produkter</h1>
            {items.length === 0 && <p>Indlaeser...</p>}
            {items.length > 0 && visibleItems.length === 0 && (
                <p>Ingen produkter matcher dit valg.</p>
            )}
            
            <div className="item-grid">
                {visibleItems.map(item => {
                    {/*
                  Determines whether the current item has
                  one or more associated image URLs.
                */}
                    const hasImages =
                        item.imageUrls && item.imageUrls.length > 0;

                    {/*
                  Builds the image source URL for the first image
                  if available, otherwise falls back to null.
                */}
                    const imgSrc = hasImages
                        ? buildImageUrl(item.imageUrls[0])
                        : null;

                    return (
                        <div key={item.id} className="item-card">
                            {/*
                          Thumbnail section for the product.
                          Displays an image if available, otherwise
                          falls back to the first character of the item name.
                        */}
                            <div className="item-thumb">
                                {imgSrc ? (
                                    <img
                                        src={imgSrc}
                                        alt={item.name}
                                        className="item-thumb-img"
                                    />
                                ) : (
                                    <span>
                                    {item.name?.charAt(0) ?? "?"}
                                </span>
                                )}
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
                            
                            {/*
                          Action button for adding the selected product
                          to the user's cart.
                        */}
                            <button
                                className="item-button"
                                onClick={() => addToCart(item)}
                            >
                                Tilfoej til kurv
                            </button>
                        </div>
                    );
                })}
            </div>
        </div>
    );
}

export default Home;
