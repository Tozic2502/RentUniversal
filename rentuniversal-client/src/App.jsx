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

export default function App() {
    // Hvilken kategori er valgt i venstremenuen (null = alle)
    const [selectedCategory, setSelectedCategory] = useState(null);

    return (
        <div className="app">
            {/* Øverste menu med dine 4 ikoner + login/logout */}
            <Header />

            {/* Layout under header: venstre kategori + indhold til højre */}
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

                        {/* Profil er beskyttet */}
                        <Route
                            path="/profile"
                            element={
                                <ProtectedRoute>
                                    <ProfilePage />
                                </ProtectedRoute>
                            }
                        />

                        {/* Udlejning er beskyttet */}
                        <Route
                            path="/udlejning"
                            element={
                                <ProtectedRoute>
                                    <Udlejning />
                                </ProtectedRoute>
                            }
                        />

                        {/* Kurv er beskyttet */}
                        <Route
                            path="/kurv"
                            element={
                                <ProtectedRoute>
                                    <Kurv />
                                </ProtectedRoute>
                            }
                        />

                        {/* Login er offentlig */}
                        <Route path="/login" element={<Login />} />
                    </Routes>
                </main>
            </div>

            <Footer />
        </div>
    );
}
