import { useUser } from "../Context/UserContext.jsx";
import { useState } from "react";

export default function Profile() {

    // Access user data and authentication functions from UserContext
    const { user, login, logout } = useUser();

    // Local state for editable profile fields
    const [nameInput, setNameInput] = useState(user?.name || "");
    const [emailInput, setEmailInput] = useState(user?.email || "");

    // Local state for password change fields
    const [oldPass, setOldPass] = useState("");
    const [newPass, setNewPass] = useState("");

    /**
     * Handles updating the user's profile information (name and email)
     */
    async function handleProfileUpdate(e) {
        e.preventDefault(); // Prevent page reload

        // Create updated user object
        const updatedUser = {
            id: user.id,
            name: nameInput,
            email: emailInput
        };

        // Send updated profile data to backend
        const response = await fetch(`http://localhost:8080/api/users/${user.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedUser),
        });

        // Show error if update fails
        if (!response.ok) {
            alert("Failed to update profile");
            return;
        }

        // Update user data in global context
        const updatedData = await response.json();
        login(updatedData);

        alert("Profil opdateret!");
    }

    /**
     * Handles changing the user's password
     */
    async function handleChangePassword(e) {
        e.preventDefault(); // Prevent page reload

        // Send password change request to backend
        const response = await fetch(`http://localhost:8080/api/users/${user.id}/password`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                oldPassword: oldPass,
                newPassword: newPass
            }),
        });

        // Show error if old password is incorrect
        if (!response.ok) {
            alert("Forkert nuv&aelig;rende password");
            return;
        }

        // Clear password fields after successful update
        setOldPass("");
        setNewPass("");

        alert("Password &aelig;ndret!");
    }

    return (
        <div className="profile-page">

            {/* Section: Profile information */}
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

                    {/* Save profile changes */}
                    <button type="submit" className="primary-btn profile-btn">
                        Gem &aelig;ndringer
                    </button>
                </form>
            </section>

            {/* Section: Change password */}
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

                    {/* Submit password update */}
                    <button type="submit" className="primary-btn profile-btn">
                        Opdater password
                    </button>
                </form>
            </section>

            {/* Logout button */}
            <button
                className="primary-btn profile-logout-btn"
                onClick={() => logout()}
            >
                Log ud
            </button>
        </div>
    );
}
