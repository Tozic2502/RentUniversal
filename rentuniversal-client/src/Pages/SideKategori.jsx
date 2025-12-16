
import React from "react";

function SideKategori({selectedCategory, onSelectCategory, searchTerm, onSearchChange,}) {
    
    return (
        <aside className="sidebar">
            <h3>Kategori</h3>

            
            <input
                type="text"
                className="sidebar-search"
                placeholder="Søg efter vare..."
                value={searchTerm}
                onChange={(e) => onSearchChange(e.target.value)}
            />

            <ul>
                <li
                    className={selectedCategory === null ? "active" : ""}
                    onClick={() => onSelectCategory(null)}
                >
                    <button className="sidebar-btn">Alle</button>
                </li>
                <li
                    className={selectedCategory === "Værktøj" ? "active" : ""}
                    onClick={() => onSelectCategory("Værktøj")}
                >
                    <button className="sidebar-btn">Værktøj</button>
                </li>
                <li
                    className={selectedCategory === "Mekanik" ? "active" : ""}
                    onClick={() => onSelectCategory("Mekanik")}
                >
                    <button className="sidebar-btn">Mekanik</button>
                </li>
            </ul>
        </aside>
    );
}

export default SideKategori;