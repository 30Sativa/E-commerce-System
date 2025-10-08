import React, { useEffect, useRef, useState } from "react";
import {
  FaUser,
  FaShoppingCart,
  FaSearch,
  FaMobileAlt,
  FaLaptop,
  FaHeadphones,
  FaMouse,
  FaKeyboard,
  FaVolumeUp,
  FaUsb,
  FaBatteryFull,
  FaPlug,
  FaShieldAlt,
  FaFacebookF,
  FaInstagram,
  FaTwitter,
  FaYoutube,
} from "react-icons/fa";
import { getProducts } from "../services/productService";
import { getCategories } from "../services/categoryService";
import { useNavigate } from "react-router";
import { useAuth } from "../context/AuthContext";


interface Product {
  id: number;
  name: string;
  price: number;
  imageUrl?: string;
}
interface Category {
  id: number;
  name: string;
}

const HomePage: React.FC = () => {
  const navigator = useNavigate();
  const { user, logout } = useAuth();
  const [products, setProducts] = useState<Product[]>([]);
  const [Categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [showCategoryDropdown, setShowCategoryDropdown] = useState(false);
  const [showUserDropdown, setShowUserDropdown] = useState(false);
  const categoryDropdownHideTimer = useRef<number | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [productData, categoryData] = await Promise.all([
          getProducts(),
          getCategories()
        ]);
        setProducts(productData);
        setCategories(categoryData);
      } catch (error) {
        console.error("Fail to load data:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  return (
    <div className="font-sans bg-white min-h-screen flex flex-col">
      {/* Header */}
      <header className="sticky top-0 left-0 w-full bg-white shadow z-50">
        <div className="max-w-7xl mx-auto px-6 flex justify-between items-center h-16">
          <h1
            className="text-2xl font-bold text-orange-600 cursor-pointer tracking-tight"
            onClick={() => navigator("/")}
          >
            NexBuy
          </h1>
          <nav className="hidden md:flex space-x-8 font-medium items-center">
            <a href="#" className="hover:text-orange-600">Home</a>
            {/* Categories Dropdown */}
            <div
              className="relative"
              onMouseEnter={() => {
                if (categoryDropdownHideTimer.current) {
                  window.clearTimeout(categoryDropdownHideTimer.current);
                  categoryDropdownHideTimer.current = null;
                }
                setShowCategoryDropdown(true);
              }}
              onMouseLeave={() => {
                if (categoryDropdownHideTimer.current) {
                  window.clearTimeout(categoryDropdownHideTimer.current);
                }
                categoryDropdownHideTimer.current = window.setTimeout(() => {
                  setShowCategoryDropdown(false);
                }, 200);
              }}
            >
              <button className="hover:text-orange-600">Categories ▾</button>
              {showCategoryDropdown && (
                <div
                  className="absolute left-0 mt-2 w-48 bg-white border rounded shadow-lg z-50"
                  onMouseEnter={() => {
                    if (categoryDropdownHideTimer.current) {
                      window.clearTimeout(categoryDropdownHideTimer.current);
                      categoryDropdownHideTimer.current = null;
                    }
                    setShowCategoryDropdown(true);
                  }}
                  onMouseLeave={() => {
                    if (categoryDropdownHideTimer.current) {
                      window.clearTimeout(categoryDropdownHideTimer.current);
                    }
                    categoryDropdownHideTimer.current = window.setTimeout(() => {
                      setShowCategoryDropdown(false);
                    }, 200);
                  }}
                >
                  {Categories.length > 0 ? (
                    Categories.map((cat) => (
                      <a
                        key={cat.id}
                        href={`/category/${cat.id}`}
                        className="block px-4 py-2 text-sm text-gray-700 hover:bg-orange-100"
                      >
                        {cat.name}
                      </a>
                    ))
                  ) : (
                    <p className="px-4 py-2 text-sm text-gray-500">No categories</p>
                  )}
                </div>
              )}
            </div>
            <a href="#" className="hover:text-orange-600">Shop</a>
            <a href="#" className="hover:text-orange-600">Blog</a>
            <a href="#" className="hover:text-orange-600">Contact</a>
          </nav>
          <div className="flex items-center space-x-4 text-gray-600">
            <FaSearch size={18} className="cursor-pointer hover:text-orange-600" />
            <FaShoppingCart size={18} className="cursor-pointer hover:text-orange-600" />
            {user ? (
              <div className="relative">
                <button
                  onClick={() => setShowUserDropdown((prev) => !prev)}
                  className="flex items-center gap-2 text-sm text-gray-800 font-medium hover:text-orange-600 focus:outline-none"
                >
                  <FaUser size={18} className="text-orange-500" />
                  <span>{user.email ? user.email.split("@")[0] : "User"}</span>
                  <svg
                    className={`w-4 h-4 transform transition-transform ${
                      showUserDropdown ? "rotate-180" : ""
                    }`}
                    fill="none"
                    stroke="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth="2"
                      d="M19 9l-7 7-7-7"
                    />
                  </svg>
                </button>

                {showUserDropdown && (
                  <div className="absolute right-0 mt-2 w-40 bg-white border rounded-lg shadow-lg py-2 z-50">
                    <button
                      onClick={() => {
                        navigator("/profile");
                        setShowUserDropdown(false);
                      }}
                      className="block w-full text-left px-4 py-2 text-sm hover:bg-gray-100"
                    >
                      👤 Profile
                    </button>

                    <button
                      onClick={() => {
                        navigator("/orders");
                        setShowUserDropdown(false);
                      }}
                      className="block w-full text-left px-4 py-2 text-sm hover:bg-gray-100"
                    >
                      🛒 My Orders
                    </button>

                    <button
                      onClick={() => {
                        logout();
                        navigator("/login");
                      }}
                      className="block w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-red-50"
                    >
                      🚪 Logout
                    </button>
                  </div>
                )}
              </div>
            ) : (
              <FaUser
                size={18}
                className="cursor-pointer hover:text-orange-600"
                onClick={() => navigator("/login")}
              />
            )}

          </div>
        </div>
      </header>

      {/* Hero Section */}
      <section className="relative flex flex-col md:flex-row items-center justify-between max-w-7xl mx-auto px-6 py-16 mt-8 gap-8">
        <div className="flex-1 text-center md:text-left">
          <h2 className="text-5xl md:text-6xl font-extrabold text-gray-900 mb-4 leading-tight">
            Discover Your Next Favorite Product
          </h2>
          <p className="text-lg text-gray-600 mb-8">
            Shop the latest trends and exclusive deals only at NexBuy.
          </p>
          <button className="bg-orange-500 text-white px-8 py-3 rounded-lg font-semibold hover:bg-orange-600 transition">
            Shop Now
          </button>
        </div>
        <div className="flex-1 flex justify-center">
          <img
            src="https://images.unsplash.com/photo-1518770660439-4636190af475?auto=format&fit=crop&w=900&q=80"
            alt="Electronics showcase"
            className="rounded-3xl shadow-2xl w-full max-w-md object-cover"
          />
        </div>
      </section>

      {/* Category Highlights (dùng API) */}
      <section className="max-w-7xl mx-auto px-6 py-10">
        <h3 className="text-2xl font-bold text-gray-900 mb-8 text-center">Popular Categories</h3>
        <div className="grid grid-cols-2 md:grid-cols-4 gap-6">
          {Categories.length > 0 ? (
            Categories.map((cat) => {
              const name = cat.name.toLowerCase();
              const getIcon = () => {
                if (name.includes("điện thoại") || name.includes("dien thoai") || name.includes("phone")) return <FaMobileAlt size={36} className="text-orange-600"/>;
                if (name.includes("laptop")) return <FaLaptop size={36} className="text-orange-600"/>;
                if (name.includes("phụ kiện") || name.includes("phu kien") || name.includes("accessory") || name.includes("accessories")) return <FaHeadphones size={36} className="text-orange-600"/>;
                if (name.includes("chuột") || name.includes("mouse")) return <FaMouse size={36} className="text-orange-600"/>;
                if (name.includes("bàn phím") || name.includes("keyboard")) return <FaKeyboard size={36} className="text-orange-600"/>;
                if (name.includes("loa") || name.includes("speaker")) return <FaVolumeUp size={36} className="text-orange-600"/>;
                if (name.includes("usb")) return <FaUsb size={36} className="text-orange-600"/>;
                if (name.includes("pin") || name.includes("battery")) return <FaBatteryFull size={36} className="text-orange-600"/>;
                if (name.includes("sạc") || name.includes("charger")) return <FaPlug size={36} className="text-orange-600"/>;
                if (name.includes("ốp") || name.includes("case") || name.includes("dán") || name.includes("screen")) return <FaShieldAlt size={36} className="text-orange-600"/>;
                return <FaHeadphones size={36} className="text-orange-600"/>; // default accessory icon
              };

              return (
                <div
                  key={cat.id}
                  className="bg-gradient-to-br from-orange-100 to-pink-100 rounded-xl p-6 flex flex-col items-center shadow hover:scale-105 transition cursor-pointer"
                  onClick={() => navigator(`/category/${cat.id}`)}
                >
                  <div className="w-16 h-16 mb-3 flex items-center justify-center">
                    {getIcon()}
                  </div>
                  <span className="font-semibold text-gray-800 text-center break-words">{cat.name}</span>
                </div>
              );
            })
          ) : (
            <p className="col-span-4 text-center text-gray-500">No categories found.</p>
          )}
        </div>
      </section>

      {/* Featured Products */}
      <section className="max-w-7xl mx-auto px-6 py-16">
        <h2 className="text-3xl font-bold text-gray-900 text-center mb-12">
          Featured Products
        </h2>
        {loading ? (
          <p className="text-center text-gray-600">Đang tải sản phẩm...</p>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
            {products.map((p) => (
              <div
                key={p.id}
                className="bg-white border rounded-2xl overflow-hidden shadow-lg hover:shadow-2xl hover:-translate-y-2 transition-all group flex flex-col"
              >
                <div className="relative group flex-1 flex items-center justify-center bg-gray-50">
                  <img
                    src={p.imageUrl || 'https://via.placeholder.com/400x500'}
                    alt={p.name}
                    className="w-full h-64 object-cover group-hover:scale-105 transition-transform duration-300"
                  />
                  <button className="absolute bottom-4 left-1/2 -translate-x-1/2 bg-orange-500 text-white px-4 py-2 rounded opacity-0 group-hover:opacity-100 transition">
                    Add to Cart
                  </button>
                </div>
                <div className="p-4 text-center">
                  <h3 className="font-medium text-gray-800 text-lg mb-1">{p.name}</h3>
                  <p className="text-orange-600 font-semibold text-xl">${p.price}</p>
                </div>
              </div>
            ))}
          </div>
        )}
      </section>

      {/* Testimonials (static, gán cứng) */}
      <section className="bg-gradient-to-r from-orange-50 to-pink-50 py-16">
        <div className="max-w-4xl mx-auto px-6">
          <h3 className="text-2xl font-bold text-gray-900 mb-8 text-center">What Our Customers Say</h3>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            {/* NOTE: Gán cứng, sau này thay bằng API testimonials */}
            <div className="bg-white rounded-xl shadow p-6 flex flex-col items-center">
              <img src="https://randomuser.me/api/portraits/men/32.jpg" className="w-16 h-16 rounded-full mb-3" alt="user" />
              <p className="text-gray-700 italic mb-2">"Great service and fast delivery!"</p>
              <span className="font-semibold text-orange-600">Nam Nguyen</span>
            </div>
            <div className="bg-white rounded-xl shadow p-6 flex flex-col items-center">
              <img src="https://randomuser.me/api/portraits/women/44.jpg" className="w-16 h-16 rounded-full mb-3" alt="user" />
              <p className="text-gray-700 italic mb-2">"Products are high quality and affordable."</p>
              <span className="font-semibold text-orange-600">Linh Tran</span>
            </div>
            <div className="bg-white rounded-xl shadow p-6 flex flex-col items-center">
              <img src="https://randomuser.me/api/portraits/men/65.jpg" className="w-16 h-16 rounded-full mb-3" alt="user" />
              <p className="text-gray-700 italic mb-2">"I love shopping here every month!"</p>
              <span className="font-semibold text-orange-600">Hieu Le</span>
            </div>
          </div>
        </div>
      </section>

      {/* Newsletter */}
      <section className="bg-white py-12 border-t">
        <div className="max-w-2xl mx-auto px-6 text-center">
          <h4 className="text-2xl font-bold mb-4">Subscribe to our Newsletter</h4>
          <p className="text-gray-600 mb-6">Get the latest updates and offers.</p>
          <div className="flex flex-col sm:flex-row gap-3 justify-center">
            <input
              type="email"
              placeholder="email@example.com"
              className="px-4 py-2 rounded w-full sm:w-auto text-gray-900 border"
            />
            <button className="bg-orange-500 text-white px-6 py-2 rounded-lg hover:bg-orange-600">
              Subscribe
            </button>
          </div>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-gray-900 text-white py-10 mt-auto">
        <div className="max-w-7xl mx-auto px-6 grid grid-cols-2 md:grid-cols-4 gap-8">
          <div>
            <h4 className="font-bold mb-4">Shop Electronics</h4>
            <ul className="space-y-2 text-sm text-gray-300">
              <li><a href="#" className="hover:text-orange-500">Smartphones</a></li>
              <li><a href="#" className="hover:text-orange-500">Laptops</a></li>
              <li><a href="#" className="hover:text-orange-500">Audio & Headphones</a></li>
              <li><a href="#" className="hover:text-orange-500">Accessories</a></li>
            </ul>
          </div>
          <div>
            <h4 className="font-bold mb-4">Support</h4>
            <ul className="space-y-2 text-sm text-gray-300">
              <li><a href="#" className="hover:text-orange-500">Warranty & Repairs</a></li>
              <li><a href="#" className="hover:text-orange-500">Returns & Refunds</a></li>
              <li><a href="#" className="hover:text-orange-500">Shipping & Delivery</a></li>
              <li><a href="#" className="hover:text-orange-500">FAQs</a></li>
            </ul>
          </div>
          <div>
            <h4 className="font-bold mb-4">Contact</h4>
            <p className="text-sm text-gray-400">support@nexbuy.com</p>
            <p className="text-sm text-gray-400">Mon–Fri: 9:00–18:00</p>
          </div>
          <div>
            <h4 className="font-bold mb-4">Follow Us</h4>
            <div className="flex items-center gap-4">
              <a href="#" aria-label="Facebook" className="text-gray-300 hover:text-orange-500"><FaFacebookF /></a>
              <a href="#" aria-label="Instagram" className="text-gray-300 hover:text-orange-500"><FaInstagram /></a>
              <a href="#" aria-label="Twitter" className="text-gray-300 hover:text-orange-500"><FaTwitter /></a>
              <a href="#" aria-label="YouTube" className="text-gray-300 hover:text-orange-500"><FaYoutube /></a>
            </div>
          </div>
        </div>
        <p className="text-center text-gray-500 text-sm mt-8">© 2025 NexBuy. All rights reserved.</p>
      </footer>
    </div>
  );
}

export default HomePage;
