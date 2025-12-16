import { useUser } from "../Context/UserContext.jsx";
import { useState } from "react";

export default function Profile() {
    const { user, login, logout } = useUser();

    const [nameInput, setNameInput] = useState(user?.name || "");
    const [emailInput, setEmailInput] = useState(user?.email || "");
    const [oldPass, setOldPass] = useState("");
    const [newPass, setNewPass] = useState("");

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

        const updatedData = await response.json();
        login(updatedData);

        alert("Profil opdateret!");
    }

    async function handleChangePassword(e) {
        e.preventDefault();

        const response = await fetch(`http://localhost:8080/api/users/${user.id}/password`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                oldPassword: oldPass,
                newPassword: newPass
            }),
        });

        if (!response.ok) {
            alert("Forkert nuværende password");
            return;
        }

        setOldPass("");
        setNewPass("");
        alert("Password ændret!");
    }

    return (
        <div className="profile-page">
            {/* Sektion: profiloplysninger */}
            <section className="profile-section">
                <h2>Min profil</h2>

                <form onSubmit={handleProfileUpdate}>
                    <label htmlFor="profile-name">Navn</label>
                    <input
                        id="profile-name"
                        type="text"
                        value={nameInput}
                        onChange={(e) => setNameInput(e.target.value)}
                    />

                    <label htmlFor="profile-email">Email</label>
                    <input
                        id="profile-email"
                        type="email"
                        value={emailInput}
                        onChange={(e) => setEmailInput(e.target.value)}
                    />

                    <button type="submit" className="primary-btn profile-btn">
                        Gem ændringer
                    </button>
                </form>
            </section>

            {/* Sektion: skift password */}
            <section className="profile-section">
                <h3>Skift password</h3>

                <form onSubmit={handleChangePassword}>
                    <label htmlFor="old-password">Gammelt password</label>
                    <input
                        id="old-password"
                        type="password"
                        value={oldPass}
                        onChange={(e) => setOldPass(e.target.value)}
                    />

                    <label htmlFor="new-password">Nyt password</label>
                    <input
                        id="new-password"
                        type="password"
                        value={newPass}
                        onChange={(e) => setNewPass(e.target.value)}
                    />

                    <button type="submit" className="primary-btn profile-btn">
                        Opdater password
                    </button>
                </form>
            </section>

            {/* Log ud-knap */}
            <button
                className="primary-btn profile-logout-btn"
                onClick={() => logout()}
            >
                Log ud
            </button>
        </div>
    );
}