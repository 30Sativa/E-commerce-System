import axiosInstance  from "./axiosInstance";

export const getImagesByProductId = async (productId: number) => {
  const res = await axiosInstance.get(`/ProductImages/product/${productId}`);
  return res.data.data;
}