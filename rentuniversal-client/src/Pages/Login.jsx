import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useUser } from "../Context/UserContext";
import { loginUser } from "../api.js";

export default function Login() {

    // Access the current user and login function from UserContext
    const { user, login } = useUser();

    // Used for programmatic navigation after successful login
    const navigate = useNavigate();

    // Local state for form inputs
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    // State for displaying login error messages
    const [errorMessage, setErrorMessage] = useState("");

    /**
     * Redirect the user to the profile page
     * if they are already logged in
     */
    useEffect(() => {
        if (user) {
            navigate("/profile");
        }
    }, [user, navigate]);

    /**
     * Handles the login form submission
     * Sends credentials to the backend API
     */
    async function handleLogin(e) {
        e.preventDefault();
        setErrorMessage("");

        try {
            const loggedInUser = await loginUser(email, password);

            if (!loggedInUser) {
                throw new Error("No user returned");
            }

            login(loggedInUser);
            navigate("/profile");

        } catch (error) {
            console.error("Login error:", error);
            setErrorMessage("Forkert email eller password!");
        }
    }


    return (
        <section className="login-section">
            <div className="login-card">
                <h2 className="login-title">Log ind</h2>

                {/* Display error message if login fails */}
                {errorMessage && (
                    <p className="login-error">{errorMessage}</p>
                )}

                {/* Login form */}
                <form className="login-form" onSubmit={handleLogin}>
                    <div className="login-field">
                        <label>Email</label>
                        <input
                            type="email"
                            placeholder="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </div>

                    <div className="login-field">
                        <label>Adgangskode</label>
                        <input
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>

                    {/* Submit button */}
                    <button type="submit" className="primary-btn login-btn">
                        Log ind
                    </button>
                </form>

                {/* Link to registration page */}
                <p className="login-register-text">
                    Ny bruger?{" "}
                    <Link to="/register">
                        Opret en konto
                    </Link>
                </p>
            </div>
        </section>
    );
}
