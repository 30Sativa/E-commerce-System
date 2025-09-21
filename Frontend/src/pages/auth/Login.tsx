import { useState } from "react";
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
      await login(email, password);
      navigate("/dashboard"); // sau khi login thường thành công
    } catch (err) {
      setError("Login failed. Please check your email and password.");
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-b from-orange-400 via-red-500 to-pink-500 w-full overflow-x-hidden">
      {/* Header */}
      <header className="bg-white shadow-sm w-full">
        <div className="max-w-full mx-auto px-6 sm:px-8 lg:px-12">
          <div className="flex justify-between items-center h-16">
            <div className="flex items-center">
              <div className="flex-shrink-0 flex items-center">
                <div className="w-8 h-8 bg-gradient-to-r from-orange-500 to-red-500 rounded-lg flex items-center justify-center mr-2">
                  <svg
                    className="w-5 h-5 text-white"
                    fill="currentColor"
                    viewBox="0 0 20 20"
                  >
                    <path
                      fillRule="evenodd"
                      d="M10 2L3 7v11a2 2 0 002 2h10a2 2 0 002-2V7l-7-5zM10 18V8l5-3.5V15a.5.5 0 01-.5.5h-9a.5.5 0 01-.5-.5V4.5L10 8v10z"
                    />
                  </svg>
                </div>
                <h1 className="text-xl font-bold text-gray-900">ShopMart</h1>
              </div>
            </div>
            <div className="hidden md:flex items-center space-x-4">
              <a href="#" className="text-gray-600 hover:text-orange-600">
                Become a Seller
              </a>
              <a href="#" className="text-gray-600 hover:text-orange-600">
                Download
              </a>
              <a href="#" className="text-gray-600 hover:text-orange-600">
                Follow us on
              </a>
            </div>
          </div>
        </div>
      </header>

      {/* Main Content */}
      <div className="flex items-center justify-center min-h-screen py-12 px-6 sm:px-8 lg:px-12 w-full">
        <div className="w-full max-w-2xl mx-auto">
          {/* Login Card */}
          <div
            className="bg-white rounded-3xl shadow-2xl overflow-hidden mx-auto"
            style={{ maxWidth: "600px" }}
          >
            {/* Card Header */}
            <div className="px-16 pt-16 pb-12 text-center">
              <h2 className="text-4xl font-bold text-gray-900 mb-4">Login</h2>
              <p className="text-xl text-gray-600">
                Sign in to access exclusive deals!
              </p>
            </div>

            {/* Login Form */}
            <div className="px-16 pb-16">
              {error && (
                <div className="mb-8 bg-red-50 border border-red-200 rounded-2xl p-6">
                  <div className="flex items-center">
                    <svg
                      className="w-6 h-6 text-red-500 mr-4"
                      fill="currentColor"
                      viewBox="0 0 20 20"
                    >
                      <path
                        fillRule="evenodd"
                        d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z"
                      />
                    </svg>
                    <span className="text-red-700 text-lg">{error}</span>
                  </div>
                </div>
              )}

              <form onSubmit={handleSubmit} className="space-y-8">
                <div>
                  <input
                    type="email"
                    placeholder="Enter your email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    className="w-full px-6 py-5 border border-gray-300 rounded-2xl focus:outline-none focus:ring-3 focus:ring-orange-500 focus:border-orange-500 text-gray-900 placeholder-gray-500 text-xl"
                    autoComplete="email"
                    required
                  />
                </div>

                <div>
                  <input
                    type="password"
                    placeholder="Enter your password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="w-full px-6 py-5 border border-gray-300 rounded-2xl focus:outline-none focus:ring-3 focus:ring-orange-500 focus:border-orange-500 text-gray-900 placeholder-gray-500 text-xl"
                    autoComplete="current-password"
                    required
                  />
                </div>

                <button
                  type="submit"
                  className="w-full bg-gradient-to-r from-orange-500 to-red-500 text-white py-5 px-8 rounded-2xl font-bold text-xl hover:from-orange-600 hover:to-red-600 focus:outline-none focus:ring-4 focus:ring-orange-500 focus:ring-offset-2 transform transition-all duration-200 hover:scale-105 shadow-xl"
                >
                  LOG IN
                </button>
              </form>

              {/* Divider */}
              <div className="my-10 flex items-center">
                <div className="flex-1 border-t border-gray-300"></div>
                <span className="mx-8 text-gray-500 text-lg font-medium">
                  OR
                </span>
                <div className="flex-1 border-t border-gray-300"></div>
              </div>

              {/* Google Login */}
              <div className="flex justify-center w-full">
                <GoogleLogin
                  onSuccess={async (credentialResponse) => {
                    if (credentialResponse.credential) {
                      try {
                        setError(null); // Clear previous errors
                        await loginWithGoogle(credentialResponse.credential);
                        navigate("/dashboard");
                      } catch (err: any) {
                        console.error("Google login error:", err);
                        if (err.response?.status === 409) {
                          setError(
                            "Account already exists. Please try logging in with your password instead."
                          );
                        } else if (err.response?.status === 400) {
                          setError("Invalid Google token. Please try again.");
                        } else {
                          setError("Google login failed. Please try again.");
                        }
                      }
                    }
                  }}
                  onError={() => {
                    setError("Google login failed. Please try again.");
                  }}
                  useOneTap={false}
                  theme="outline"
                  size="large"
                  text="signin_with"
                  shape="rectangular"
                />
              </div>

              {/* Links */}
              <div className="mt-10 text-center space-y-5">
                <a
                  href="#"
                  className="text-orange-600 hover:text-orange-700 text-lg font-semibold"
                >
                  Forgot Password?
                </a>

                <div className="flex items-center justify-center space-x-2 text-lg">
                  <span className="text-gray-600">New to ShopMart?</span>
                  <a
                    href="#"
                    className="text-orange-600 hover:text-orange-700 font-semibold"
                  >
                    Sign up
                  </a>
                </div>
              </div>
            </div>
          </div>

          {/* Additional Links */}
          <div className="mt-12 text-center space-y-4">
            <div className="text-lg text-white/90">
              <a href="#" className="hover:text-white">
                Return to ShopMart.com
              </a>
            </div>
            <div className="flex items-center justify-center space-x-8 text-base text-white/75">
              <a href="#" className="hover:text-white">
                Terms of Service
              </a>
              <span>•</span>
              <a href="#" className="hover:text-white">
                Privacy Policy
              </a>
              <span>•</span>
              <a href="#" className="hover:text-white">
                Help
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
