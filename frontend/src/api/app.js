import axios from "axios";
import APP_BASE_URL from "./config";

export default {
  login(username, password) {
    return axios({
      method: "post",
      url: APP_BASE_URL + "auth/login",
      headers: {
        "Content-Type": "application/json",
      },
      data: {
        username: username,
        password: password,
      },
    });
  },
  logout() {
    return axios({
      method: "get",
      url: APP_BASE_URL + "auth/logout",
    });
  },
  getCurrentUser(){
    return axios({
      method: "get",
      url: APP_BASE_URL + "api/users/currentuser",
    });
  },
  getEmailsStatus(){
    return axios({
      method: "get",
      url: APP_BASE_URL + "api/emails/status",
    });
  },
};
