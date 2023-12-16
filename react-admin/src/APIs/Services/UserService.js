import BASE_URL from "../../constants/URLs"
import axiosInstance from "../../utils/axiosInstance";
import HTTPClient from "../HTTPClient";

export class userService extends HTTPClient {
  constructor() {
    super(BASE_URL);
  }

  async getAllUsers(token) {
    return await this.get("Accounts", token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  async getUnassignedUsers(token) {
    return await this.get("Accounts/unassigned-users", token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  // async getAllUsers(token) {
  //   return await this.get("Accounts", token);
  // }

  // async getUserById(id, token) {
  //   return await this.getById(`Accounts/${id}`, id, token);
  // }

  async getUserById(id, token) {
    return await this.get(`Accounts/${id}`, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  // async createUser(body, token) {
  //   return await this.post("Accounts", body, token);
  // }

  // async createStudentUser(body, token) {
  //   return await this.post("Accounts/create-student-user", body, token, {
  //     headers: {
  //       Authorization: `Bearer ${token}`,
  //       "Content-Type": "application/json",
  //     },
  //   });
  // }
  async createStudentUser(body) {
    return await this.post("Accounts/create-student-user", body, {
      headers: {
        "Content-Type": "application/json",
      },
    });
  }

  async createUser(body, token) {
    return await this.post("Accounts/create-user", body, token, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }

  // async createUser(body, token) {
  //   // Set the Content-Type to application/json
  //   const headers = {
  //     Authorization: `Bearer ${token}`,
  //     "Content-Type": "application/json",
  //   };

  //   // Stringify the body object to JSON
  //   const requestBody = JSON.stringify(body);

  //   return await this.post("Accounts/create-user", requestBody, token, { headers });
  // }

  // async createUser(body, token) {
  //   // Convert FormData to plain JavaScript object
  //   const data = {};
  //   body.forEach((value, key) => {
  //     data[key] = value;
  //   });
  
  //   return await this.post("Accounts/create-user", data, token);
  // }

  async updateUser(id, body, token) {
    return await this.put(`Accounts`, id, body, token);
  }
  // async updateUser(id, body, token) {
  //   return await this.put("Accounts", id, body, token);
  // }

  async deleteUser(id, token) {
    return await this.delete("Accounts", id, token, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  // async deleteUser(id, token) {
  //   return await this.delete(`Accounts/${id}`, token);
  // }

  // async resendOTP(id, token) {
  //   return await this.resendOTP("Accounts/resend-otp", id, token);
  // }

  async resendOTPRequest(id, token) {
    return await this.post(`Accounts/resend-otp/${id}`, null, token);
  }

  // async verifyAccount(id, body, token) {
  //   return await this.verifyAcc("Accounts/verify-account", id, body, token);
  // }

  async verifyAccountRequest(id, body, token) {
    return await this.post(`Accounts/verify-account/${id}`, body, token);
  }
}

// import BASE_URL from "../../constants/URLs"
// import axiosInstance from "../../utils/axiosInstance";
// import HTTPClient from "../HTTPClient";

// export class userService extends HTTPClient {
//   constructor() {
//     super(BASE_URL);
//   }

//   async createUser(body, token) {
//     // Set the Content-Type to application/json
//     const headers = {
//       Authorization: `Bearer ${token}`,
//       "Content-Type": "application/json",
//     };

//     return await this.post("Accounts/create-user", body, token, { headers });
//   }
// }