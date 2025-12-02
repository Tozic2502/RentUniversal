import { Link, Routes, Route } from "react-router-dom";
import Home from "./Pages/Home.jsx";
import Udlejning from "./Pages/Rental.jsx";
import Kurv from "./Pages/Cart.jsx";
import Login from "./Pages/Login.jsx";
import { useUser } from "./Context/UserContext.jsx";
import ProtectedRoute from "./Components/ProtectedRoute.jsx"; // <-- ADD THIS

export default function App() {
    const { user, logout } = useUser();

    return (
        <div>
            <nav
                style={{
                    padding: "10px",
                    background: "#222",
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                }}
            >
                <div>
                    <Link to="/" style={{ marginRight: "20px", color: "white" }}>Home</Link>
                    <Link to="/udlejning" style={{ marginRight: "20px", color: "white" }}>Udlejning</Link>
                    <Link to="/kurv" style={{ marginRight: "20px", color: "white" }}>Kurv</Link>
                </div>

                <div>
                    {user ? (
                        <>
                            <span style={{ color: "lightgreen", marginRight: "15px" }}>
                                {user.name || user.email}
                            </span>

                            <button
                                onClick={() => {
                                    logout();
                                    window.location.href = "/";
                                }}
                                style={{
                                    background: "crimson",
                                    color: "white",
                                    border: "none",
                                    padding: "6px 10px",
                                    cursor: "pointer",
                                    borderRadius: "4px",
                                }}
                            >
                                Logout
                            </button>
                        </>
                    ) : (
                        <Link to="/login" style={{ color: "white" }}>Login</Link>
                    )}
                </div>
            </nav>

            <Routes>
                <Route path="/" element={<Home />} />

                {/* 🔒 Protected Routes */}
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

                {/* Public route */}
                <Route path="/login" element={<Login />} />
            </Routes>
        </div>
    );
}
