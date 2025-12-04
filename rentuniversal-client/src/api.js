const API_BASE = "http://localhost:8080/api";

// ---------------- Items ----------------

export async function getItems() {
    const response = await fetch(`${API_BASE}/items`);
    if (!response.ok) throw new Error("Failed to load items");
    return response.json();
}

// ---------------- Rentals ----------------

// Create a rental
export async function rentItem(userID, itemID) {
    const response = await fetch(`${API_BASE}/rentals`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userID, itemID }),
    });

    if (!response.ok) {
        throw new Error("Renting item failed");
    }
}

// Get rentals by user
export async function getUserRentals(userId) {
    const response = await fetch(`${API_BASE}/rentals/user/${userId}`);
    if (!response.ok) throw new Error("Failed to load rentals");
    return response.json();
}

// Return rental (future feature - optional)
export async function returnRental(rentalId) {
    const response = await fetch(`${API_BASE}/rentals/return/${rentalId}`, {
        method: "PUT",
    });

    if (!response.ok) throw new Error("Failed to return rental");
}
