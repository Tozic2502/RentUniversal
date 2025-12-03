import { useState } from "react";
import { useUser } from "../Context/UserContext.jsx";
import { useNavigate } from "react-router-dom";

function Login() {
    const { user, login, logout } = useUser();
    const navigate = useNavigate();

    // States for login
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    // States for profile editing
    const [nameInput, setNameInput] = useState(user?.name || "");
    const [emailInput, setEmailInput] = useState(user?.email || "");

    async function handleLogin(e) {
        e.preventDefault();
        setError("");

        try {
            const response = await fetch("http://localhost:8080/api/users/authenticate", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, password })
            });

            if (!response.ok) {
                setError("Forkert email eller password");
                return;
            }

            const userData = await response.json();
            login(userData);
            navigate("/");  // redirect home after login
        } catch (err) {
            setError("Serverfejl - prøv igen");
        }
    }

    async function handleProfileUpdate(e) {
        e.preventDefault();

        const updatedUser = {
            id: user.id,
            name: nameInput,
            email: emailInput
        };

        const response = await fetch(`http://localhost:8080/api/users/${user.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedUser),
        });

        if (!response.ok) {
            alert("Failed to update profile");
            return;
        }

        const newUserData = await response.json();
        login(newUserData); // Update context!

        alert("Profil opdateret!");
    }


    // 🔥 If user is logged in → Show Profile UI
    if (user) {
        return (
            <div>
                <h2>Din Profil</h2>
                <form onSubmit={handleProfileUpdate}>
                    <label>Navn</label>
                    <input
                        type="text"
                        value={nameInput}
                        onChange={(e) => setNameInput(e.target.value)}
                    />

                    <label>Email</label>
                    <input
                        type="email"
                        value={emailInput}
                        onChange={(e) => setEmailInput(e.target.value)}
                    />

                    <button type="submit">Gem ændringer</button>
                </form>

                <button
                    style={{ marginTop: "15px" }}
                    onClick={() => { logout(); navigate("/"); }}
                >
                    Log ud
                </button>
            </div>
        );
    }

    // 🔑 If user NOT logged in → Show Login UI
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

            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />

            <button type="submit">Login</button>
        </form>
    );
}

export default Login;
