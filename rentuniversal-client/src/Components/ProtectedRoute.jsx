import { Navigate } from "react-router-dom";
import { useUser } from "../Context/UserContext.jsx";

export default function ProtectedRoute({ children }) {
    const { user, loading } = useUser();

    if (loading) {
        return <div>Loading...</div>; // prevent redirect too early
    }

    if (!user) {
        return <Navigate to="/login" replace />;
    }

    return children;
}