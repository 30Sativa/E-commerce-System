import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import type { JSX } from "react";

export default function PrivateRoute({
  children,
  roles,
}: {
  children: JSX.Element;
  roles?: string[];
}) {
  const { user } = useAuth();

  if (!user) {
    return <Navigate to="/login" replace />;
  }

  if (
    roles &&
    !roles.map((r) => r.toLowerCase()).includes(user.role?.toLowerCase())
  ) {
    return <Navigate to="/unauthorized" replace />;
  }

  return children;
}
