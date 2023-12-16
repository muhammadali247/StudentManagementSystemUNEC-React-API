import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

export class groupSubjectService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }
  async getAllGroupSubjects(token) {
    return await this.getAll("GroupSubjects", token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async getGroupSubjectById(id, token) {
    return await this.getById("GroupSubjects", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async createGroupSubject(body, token) {
    return await this.post("GroupSubjects", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async updateGroupSubject(id, body, token) {
    return await this.put("GroupSubjects", id, body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async deleteGroupSubject(id, token) {
    return await this.delete("GroupSubjects", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}