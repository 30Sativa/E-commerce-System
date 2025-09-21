import { jwtDecode } from "jwt-decode";

export type JwtPayload = {
  nameid: string;
  email: string;
  role: string;
  exp: number;
  iat: number;
};

export function decodeToken(token: string): JwtPayload {
  return jwtDecode<JwtPayload>(token);
}
