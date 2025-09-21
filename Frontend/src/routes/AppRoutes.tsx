import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "../pages/auth/Login";
import DashboardPage from "../pages/Dashboard";
import AdminPage from "../pages/Admin";
import PrivateRoute from "./PrivateRoute";

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Route mặc định */}
        <Route path="/" element={<Navigate to="/login" replace />} />

        <Route path="/login" element={<LoginPage />} />

        <Route
          path="/dashboard"
          element={
            <PrivateRoute>
              <DashboardPage />
            </PrivateRoute>
          }
        />

        <Route
          path="/admin"
          element={
            <PrivateRoute roles={["Admin"]}>
              <AdminPage />
            </PrivateRoute>
          }
        />

        {/* Unauthorized page */}
        <Route path="/unauthorized" element={<h1>403 Unauthorized</h1>} />
      </Routes>
    </BrowserRouter>
  );
}
