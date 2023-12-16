import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

export class roleService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }

  async getAllRoles(token) {
    return await this.getAll("Roles", token);
  }
  async getRoleById(id, token) {
    return await this.getById("Roles", id, token);
  }
  async createRole(body, token) {
    return await this.post("Roles", body, token);
  }
  async updateRole(id, body, token) {
    return await this.put("Roles", id, body, token);
  }
  async deleteRole(id, token) {
    return await this.delete("Roles", id, token);
  }
}