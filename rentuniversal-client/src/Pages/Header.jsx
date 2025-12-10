// src/Pages/Header.jsx
import { Link, NavLink, useNavigate } from "react-router-dom";
import { useUser } from "../Context/UserContext.jsx";
import HomeImg from "../assets/Home.jpg";
import UdlejningImg from "../assets/Udlejning.jpg";
import IndkobsvognImg from "../assets/Indkobsvogn.jpg";
import LoginImg from "../assets/Login.jpg";

function Header() {
    const { user, logout } = useUser();
    const navigate = useNavigate();

    return (
        <header className="header">
            <div className="header-left">
                <NavIcon to="/" label="Home" icon={HomeImg} />
                <NavIcon to="/udlejning" label="Udlejning" icon={UdlejningImg} />
                <NavIcon to="/kurv" label="Kurv" icon={IndkobsvognImg} />
                {/* Nyt menupunkt */}
                <NavIcon to="/support" label="Support" />
            </div>

            <div className="header-right">
                {user ? (
                    <>
                        <Link to="/profile" className="header-user">
                            {user.name || user.email}
                        </Link>

                        <button
                            className="header-logout"
                            onClick={() => {
                                logout();
                                navigate("/");
                            }}
                        >
                            Log ud
                        </button>
                    </>
                ) : (
                    <NavIcon to="/login" label="Login" icon={LoginImg} />
                )}
            </div>
        </header>
    );
}

function NavIcon({ to, label, icon }) {
    return (
        <NavLink
            to={to}
            className={({ isActive }) =>
                "nav-button" + (isActive ? " nav-button-active" : "")
            }
        >
            {icon && <img src={icon} alt={label} className="nav-icon" />}
            <span>{label}</span>
        </NavLink>
    );
}

export default Header;
