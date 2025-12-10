// src/App.jsx
import "./App.css";
import { Routes, Route } from "react-router-dom";
import { useState } from "react";

import Home from "./Pages/Home.jsx";
import Udlejning from "./Pages/Rental.jsx";
import Kurv from "./Pages/Cart.jsx";
import Login from "./Pages/Login.jsx";
import Register from "./Pages/Register.jsx";                      // NY
import ProtectedRoute from "./Components/ProtectedRoute.jsx";
import ProfilePage from "./Pages/ProfilePage.jsx";
import Header from "./Pages/Header.jsx";
import Footer from "./Pages/Footer.jsx";
import SideKategori from "./Pages/SideKategori.jsx";
import SupportPage from "./Pages/Page-Support/Support.jsx";      // SUPPORT

export default function App() {
    const [selectedCategory, setSelectedCategory] = useState(null);

    return (
        <div className="app">
            <Header />

            <div className="layout">
                <SideKategori
                    selectedCategory={selectedCategory}
                    onSelectCategory={setSelectedCategory}
                />

                <main className="main">
                    <Routes>
                        {/* Forside */}
                        <Route
                            path="/"
                            element={<Home selectedCategory={selectedCategory} />}
                        />

                        {/* Profil (beskyttet) */}
                        <Route
                            path="/profile"
                            element={
                                <ProtectedRoute>
                                    <ProfilePage />
                                </ProtectedRoute>
                            }
                        />

                        {/* Udlejning (beskyttet) */}
                        <Route
                            path="/udlejning"
                            element={
                                <ProtectedRoute>
                                    <Udlejning />
                                </ProtectedRoute>
                            }
                        />

                        {/* Kurv (beskyttet) */}
                        <Route
                            path="/kurv"
                            element={
                                <ProtectedRoute>
                                    <Kurv />
                                </ProtectedRoute>
                            }
                        />

                        {/* Login + Register (offentlige) */}
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />

                        {/* Support / Om os + kontaktformular */}
                        <Route path="/support" element={<SupportPage />} />
                    </Routes>
                </main>
            </div>

            <Footer />
        </div>
    );
}
