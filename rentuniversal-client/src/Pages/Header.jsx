// src/Pages/Header.jsx
import { NavLink, useNavigate } from "react-router-dom";
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
            </div>

            <div className="header-right">
                {user ? (
                    <>
                        <span className="header-user">
                            {user.name || user.email}
                        </span>
                        <button
                            className="header-logout"
                            onClick={() => {
                                logout();
                                navigate("/");
                            }}
                        >
                            Logout
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
            <img src={icon} alt={label} className="nav-icon" />
            <span>{label}</span>
        </NavLink>
    );
}

export default Header;
