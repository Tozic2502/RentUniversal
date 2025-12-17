// src/Pages/Header.jsx
import { Link, NavLink, useNavigate } from "react-router-dom";
import HomeImg from "../../assets/HomeIcon.svg";
import UdlejningImg from "../../assets/RentIcon.svg";
import IndkobsvognImg from "../../assets/BasketIcon.svg";
import LoginImg from "../../assets/ProfileIcon.svg";
import SupImg from "../../assets/SupportIcon.svg";
import {useUser} from "../../Context/UserContext.jsx";

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
                <NavIcon to="/support" label="Support" icon={SupImg} />
            </div>

            <div className="header-right">
                {user ? (
                    <>
                        <Link to="/profile" className="header-user">
                            <NavIcon to="/login" label={user.name || user.email} icon={LoginImg} />
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