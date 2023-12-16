import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

export class SubjectService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }
  async getAllSubjects(token) {
    return await this.getAll("Subjects", token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async getSubjectById(id, token) {
    return await this.getById("Subjects", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async createSubject(body, token) {
    return await this.post("Subjects", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async updateSubject(id, body, token) {
    return await this.put("Subjects", id, body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async deleteSubject(id, token) {
    return await this.delete("Subjects", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}