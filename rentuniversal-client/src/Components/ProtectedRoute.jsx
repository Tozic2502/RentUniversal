import { Navigate } from "react-router-dom";
import { useUser } from "../Context/UserContext.jsx";

// ProtectedRoute component ensures that only authenticated users can access certain routes.
export default function ProtectedRoute({ children }) {
    // Destructure user and loading state from the UserContext.
    const { user, loading } = useUser();

    // If the loading state is true, display a loading message to prevent premature redirection.
    if (loading) {
        return <div>Loading...</div>; // prevent redirect too early
    }

    // If no user is authenticated, redirect to the login page.
    if (!user) {
        return <Navigate to="/login" replace />;
    }

    // If the user is authenticated, render the child components.
    return children;
}