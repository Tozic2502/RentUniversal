import "./App.css";
import { Routes, Route } from "react-router-dom";
import { useState } from "react";
import Home from "./Pages/Home.jsx";
import Rental from "./Pages/Rental.jsx"; // Browse & add to cart
import Kurv from "./Pages/Cart.jsx";
import Login from "./Pages/Login.jsx";
import ProfilePage from "./Pages/ProfilePage.jsx";
import Header from "./Pages/Header.jsx";
import Footer from "./Pages/Footer.jsx";
import SideKategori from "./Pages/SideKategori.jsx";
import Udlejning from "./Pages/Udlejning.jsx"; // My rentals
import ProtectedRoute from "./Components/ProtectedRoute.jsx";

export default function App() {
    // Hvilken kategori er valgt i venstremenuen (null = alle)
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
                        {/* Home får valgt kategori som prop */}
                        <Route
                            path="/"
                            element={<Home selectedCategory={selectedCategory} />}
                        />
                        <Route path="/login" element={<Login />} />

                        {/* 🔒 Protected pages */}
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
                       

                    </Routes>
                </main>
            </div>

            <Footer />
        </div>
    );
}
