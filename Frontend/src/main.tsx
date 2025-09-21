import React, { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { AuthProvider } from "./context/AuthContext.tsx";
import AppRoutes from "./routes/AppRoutes.tsx";
import { GoogleOAuthProvider } from "@react-oauth/google";

// Google OAuth Client ID - Make sure this is configured for localhost:5173 in Google Cloud Console
const clientId =
  "132851444389-stbv6qvjuorm8vqte2mncnc9c94ceprv.apps.googleusercontent.com";

createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <GoogleOAuthProvider clientId={clientId}>
      <AuthProvider>
        <AppRoutes />
      </AuthProvider>
    </GoogleOAuthProvider>
  </React.StrictMode>
);
