import React from "react";

/**
 * SideKategori
 * Sidebar that:
 * - Builds category list dynamically from items
 * - Shows search input
 * - Filters items by category
 */
function SideKategori({items, selectedCategory, onSelectCategory, searchTerm, onSearchChange}) {
    // Build UNIQUE category list from backend items
    const categories = [
        "Alle",
        ...Array.from(
            new Set(
                items
                    .map(item => item.category)
                    .filter(Boolean)
            )
        )
    ];

    return (
        <aside className="sidebar">
            <h3>Kategori</h3>

            <input
                type="text"
                className="sidebar-search"
                placeholder="Soeg efter vare..."
                value={searchTerm}
                onChange={(e) => onSearchChange(e.target.value)}
            />

            <ul>
                {categories.map(category => (
                    <li
                        key={category}
                        className={
                            (category === "Alle" && selectedCategory === null) ||
                            selectedCategory === category
                                ? "active"
                                : ""
                        }
                        onClick={() =>
                            onSelectCategory(
                                category === "Alle" ? null : category
                            )
                        }
                    >
                        <button className="sidebar-btn">
                            {category}
                        </button>
                    </li>
                ))}
            </ul>
        </aside>
    );
}

export default SideKategori;
