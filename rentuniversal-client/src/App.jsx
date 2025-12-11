// src/App.jsx
import "./App.css";
import { Routes, Route } from "react-router-dom";
import { useState } from "react";
import Home from "./Pages/Home.jsx";
import Udlejning from "./Pages/Rental.jsx";
import Kurv from "./Pages/Cart.jsx";
import Login from "./Pages/Login.jsx";
import ProtectedRoute from "./Components/ProtectedRoute.jsx";
import ProfilePage from "./Pages/ProfilePage.jsx";
import Header from "./Pages/Header.jsx";
import Footer from "./Pages/Footer.jsx";
import SideKategori from "./Pages/SideKategori.jsx";
import Register from "./Pages/Register.jsx";
import SupportPage from "./Pages/Page-Support/Support.jsx";

export default function App() {
    // Hvilken kategori er valgt i venstremenuen (null = alle)
    const [selectedCategory, setSelectedCategory] = useState(null);

    // Tekst i søgefeltet
    const [searchTerm, setSearchTerm] = useState("");

    return (
        <div className="app">
            <Header />

            <div className="layout">
                <SideKategori
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
                                    selectedCategory={selectedCategory}
                                    searchTerm={searchTerm}
                                />
                            }
                        />

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

                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/support" element={<SupportPage />} />
                    </Routes>
                </main>
            </div>

            <Footer />
        </div>
    );
}
