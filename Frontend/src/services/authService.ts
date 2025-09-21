import axios from "axios";

// ✅ Lấy API_URL từ biến môi trường (.env.local)
const API_URL = import.meta.env.VITE_API_URL + "/Auth";

// Create axios instance with default config
const apiClient = axios.create({
  baseURL: API_URL,
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});

export async function loginRequest(email: string, password: string) {
  const res = await apiClient.post("/login", { email, password });
  return res.data;
}

export async function googleLoginRequest(idToken: string) {
  try {
    console.log(
      "📤 Sending idToken to backend:",
      idToken.substring(0, 20) + "..."
    );

    const res = await apiClient.post("/google-login", { idToken });

    console.log("✅ Google login response:", res.data);
    return res.data;
  } catch (error: any) {
    console.error(
      "❌ Google login API error:",
      error.response?.data || error.message
    );
    throw error;
  }
}
