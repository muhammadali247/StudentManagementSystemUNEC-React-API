import axiosInstance from "../utils/axiosInstance";
import axios from "axios";

class HTTPClient {
  BaseUrl;
  constructor(BASEURL) {
    this.BaseUrl = BASEURL;
  }
  // async getAll(endPoint, token) {
  //   return await axios.get(`${this.BaseUrl}/${endPoint}`, {
  //     headers: {
  //       Authorization: `Bearer ${token}`,
  //     },
  //   });
  // }
  async getAll(endPoint, token) {
    return await axios.get(`${this.BaseUrl}/${endPoint}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async getById(endPoint, id, token) {
    return await axios.get(`${this.BaseUrl}/${endPoint}/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async get(endPoint, body, token) {
    return await axios.get(`${this.BaseUrl}/${endPoint}`, body, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }
  async post(endPoint, body) {
    return await axios.post(`${this.BaseUrl}/${endPoint}`, body);
  }
  async postLogout(endPoint) {
    return await axios.post(`${this.BaseUrl}/${endPoint}`);
  }
  async put(endPoint, id, body, token) {
    return await axios.put(`${this.BaseUrl}/${endPoint}/${id}`, body);
  }
  async delete(endPoint, id, token) {
    return await axios.delete(`${this.BaseUrl}/${endPoint}/${id}`);
  }
  // async login(endPoint, body) {
  //   return await axiosInstance.post(`${this.BaseUrl}/${endPoint}`, body);
  // }
  async login(endPoint, body) {
    return await axios.post(`${this.BaseUrl}/${endPoint}`, body);
  }
  async resendOTP(endPoint, id, token) {
    return await axios.post(`${this.BaseUrl}/${endPoint}/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }
  async verifyAcc(endPoint, id, body, token) {
    return await axios.post(`${this.BaseUrl}/${endPoint}/${id}`, body, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }
  async ValidateResetPassword(endPoint, body, token) {
    return await axios.get(`${this.BaseUrl}/${endPoint}`, body, token, {
      headers: {
        "Content-Type": "application/json",
      },
    });
  }
}
export default HTTPClient;
