import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

export class LessonTypeService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }
  async getAllLessonTypes(token) {
    return await this.getAll("LessonTypes", token);
  }
  async getLessonTypeById(id, token) {
    return await this.getById("LessonTypes", id, token);
  }
  async createLessonType(body, token) {
    return await this.post("LessonTypes", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }
  async deleteLessonType(id, token) {
    return await this.delete("LessonTypes", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async updateLessonType(id, body, token) {
    return await this.put("LessonTypes", id, body, token);
  }
}