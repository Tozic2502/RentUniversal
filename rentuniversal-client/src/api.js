const API_BASE = "http://localhost:8080/api";

// ---------------- Items ----------------

export async function getItems() {
    const response = await fetch(`${API_BASE}/items`);
    if (!response.ok) throw new Error("Failed to load items");

    const items = await response.json();

    // Only return available items
    return items.filter(item => item.isAvailable === true);
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

// ---------------- Users ----------------

//Authenticate user with email + password
export async function loginUser(email, password) {
    const response = await fetch(`${API_BASE}/users/authenticate`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
        throw new Error("Invalid email or password");
    }

    // Read raw text first (prevents JSON crash)
    const text = await response.text();

    // If backend returns no body
    if (!text) {
        return null;
    }

    // Parse JSON safely
    return JSON.parse(text);
}

// -------------------Contact---------------------

// Post contact message
export async function supportMessage(id, name, email, message) {
    const response = await fetch(`${API_BASE}/Contact`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            id,
            name,
            email,
            message
        })
    });
   
    if (!response.ok) {
        throw new Error("Failed to send contact message");
    }

    return true;
}
