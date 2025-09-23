import axiosInstance from "./axiosInstance";

export const getProducts = async () => {
  const res = await axiosInstance.get("/Products");
  return res.data.data;
};
