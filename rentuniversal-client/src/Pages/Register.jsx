// src/Pages/Register.jsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Register() {

    // Local state for form input fields
    const [fullName, setFullName] = useState("");
    const [email, setEmail] = useState("");
    const [identification, setIdentification] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    // State for displaying validation and server errors
    const [error, setError] = useState("");

    // Used to redirect user after successful registration
    const navigate = useNavigate();

    /**
     * Handles registration form submission
     * Performs client-side validation before sending data to the backend
     */
    async function handleSubmit(e) {
        e.preventDefault(); // Prevent page reload
        setError(""); // Clear previous error messages

        // 1) Validate identification: must be exactly 10 digits
        if (!/^\d{10}$/.test(identification)) {
            setError("Identifikation skal være præcis 10 cifre.");
            return;
        }

        // 2) Check that both passwords match
        if (password !== confirmPassword) {
            setError("Adgangskoderne matcher ikke.");
            return;
        }

        // 3) Prepare payload for backend API
        const payload = {
            name: fullName,
            email: email,
            password: password,
            identificationId: identification
        };

        try {
            // Send registration request to backend
            const response = await fetch("http://localhost:8080/api/users/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload)
            });

            // Show error if registration fails
            if (!response.ok) {
                setError("Kunne ikke oprette bruger. Prøv igen.");
                return;
            }

            // On success: redirect user to login page
            navigate("/login");

        } catch (err) {
            // Handle network or unexpected errors
            console.error("Register error", err);
            setError("Der skete en fejl. Prøv igen.");
        }
    }

    return (
        <section className="login-section">
            <div className="login-card">
                <h2 className="login-title">Opret konto</h2>

                {/* Display validation or server error */}
                {error && <p className="login-error">{error}</p>}

                {/* Registration form */}
                <form className="login-form" onSubmit={handleSubmit}>

                    <div className="login-field">
                        <label>Fulde navn</label>
                        <input
                            type="text"
                            placeholder="Dit fulde navn"
                            value={fullName}
                            onChange={e => setFullName(e.target.value)}
                            required
                        />
                    </div>

                    <div className="login-field">
                        <label>Email</label>
                        <input
                            type="email"
                            placeholder="din@email.dk"
                            value={email}
                            onChange={e => setEmail(e.target.value)}
                            required
                        />
                    </div>

                    <div className="login-field">
                        <label>Identifikation (10 cifre)</label>
                        <input
                            type="text"
                            inputMode="numeric"
                            maxLength={10}
                            placeholder="F.eks. 1234567890"
                            value={identification}
                            onChange={e => {
                                // Allow only numeric input and limit to 10 characters
                                const value = e.target.value
                                    .replace(/\D/g, "")
                                    .slice(0, 10);
                                setIdentification(value);
                            }}
                            required
                        />
                    </div>

                    <div className="login-field">
                        <label>Adgangskode</label>
                        <input
                            type="password"
                            placeholder="Adgangskode"
                            value={password}
                            onChange={e => setPassword(e.target.value)}
                            required
                        />
                    </div>

                    <div className="login-field">
                        <label>Bekræft adgangskode</label>
                        <input
                            type="password"
                            placeholder="Gentag adgangskode"
                            value={confirmPassword}
                            onChange={e => setConfirmPassword(e.target.value)}
                            required
                        />
                    </div>

                    {/* Submit registration */}
                    <button type="submit" className="primary-btn login-btn">
                        Opret konto
                    </button>
                </form>
            </div>
        </section>
    );
}
