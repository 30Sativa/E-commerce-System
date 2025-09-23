import axios from "axios";

// Lấy baseURL từ .env.local
const API_URL = import.meta.env.VITE_API_URL as string;

const axiosInstance = axios.create({
  baseURL: API_URL,
  withCredentials: true, // nếu backend có cookie auth
});

// Interceptor: tự động thêm token (nếu có)
axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default axiosInstance;
