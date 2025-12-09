// src/Pages/Register.jsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Register() {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [successMessage, setSuccessMessage] = useState("");

    const navigate = useNavigate();

    async function handleRegister(e) {
        e.preventDefault();
        setErrorMessage("");
        setSuccessMessage("");

        try {
            const response = await fetch("http://localhost:8080/api/users/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ name, email, password }),
            });

            if (!response.ok) {
                const body = await response.json().catch(() => null);
                const msg = body?.message || "Der skete en fejl under oprettelse.";
                setErrorMessage(msg);
                return;
            }

            setSuccessMessage("Konto oprettet. Du kan nu logge ind.");
            setTimeout(() => {
                navigate("/login");
            }, 1000);
        } catch (err) {
            console.error(err);
            setErrorMessage("Serverfejl – prøv igen senere.");
        }
    }

    return (
        <section className="login-section">
            <div className="login-card">
                <h2 className="login-title">Opret konto</h2>

                {errorMessage && (
                    <p className="login-error" style={{ color: "red", fontSize: "0.9rem" }}>
                        {errorMessage}
                    </p>
                )}

                {successMessage && (
                    <p className="login-success" style={{ color: "green", fontSize: "0.9rem" }}>
                        {successMessage}
                    </p>
                )}

                <form className="login-form" onSubmit={handleRegister}>
                    <div className="login-field">
                        <label>Fulde navn</label>
                        <input
                            type="text"
                            placeholder="Dit fulde navn"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </div>

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
                            placeholder="Adgangskode"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>

                    <button type="submit" className="primary-btn login-btn">
                        Opret konto
                    </button>
                </form>
            </div>
        </section>
    );
}
