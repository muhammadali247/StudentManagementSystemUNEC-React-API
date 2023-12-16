import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

export class studentService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }
  async getAllStudents(token) {
    return await this.getAll("Students", token);
  }
  async getStudentById(id, token) {
    return await this.getById(`Students`, id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  // async getUserById(id, token) {
  //   return await this.get(`Accounts/${id}`, token, {
  //     headers: {
  //       Authorization: `Bearer ${token}`,
  //     },
  //   });
  // }
  async createStudent(body, token) {
    return await this.post("Students", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "multipart/form-data",
      },
    });
  }
  async updateStudent(id, body, token) {
    return await this.put("Students", id, body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "multipart/form-data",
      },
    });
  }
  async deleteStudent(id, token) {
    return await this.delete("Students", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}