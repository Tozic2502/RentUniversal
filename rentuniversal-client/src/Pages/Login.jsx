import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useUser } from "../Context/UserContext";

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
        e.preventDefault(); // Prevent page reload
        setErrorMessage(""); // Clear previous errors

        // Send login request to backend
        const response = await fetch("http://localhost:8080/api/users/authenticate", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password }),
        });

        // Show error message if login fails
        if (!response.ok) {
            setErrorMessage("Forkert email eller password!");
            return;
        }

        // Parse logged-in user data from response
        const loggedInUser = await response.json();

        // Store user in global context
        login(loggedInUser);

        // Navigate to profile page after successful login
        navigate("/profile");
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
