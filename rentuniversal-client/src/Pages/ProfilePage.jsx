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
        <div style={{ padding: "20px" }}>
            <h2>Min Profil</h2>

            <form onSubmit={handleProfileUpdate}>
                <label>Navn</label><br />
                <input type="text" value={nameInput} onChange={e => setNameInput(e.target.value)} /><br /><br />

                <label>Email</label><br />
                <input type="email" value={emailInput} onChange={e => setEmailInput(e.target.value)} /><br /><br />

                <button type="submit">Gem ændringer</button>
            </form>
            <hr style={{ margin: "20px 0" }} />

            <h3>Skift password</h3>

            <form onSubmit={handleChangePassword}>
                <label>Gammelt password</label><br />
                <input
                    type="password"
                    value={oldPass}
                    onChange={(e) => setOldPass(e.target.value)}
                /><br /><br />

                <label>Nyt password</label><br />
                <input
                    type="password"
                    value={newPass}
                    onChange={(e) => setNewPass(e.target.value)}
                /><br /><br />

                <button type="submit">Opdater password</button>
            </form>

            <button style={{ marginTop: "20px" }} onClick={() => logout()}>
                Log ud
            </button>
        </div>
    );
}