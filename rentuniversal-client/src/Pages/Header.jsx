// src/Pages/Header.jsx
import { Link, NavLink, useNavigate } from "react-router-dom";
import { useUser } from "../Context/UserContext.jsx";
import HomeImg from "../assets/HomeIcon.svg";
import UdlejningImg from "../assets/RentIcon.svg";
import IndkobsvognImg from "../assets/BasketIcon.svg";
import LoginImg from "../assets/ProfileIcon.svg";

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
                        {/* Samme ikon som Login, men label = brugernavn */}
                        <NavIcon
                            to="/profile"
                            label={user.name || user.email}
                            icon={LoginImg}
                        />

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