import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

class teacherService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }

  async getAllTeachers(token) {
    return await this.getAll("Teachers", token);
  }
  async getTeacherById(id, token) {
    return await this.getById("Teachers", id, token);
  }
  async createTeacher(body, token) {
    return await this.post("Teachers", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "multipart/form-data",
      },
    });
  }
  async deleteTeacher(id, token) {
    return await this.delete("Teachers", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async updateTeacher(id, body, token) {
    return await this.put("Teachers", id, body, token);
  }
}

export default teacherService;