import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useUser } from "../Context/UserContext";


export default function Login() {
    const { user, login } = useUser();
    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [errorMessage, setErrorMessage] = useState("");


    useEffect(() => {
        if (user) {
            navigate("/profile");
        }
    }, [user, navigate]);

    async function handleLogin(e) {
        e.preventDefault();
        setErrorMessage("");

        const response = await fetch("http://localhost:8080/api/users/authenticate", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password }),
        });

        if (!response.ok) {
            setErrorMessage("Forkert email eller password!");
            return;
        }

        const loggedInUser = await response.json();
        login(loggedInUser);
        navigate("/profile");
    }

    return (
        <section className="login-section">
            <div className="login-card">
                <h2 className="login-title">Log ind</h2>

                {errorMessage && (
                    <p className="login-error">{errorMessage}</p>
                )}

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

                    <button type="submit" className="primary-btn login-btn">
                        Log ind
                    </button>
                </form>
            </div>
        </section>
    );
}