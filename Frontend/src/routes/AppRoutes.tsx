import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "../pages/auth/Login";
import AdminPage from "../pages/Admin";
import PrivateRoute from "./PrivateRoute";
import HomePage from "../pages/HomePage";
export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Route mặc định */}
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />

        <Route
          path="/dashboard"
          element={
            <PrivateRoute>
              <h1>Dashboard</h1>
            </PrivateRoute>
          }
        />

        <Route
          path="/admin"
          element={
            <PrivateRoute roles={["admin"]}>
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
