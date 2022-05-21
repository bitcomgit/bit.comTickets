import axios from "axios";
import APP_BASE_URL from "./config";

export default {
  getChats() {
    return axios({
      method: "get",
      url: APP_BASE_URL + "api/chats",
    });
  },
  // addMessage(message) {
  //   return axios({
  //     method: "post",
  //     headers: { "Content-Type": "application/json" },
  //     url: APP_BASE_URL + "api/chat",
  //     data: message,
  //   });
  // },
  // setReadMessages(id) {
  //   return axios({
  //     method: "get",
  //     url: APP_BASE_URL + "api/chat/" + id,
  //   });
  // },
  addUserToChat(user) {
    return axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: APP_BASE_URL + "api/chats/adduser",
      data:user,
    });
  },
};
