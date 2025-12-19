import "./App.css";
import { Routes, Route } from "react-router-dom";
import { useEffect, useState } from "react";
import { getItems } from "./api";

// Pages
import Home from "./Pages/Home.jsx";
import Kurv from "./Pages/Cart.jsx";
import Login from "./Pages/Login.jsx";
import ProfilePage from "./Pages/ProfilePage.jsx";
import Register from "./Pages/Register.jsx";
import SupportPage from "./Pages/Support.jsx";
import Udlejning from "./Pages/Udlejning.jsx";

// Layout
import Header from "./Pages/Page-Support/Header.jsx";
import Footer from "./Pages/Page-Support/Footer.jsx";
import SideKategori from "./Pages/Page-Support/SideKategori.jsx";

// Auth
import ProtectedRoute from "./Components/ProtectedRoute.jsx";

/**
 * App
 * Root component.
 * Holds shared state for:
 * - items
 * - selected category
 * - search term
 */
export default function App() {
    const [items, setItems] = useState([]);
    const [selectedCategory, setSelectedCategory] = useState(null);
    const [searchTerm, setSearchTerm] = useState("");

    // Load items once for entire app
    useEffect(() => {
        async function loadItems() {
            const data = await getItems();
            setItems(data);
        }
        loadItems();
    }, []);
    
    {/*
  Renders the main application layout.
  This component composes the overall page structure including
  header, sidebar navigation, routed content, and footer.
*/}

    return (
        <div className="app">
            <Header />

            <div className="layout">
                <SideKategori
                    items={items}
                    selectedCategory={selectedCategory}
                    onSelectCategory={setSelectedCategory}
                    searchTerm={searchTerm}
                    onSearchChange={setSearchTerm}
                />

                <main className="main">
                    <Routes>
                        <Route
                            path="/"
                            element={
                                <Home
                                    items={items}
                                    selectedCategory={selectedCategory}
                                    searchTerm={searchTerm}
                                />
                            }
                        />

                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />

                        <Route
                            path="/profile"
                            element={
                                <ProtectedRoute>
                                    <ProfilePage />
                                </ProtectedRoute>
                            }
                        />

                        <Route
                            path="/udlejning"
                            element={
                                <ProtectedRoute>
                                    <Udlejning />
                                </ProtectedRoute>
                            }
                        />

                        <Route
                            path="/kurv"
                            element={
                                <ProtectedRoute>
                                    <Kurv />
                                </ProtectedRoute>
                            }
                        />

                        <Route path="/support" element={<SupportPage />} />
                    </Routes>
                </main>
            </div>

            <Footer />
        </div>
    );
}
