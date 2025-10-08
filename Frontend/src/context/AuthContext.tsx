import { createContext, useContext, useState, useEffect } from "react";
import type { ReactNode } from "react";
import { loginRequest, googleLoginRequest } from "../services/authService";
import { decodeToken, type JwtPayload } from "../utils/jwt";

type AuthContextType = {
  user: JwtPayload | null;
  token: string | null;
  login: (email: string, password: string) => Promise<JwtPayload>;
  loginWithGoogle: (idToken: string) => Promise<JwtPayload>;
  logout: () => void;
};

const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<JwtPayload | null>(null);
  const [token, setToken] = useState<string | null>(null);

  // Khi reload: lấy token từ localStorage và decode lại
  useEffect(() => {
    const savedToken = localStorage.getItem("token");
    if (savedToken) {
      try {
        const decoded = decodeToken(savedToken);
        if (decoded.exp * 1000 > Date.now()) {
          setToken(savedToken);
          setUser(decoded);
        } else {
          localStorage.removeItem("token"); // token hết hạn
        }
      } catch (err) {
        console.error("Invalid token in storage:", err);
        localStorage.removeItem("token");
      }
    }
  }, []);

  // Login thường
  async function login(email: string, password: string) {
    try {
      const res = await loginRequest(email, password);

      if (!res.success || !res.data?.token) {
        throw new Error("Login failed: no token returned");
      }

      const token = res.data.token;
      const decoded = decodeToken(token);

      setUser(decoded);
      setToken(token);

      localStorage.setItem("token", token);

      return decoded;
    } catch (err) {
      console.error("Login error:", err);
      throw err;
    }
  }

  // Login với Google
  async function loginWithGoogle(idToken: string) {
    try {
      console.log("Attempting Google login with token:", idToken.substring(0, 20) + "...");
      const res = await googleLoginRequest(idToken);

      if (!res.success || !res.data?.token) {
        console.error("Google login response:", res);
        throw new Error("Google login failed: no token returned");
      }

      const token = res.data.token;
      const decoded = decodeToken(token);

      setUser(decoded);
      setToken(token);

      localStorage.setItem("token", token);
      console.log("Google login successful");
      return decoded;
    } catch (err) {
      console.error("Google login error:", err);
      throw err;
    }
  }

  function logout() {
    setUser(null);
    setToken(null);
    localStorage.removeItem("token");
  }

  return (
    <AuthContext.Provider
      value={{ user, token, login, loginWithGoogle, logout }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = () => useContext(AuthContext);
