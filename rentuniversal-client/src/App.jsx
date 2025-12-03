import { Link, Routes, Route } from "react-router-dom";
import Home from "./Pages/Home.jsx";
import Udlejning from "./Pages/Rental.jsx";
import Kurv from "./Pages/Cart.jsx";
import Login from "./Pages/Login.jsx";
import { useUser } from "./Context/UserContext.jsx";
import ProtectedRoute from "./Components/ProtectedRoute.jsx";
import ProfilePage from "./Pages/ProfilePage.jsx";

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
                            <Link to="/profile" style={{ marginRight: "20px", color: "lightgreen" }}>
                                {user.name || user.email}
                            </Link>
                            <button onClick={logout}>Logout</button>
                        </>
                    ) : (
                        <Link to="/login" style={{ color: "white" }}>Login</Link>
                    )}
                </div>
            </nav>

            <Routes>
                <Route path="/" element={<Home />} />
                <Route
                    path="/profile"
                    element={
                        <ProtectedRoute>
                            <ProfilePage />
                        </ProtectedRoute>
                    }
                />
                <Route path="/udlejning" element={<ProtectedRoute><Udlejning /></ProtectedRoute>} />
                <Route path="/kurv" element={<ProtectedRoute><Kurv /></ProtectedRoute>} />
                <Route path="/login" element={<Login />} />
            </Routes>
        </div>
    );
}
