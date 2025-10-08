import React, { useState } from "react";
import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { GoogleLogin } from "@react-oauth/google";

export default function LoginPage() {
  const { login, loginWithGoogle } = useAuth();
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    try {
      const userData = await login(email, password);
      if (userData.role?.toLowerCase() === "admin") {
        navigate("/admin");
        return;
      } else {
        navigate("/");
      }
    } catch (err) {
      setError("Login failed. Please check your email and password.");
    }
  };

  return (
    <div className="font-sans bg-white min-h-screen flex flex-col">
      {/* Header */}
      <header className="fixed top-0 left-0 w-full bg-white shadow z-50">
        <div className="max-w-7xl mx-auto px-6 flex justify-between items-center h-16">
          <h1
            className="text-2xl font-bold text-gray-800 cursor-pointer"
            onClick={() => navigate("/")}
          >
            NexBuy
          </h1>

          <nav className="hidden md:flex space-x-6 font-medium">
            <a href="#" className="hover:text-orange-600">
              Home
            </a>
            <a href="#" className="hover:text-orange-600">
              Shop
            </a>
            <a href="#" className="hover:text-orange-600">
              Contact
            </a>
          </nav>
        </div>
      </header>

      {/* Body */}
      <div className="flex flex-col items-center justify-center flex-1 bg-gradient-to-r from-orange-500 to-pink-500 pt-24 pb-12">
        <div className="bg-white rounded-2xl shadow-xl w-full max-w-md p-8">
          <h2 className="text-3xl font-bold text-gray-800 text-center mb-6">
            Welcome Back
          </h2>
          <p className="text-center text-gray-500 mb-8 text-base">
            Sign in to access your account
          </p>

          {error && (
            <div className="mb-6 bg-red-50 border border-red-200 rounded-lg p-4 text-center">
              <p className="text-red-600 text-sm">{error}</p>
            </div>
          )}

          <form onSubmit={handleSubmit} className="space-y-6">
            <div>
              <input
                type="email"
                placeholder="Enter your email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500 text-gray-900 text-base"
                required
              />
            </div>
            <div>
              <input
                type="password"
                placeholder="Enter your password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500 text-gray-900 text-base"
                required
              />
            </div>

            <button
              type="submit"
              className="w-full bg-orange-500 text-white py-3 rounded-lg font-semibold hover:bg-orange-600 transition duration-200 shadow-md"
            >
              Log In
            </button>
          </form>

          {/* Divider */}
          <div className="my-8 flex items-center">
            <div className="flex-1 border-t border-gray-300"></div>
            <span className="mx-4 text-gray-500 text-sm font-medium">OR</span>
            <div className="flex-1 border-t border-gray-300"></div>
          </div>

          {/* Google Login */}
          <div className="flex justify-center mb-6">
            <GoogleLogin
              onSuccess={async (credentialResponse) => {
                if (credentialResponse.credential) {
                  try {
                    const userData = await loginWithGoogle(
                      credentialResponse.credential
                    );
                    if (userData.role?.toLowerCase() === "admin") {
                      navigate("/admin");
                    } else {
                      navigate("/");
                    }
                  } catch {
                    setError("Google login failed. Please try again.");
                  }
                }
              }}
              onError={() => setError("Google login failed. Please try again.")}
              theme="outline"
              size="large"
              text="signin_with"
              shape="rectangular"
            />
          </div>

          {/* Links */}
          <div className="text-center space-y-3">
            <a
              href="#"
              className="text-orange-600 hover:text-orange-700 text-sm font-medium"
            >
              Forgot Password?
            </a>
            <div className="text-gray-600 text-sm">
              New here?{" "}
              <a
                href="#"
                className="text-orange-600 hover:text-orange-700 font-medium"
              >
                Create Account
              </a>
            </div>
          </div>
        </div>
      </div>

      {/* Footer */}
      <footer className="bg-gray-900 text-white py-8">
        <p className="text-center text-gray-400 text-sm">
          © 2025 NexBuy. All rights reserved.
        </p>
      </footer>
    </div>
  );
}
