import React from "react";

// SideKategori component renders a sidebar for category selection and search functionality.
// Props:
// - selectedCategory: The currently selected category.
// - onSelectCategory: Callback function to handle category selection.
// - searchTerm: The current search term entered by the user.
// - onSearchChange: Callback function to handle changes in the search input.
function SideKategori({ selectedCategory, onSelectCategory, searchTerm, onSearchChange }) {
    
    return (
        <aside className="sidebar">
            {/* Sidebar header */}
            <h3>Kategori</h3>

            {/* Search input field */}
            <input
                type="text"
                className="sidebar-search"
                placeholder="S&oslash;g efter vare..." // Placeholder text in Danish: "Search for item..."
                value={searchTerm}
                onChange={(e) => onSearchChange(e.target.value)} // Trigger onSearchChange when input changes
            />

            {/* Category list */}
            <ul>
                {/* "Alle" (All) category option */}
                <li
                    className={selectedCategory === null ? "active" : ""} // Highlight if no category is selected
                    onClick={() => onSelectCategory(null)} // Select "All" category
                >
                    <button className="sidebar-btn">Alle</button>
                </li>

                {/* "Værktøj" (Tools) category option */}
                <li
                    className={selectedCategory === "Værktøj" ? "active" : ""} // Highlight if "Værkt&oslash;j" is selected
                    onClick={() => onSelectCategory("Værktøj")} // Select "Værkt&oslash;j" category
                >
                    <button className="sidebar-btn">Værktøj</button>
                </li>

                {/* "Mekanik" (Mechanics) category option */}
                <li
                    className={selectedCategory === "Mekanik" ? "active" : ""} // Highlight if "Mekanik" is selected
                    onClick={() => onSelectCategory("Mekanik")} // Select "Mekanik" category
                >
                    <button className="sidebar-btn">Mekanik</button>
                </li>
            </ul>
        </aside>
    );
}

export default SideKategori;