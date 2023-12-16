import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

export class examService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }

  async getAllExams(token) {
    return await this.getAll("Exams", token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async getExamById(id, token) {
    return await this.getById("Exams", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async getExamByIdForUpdate(id, token) {
    return await this.getById("Exams/update", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async createExam(body, token) {
    return await this.post("Exams", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async deleteExam(id, token) {
    return await this.delete("Exams", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async updateExam(id, body, token) {
    return await this.put("Exams", id, body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}