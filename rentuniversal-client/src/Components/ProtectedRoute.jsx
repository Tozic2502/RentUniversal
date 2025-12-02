import { Navigate } from "react-router-dom";
import { useUser } from "../Context/UserContext.jsx";

export default function ProtectedRoute({ children }) {
    const { user } = useUser();

    if (!user) {
        return <Navigate to="/login" replace />;
    }

    return children;
}
