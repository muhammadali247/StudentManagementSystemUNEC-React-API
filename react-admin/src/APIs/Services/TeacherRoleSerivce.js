import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

export class TeacherRoleService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }
  async getAllTeacherRoles(token) {
    return await this.getAll("TeacherRoles", token);
  }
  async getTeacherRoleById(id, token) {
    return await this.getById("TeacherRoles", id, token);
  }
  async createTeacherRole(body, token) {
    return await this.post("TeacherRoles", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }
  async deleteTeacherRole(id, token) {
    return await this.delete("TeacherRoles", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async updateTeacherRole(id, body, token) {
    return await this.put("TeacherRoles", id, body, token);
  }
}