import React, { useEffect, useState } from "react";
import { FaUser, FaShoppingCart, FaSearch } from "react-icons/fa";
import { getProducts } from "../services/productService";

interface Product {
  id: number;
  name: string;
  price: number;
  image: string;
}
interface Category {
  id: number;
  name: string;
}

const HomePage: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [Categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [showDropdown, setShowDropdown] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getProducts();
        setProducts(data);
        setCategories(data);
      } catch (error) {
        console.error("Fail to load products: ", error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  return (
    <div className="font-sans bg-white">
      {/* Header */}
      <header className="fixed top-0 left-0 w-full bg-white shadow z-50">
        <div className="max-w-7xl mx-auto px-6 flex justify-between items-center h-16">
          <h1 className="text-2xl font-bold text-gray-800">NexBuy</h1>

          <nav className="hidden md:flex space-x-6 font-medium">
            <a href="#" className="hover:text-orange-600">
              Home
            </a>
            {/* Categories Dropdown */}
            <div
              className="relative"
              onMouseEnter={() => setShowDropdown(true)}
              onMouseLeave={() => setShowDropdown(false)}
            >
              <button className="hover:text-orange-600">Categories ▾</button>

              {showDropdown && (
                <div className="absolute left-0 mt-2 w-48 bg-white border rounded shadow-lg z-50">
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
                    <p className="px-4 py-2 text-sm text-gray-500">
                      No categories
                    </p>
                  )}
                </div>
              )}
            </div>
            <a href="#" className="hover:text-orange-600">
              Shop
            </a>
            <a href="#" className="hover:text-orange-600">
              Features
            </a>
            <a href="#" className="hover:text-orange-600">
              Blog
            </a>
            <a href="#" className="hover:text-orange-600">
              About
            </a>
            <a href="#" className="hover:text-orange-600">
              Contact
            </a>
          </nav>

          <div className="flex items-center space-x-4 text-gray-600">
            <FaSearch className="cursor-pointer hover:text-orange-600" />
            <FaShoppingCart className="cursor-pointer hover:text-orange-600" />
            <FaUser className="cursor-pointer hover:text-orange-600" />
          </div>
        </div>
      </header>

      {/* Hero Banner */}
      <section className="h-[90vh] bg-gradient-to-r from-orange-500 to-pink-500 flex items-center justify-center text-center mt-16">
        <div>
          <h2 className="text-5xl md:text-6xl font-extrabold text-white mb-4">
            New Season Arrivals
          </h2>
          <p className="text-lg text-white/90 mb-6">Check out all the trends</p>
          <button className="bg-white text-orange-600 px-8 py-3 rounded-lg font-semibold hover:bg-gray-100 transition">
            Shop Now
          </button>
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
                className="bg-white border rounded-xl overflow-hidden shadow hover:shadow-lg transition"
              >
                <div className="relative group">
                  <img
                    src={p.image || "https://via.placeholder.com/400x500"}
                    alt={p.name}
                    className="w-full h-80 object-cover group-hover:scale-105 transition-transform duration-300"
                  />
                  <button className="absolute bottom-4 left-1/2 -translate-x-1/2 bg-orange-500 text-white px-4 py-2 rounded opacity-0 group-hover:opacity-100 transition">
                    Add to Cart
                  </button>
                </div>
                <div className="p-4 text-center">
                  <h3 className="font-medium text-gray-800">{p.name}</h3>
                  <p className="text-orange-600 font-semibold">${p.price}</p>
                </div>
              </div>
            ))}
          </div>
        )}
      </section>

      {/* Footer */}
      <footer className="bg-gray-900 text-white py-12">
        <div className="max-w-7xl mx-auto px-6 grid grid-cols-2 md:grid-cols-4 gap-8">
          <div>
            <h4 className="font-bold mb-4">Categories</h4>
            <ul className="space-y-2 text-sm text-gray-300">
              <li>Women</li>
              <li>Men</li>
              <li>Shoes</li>
              <li>Watches</li>
            </ul>
          </div>
          <div>
            <h4 className="font-bold mb-4">Help</h4>
            <ul className="space-y-2 text-sm text-gray-300">
              <li>Track Order</li>
              <li>Returns</li>
              <li>Shipping</li>
              <li>FAQs</li>
            </ul>
          </div>
          <div>
            <h4 className="font-bold mb-4">Get in Touch</h4>
            <p className="text-sm text-gray-400">
              Any questions? Let us know at support@cozastore.com
            </p>
          </div>
          <div>
            <h4 className="font-bold mb-4">Newsletter</h4>
            <input
              type="email"
              placeholder="email@example.com"
              className="px-4 py-2 rounded w-full mb-2 text-gray-900"
            />
            <button className="w-full bg-orange-500 text-white py-2 rounded-lg hover:bg-orange-600">
              Subscribe
            </button>
          </div>
        </div>
        <p className="text-center text-gray-500 text-sm mt-8">
          © 2025 Cozastore. All rights reserved.
        </p>
      </footer>
    </div>
  );
};

export default HomePage;
