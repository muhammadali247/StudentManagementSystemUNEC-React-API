import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

class examTypeService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }
  async getAllExamTypes(token) {
    return await this.getAll("ExamTypes", token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async getExamTypeById(id, token) {
    return await this.getById("ExamTypes", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async createExamType(body, token) {
    return await this.post("ExamTypes", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async deleteExamType(id, token) {
    return await this.delete("ExamTypes", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async updateExamType(id, body, token) {
    return await this.put("ExamTypes", id, body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}

export default examTypeService;