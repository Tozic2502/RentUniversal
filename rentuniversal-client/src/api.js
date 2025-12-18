const API_BASE = "http://localhost:8080/api";

// ---------------- Items ----------------

export async function getItems() {
    const response = await fetch(`${API_BASE}/items`);
    if (!response.ok) throw new Error("Failed to load items");
    return response.json();
}

// ---------------- Rentals ----------------

// Create a rental
export async function rentItem(userId, itemId, startDate, endDate) {
    const response = await fetch(`http://localhost:8080/api/rentals`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userId, itemId, startDate, endDate }),
    });

    if (!response.ok) throw new Error("Renting item failed");
    return response.json();
}


// Get rentals by user
export async function getUserRentals(userId) {
    const response = await fetch(`${API_BASE}/rentals/user/${userId}`);
    if (!response.ok) throw new Error("Failed to load rentals");
    return response.json();
}

// Return rental
export async function returnRental(rentalId) {
    const response = await fetch(`${API_BASE}/Rentals/return/${rentalId}`, {
        method: "PUT",
    });

    if (!response.ok) throw new Error("Failed to return rental");
}

// -------------------Contact---------------------

// Post contact message
const response = await fetch("http://localhost:8080/api/support-messages", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ name, email, message }), 
});