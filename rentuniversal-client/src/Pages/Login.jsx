import { useState } from "react";
import { useUser } from "../Context/UserContext.jsx";
import { useNavigate } from "react-router-dom";

function Login() {
    const { login } = useUser();
    const [email, setEmail] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    async function handleLogin(e) {
        e.preventDefault();
        setError("");

        try {
            const response = await fetch("http://localhost:8080/api/users/authenticate", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email }) // <-- FIXED
            });

            if (!response.ok) {
                setError("Login mislykkedes");
                return;
            }

            const user = await response.json();
            login(user);

            navigate("/"); // after login go to Home
        } catch (err) {
            console.error(err);
            setError("Server fejl - prøv igen");
        }
    }

    return (
        <form onSubmit={handleLogin}>
            <h1>Login</h1>

            {error && <p style={{ color: "red" }}>{error}</p>}

            <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <button type="submit">Login</button>
        </form>
    );
}

export default Login;
