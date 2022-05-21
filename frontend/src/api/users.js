import axios from "axios";
import APP_BASE_URL from "./config";

export default {
  getUsers() {
    return axios({
      method: "get",
      url: APP_BASE_URL + "api/users",
    });
  },
  getEmployees(){
    return axios({
      method: "get",
      url: APP_BASE_URL + "api/users/employees",
    });
  },
  addUser(user) {
    return axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: APP_BASE_URL + "api/users",
      data:user,

    });
  },
  updateUser(user) {
    return axios({
      method: "put",
      headers: { "Content-Type": "application/json" },
      url: APP_BASE_URL + "api/users",
      data: user,
    });
  },
  updatePassword(user) {
    return axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: APP_BASE_URL + "api/users/password",
      data: user,
    });
  },
  updateConfig(config) {
    return axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: APP_BASE_URL + "api/users/config",
      data: config,
    });
  },


};
