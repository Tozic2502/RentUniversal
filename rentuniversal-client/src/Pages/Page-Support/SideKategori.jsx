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
                placeholder="Søg efter vare..." // Placeholder text in Danish: "Search for item..."
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

                {/* "V&aelig;rktøj" (Tools) category option */}
                <li
                    className={selectedCategory === "V&aelig;rktøj" ? "active" : ""} // Highlight if "Værktøj" is selected
                    onClick={() => onSelectCategory("V&aelig;rktøj")} // Select "Værktøj" category
                >
                    <button className="sidebar-btn">V&aelig;rktøj</button>
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