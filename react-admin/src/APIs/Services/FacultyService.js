import BASE_URL from "../../constants/URLs"
import HTTPClient from "../HTTPClient";

class facultyService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }
  async getAllFaculties(token) {
    return await this.getAll("Faculties", token);
  }
  async getFacultyById(id, token) {
    return await this.getById("Faculties", id, token);
  }
  async getFacultyByIdForUpdate(id, token) {
    return await this.getById("Faculties/update", id, token);
  }
  async createFaculty(body, token) {
    return await this.post("Faculties", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }
  // async createFaculty(body) {
  //   return await this.post("Faculties", body);
  // }
  // async createFaculty(body) {
  //   return await this.post("Faculties", body);
  // }
  async deleteFaculty(id, token) {
    return await this.delete("Faculties", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  async updateFaculty(id, body, token) {
    return await this.put("Faculties", id, body, token);
  }
}
export default facultyService;
