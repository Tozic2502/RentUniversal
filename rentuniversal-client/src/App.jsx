import { Link, Routes, Route } from "react-router-dom";
import Home from "./Pages/Home.jsx";
import Udlejning from "./Pages/Rental.jsx";
import Kurv from "./Pages/Cart.jsx";
import Login from "./Pages/Login.jsx";

export default function App() {
    return (
        <div>
            <nav style={{ padding: "10px", background: "#222" }}>
                <Link to="/" style={{ marginRight: "20px", color: "white" }}>Home</Link>
                <Link to="/udlejning" style={{ marginRight: "20px", color: "white" }}>Udlejning</Link>
                <Link to="/kurv" style={{ marginRight: "20px", color: "white" }}>Kurv</Link>
                <Link to="/login" style={{ color: "white" }}>Login</Link>
            </nav>

            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/udlejning" element={<Udlejning />} />
                <Route path="/kurv" element={<Kurv />} />
                <Route path="/login" element={<Login />} />
            </Routes>
        </div>
    );
}
