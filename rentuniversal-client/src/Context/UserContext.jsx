import { createContext, useState, useContext, useEffect } from "react";

// Create a context to manage user-related data and actions
const UserContext = createContext();

// Provider component to wrap the application and provide user context
export function UserProvider({ children }) {
    // State to store the current user object
    const [user, setUser] = useState(null);
    // State to track whether the user data is still loading
    const [loading, setLoading] = useState(true);

    // Effect to load user data from localStorage when the component mounts
    useEffect(() => {
        const storedUser = localStorage.getItem("user");
        if (storedUser) {
            // Parse and set the user data if it exists in localStorage
            setUser(JSON.parse(storedUser));
        }
        // Mark loading as complete
        setLoading(false);
    }, []);

    // Function to log in a user and save their data to localStorage
    const login = (userData) => {
        setUser(userData); // Update the user state
        localStorage.setItem("user", JSON.stringify(userData)); // Persist user data
    };

    // Function to log out a user and remove their data from localStorage
    const logout = () => {
        setUser(null); // Clear the user state
        localStorage.removeItem("user"); // Remove user data from storage
    };

    // Provide the user data, login, logout functions, and loading state to children components
    return (
        <UserContext.Provider value={{ user, login, logout, loading }}>
            {children}
        </UserContext.Provider>
    );
}

// Custom hook to access the UserContext
export function useUser() {
    return useContext(UserContext);
}