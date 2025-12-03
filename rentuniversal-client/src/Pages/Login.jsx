import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useUser } from "../context/UserContext";


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
        <div className="login-page">
            <div className="login-box">
                <h2>Log ind</h2>

                {errorMessage && <p className="login-error">{errorMessage}</p>}

                <form onSubmit={handleLogin}>
                    <input
                        type="email"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />

                    <button type="submit" className="login-btn">Log ind</button>
                </form>
            </div>
        </div>
    );
}
