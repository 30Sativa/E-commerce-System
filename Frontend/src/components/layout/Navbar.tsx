import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";

export default function Navbar() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  return (
    <header className="fixed top-0 left-0 w-full bg-white shadow z-50">
      <div className="max-w-7xl mx-auto px-6 flex justify-between items-center h-16">
        <h1
          className="text-2xl font-bold text-gray-800 cursor-pointer"
          onClick={() => navigate("/")}
        >
          NexBuy
        </h1>

        <nav className="hidden md:flex space-x-6 font-medium items-center">
          <a href="/" className="hover:text-orange-600">
            Home
          </a>
          <a href="/shop" className="hover:text-orange-600">
            Shop
          </a>
          <a href="/contact" className="hover:text-orange-600">
            Contact
          </a>

          {user ? (
            <div className="flex items-center gap-4 ml-8">
              <span className="text-gray-700 text-sm">
                {user.email ? user.email.split("@")[0] : "User"}
              </span>
              <button
                onClick={() => {
                  logout();
                  navigate("/login");
                }}
                className="bg-red-500 text-white px-3 py-1 rounded-lg text-sm hover:bg-red-600"
              >
                Logout
              </button>
            </div>
          ) : (
            <button
              onClick={() => navigate("/login")}
              className="text-orange-600 hover:text-orange-700 text-sm font-medium"
            >
              Login
            </button>
          )}
        </nav>
      </div>
    </header>
  );
}
