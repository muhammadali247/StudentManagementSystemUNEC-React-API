import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

export class authService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }

  async Login(body) {
    return await this.login("Authentication/Login", body);
  }

  async Logout() {
    return await this.postLogout("Authentication/logout");
  }

  // async forgotPassword(body, token) {
  //   return await this.post("Authentication/forgot-password", body, token);
  // }

  async forgotPassword(body) {
    return await this.post("Authentication/forgot-password", body);
  }

  async RenewToken(body) {
    return await this.post("Authentication/renew-token", body);
  }

  // async ResetPassword(body, token) {
  //   return await this.post("Authentication/reset-password", body, token);
  // }

  async ResetPassword(id, body, token) {
    return await this.post(`Authentication/reset-password/${id}`, body, token);
  }

  async validateResetPassword(body, token) {
    return await this.get("Authentication/validate-reset-password-token", body, token);
  }
}