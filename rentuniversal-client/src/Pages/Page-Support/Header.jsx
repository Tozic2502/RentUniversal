// src/Pages/Header.jsx
import { Link, NavLink, useNavigate } from "react-router-dom";
import HomeImg from "../../assets/HomeIcon.svg";
import UdlejningImg from "../../assets/RentIcon.svg";
import IndkobsvognImg from "../../assets/BasketIcon.svg";
import LoginImg from "../../assets/ProfileIcon.svg";
import SupImg from "../../assets/SupportIcon.svg";
import { useUser } from "../../Context/UserContext.jsx";

// Header component: Represents the navigation bar of the application
function Header() {
    // Extract user information and logout function from the UserContext
    const { user, logout } = useUser();
    // Hook to programmatically navigate between routes
    const navigate = useNavigate();

    return (
        <header className="header">
            {/* Left section of the header containing navigation icons */}
            <div className="header-left">
                <NavIcon to="/" label="Home" icon={HomeImg} />
                <NavIcon to="/udlejning" label="Udlejning" icon={UdlejningImg} />
                <NavIcon to="/kurv" label="Kurv" icon={IndkobsvognImg} />
                {/* New menu item for Support */}
                <NavIcon to="/support" label="Support" icon={SupImg} />
            </div>

            {/* Right section of the header for user-related actions */}
            <div className="header-right">
                {user ? (
                    <>
                        {/* Display user profile link */}
                        <Link to="/profile" className="header-user">
                            <NavIcon to="/login" label={user.name || user.email} icon={LoginImg} />
                        </Link>

                        {/* Logout button */}
                        <button
                            className="header-logout"
                            onClick={() => {
                                logout(); // Log the user out
                                navigate("/"); // Redirect to the home page
                            }}
                        >
                            Log ud
                        </button>
                    </>
                ) : (
                    // Display login option if no user is logged in
                    <NavIcon to="/login" label="Login" icon={LoginImg} />
                )}
            </div>
        </header>
    );
}

// NavIcon component: Represents a single navigation button with an icon and label
function NavIcon({ to, label, icon }) {
    return (
        <NavLink
            to={to}
            className={({ isActive }) =>
                "nav-button" + (isActive ? " nav-button-active" : "") // Apply active class if the route is active
            }
        >
            {/* Display the icon if provided */}
            {icon && <img src={icon} alt={label} className="nav-icon" />}
            <span>{label}</span> {/* Display the label */}
        </NavLink>
    );
}

// Export the Header component as the default export
export default Header;