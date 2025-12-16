// src/Pages/Register.jsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Register() {
    const [fullName, setFullName] = useState("");
    const [email, setEmail] = useState("");
    const [identification, setIdentification] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState("");

    const navigate = useNavigate();

    async function handleSubmit(e) {
        e.preventDefault();
        setError("");

        // 1) Valider identifikation: præcis 10 cifre
        if (!/^\d{10}$/.test(identification)) {
            setError("Identifikation skal være præcis 10 cifre.");
            return;
        }

        // 2) Tjek at adgangskoderne er ens
        if (password !== confirmPassword) {
            setError("Adgangskoderne matcher ikke.");
            return;
        }

        // 3) Payload til dit API (tilpas URL hvis nødvendigt)
        const payload = {
            name: fullName,
            email: email,
            password: password,
            identificationId: identification
        };

        try {
            const response = await fetch("http://localhost:8080/api/users/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload)
            });

            if (!response.ok) {
                setError("Kunne ikke oprette bruger. Prøv igen.");
                return;
            }

            // Ved succes: send brugeren tilbage til login
            navigate("/login");
        } catch (err) {
            console.error("Register error", err);
            setError("Der skete en fejl. Prøv igen.");
        }
    }

    return (
        <section className="login-section">
            <div className="login-card">
                <h2 className="login-title">Opret konto</h2>

                {error && <p className="login-error">{error}</p>}

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
                                // tillad kun tal og max 10 tegn
                                const value = e.target.value.replace(/\D/g, "").slice(0, 10);
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

                    <button type="submit" className="primary-btn login-btn">
                        Opret konto
                    </button>
                </form>
            </div>
        </section>
    );
}
