// src/Pages/SideKategori.jsx
import { useEffect, useState } from "react";
import { getItems } from "../api";

function SideKategori({ selectedCategory, onSelectCategory }) {
    const [categories, setCategories] = useState([]);
    const [error, setError] = useState("");

    useEffect(() => {
        async function load() {
            try {
                const items = await getItems();

                const uniqueCategories = Array.from(
                    new Set(
                        items
                            .map((i) => i.category)
                            .filter(Boolean)
                    )
                );

                setCategories(uniqueCategories);
            } catch (err) {
                console.error("Failed to load categories", err);
                setError("Kunne ikke indlæse kategorier.");
            }
        }

        load();
    }, []);

    return (
        <aside className="sidebar">
            <h3>Kategori</h3>

            {error && (
                <p style={{ color: "red", fontSize: "12px" }}>
                    {error}
                </p>
            )}

            <ul>
                {/* “Alle” viser alle produkter */}
                <li
                    className={!selectedCategory ? "active" : ""}
                    onClick={() => onSelectCategory(null)}
                >
                    <button type="button" className="sidebar-btn">
                        Alle
                    </button>
                </li>

                {categories.length === 0 && !error && <li>Indlæser...</li>}

                {categories.map((cat) => (
                    <li
                        key={cat}
                        className={cat === selectedCategory ? "active" : ""}
                        onClick={() => onSelectCategory(cat)}
                    >
                        <button type="button" className="sidebar-btn">
                            {cat}
                        </button>
                    </li>
                ))}
            </ul>
        </aside>
    );
}

export default SideKategori;