import "./App.css";
import { Routes, Route } from "react-router-dom";
import { useState } from "react";

// Pages
import Home from "./Pages/Home.jsx";
import Kurv from "./Pages/Cart.jsx";
import Login from "./Pages/Login.jsx";
import ProfilePage from "./Pages/ProfilePage.jsx";
import Register from "./Pages/Register.jsx";
import SupportPage from "./Pages/Page-Support/Support.jsx";
import Udlejning from "./Pages/Udlejning.jsx";

// Layout components
import Header from "./Pages/Page-Support/Header.jsx";
import Footer from "./Pages/Page-Support/Footer.jsx";
import SideKategori from "./Pages/Page-Support/SideKategori.jsx";

// Route protection
import ProtectedRoute from "./Components/ProtectedRoute.jsx";

export default function App() {

    // Currently selected category in the side menu (null = all categories)
    const [selectedCategory, setSelectedCategory] = useState(null);

    // Search input value used to filter items on the Home page
    const [searchTerm, setSearchTerm] = useState("");

    return (
        <div className="app">

            {/* Global header */}
            <Header />

            <div className="layout">

                {/* Sidebar for category selection and search */}
                <SideKategori
                    selectedCategory={selectedCategory}
                    onSelectCategory={setSelectedCategory}
                    searchTerm={searchTerm}
                    onSearchChange={setSearchTerm}
                />

                {/* Main content area */}
                <main className="main">
                    <Routes>

                        {/* Home page (public) */}
                        <Route
                            path="/"
                            element={
                                <Home
                                    selectedCategory={selectedCategory}
                                    searchTerm={searchTerm}
                                />
                            }
                        />

                        {/* Authentication pages */}
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />

                        {/* 🔒 Protected routes (require login) */}
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

                        {/* Support page (public) */}
                        <Route path="/support" element={<SupportPage />} />

                    </Routes>
                </main>
            </div>

            {/* Global footer */}
            <Footer />
        </div>
    );
}
