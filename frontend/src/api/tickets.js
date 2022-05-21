import axios from "axios";
import APP_BASE_URL from "./config";

export default {
  getTickets() {
    return axios({
      method: "get",
      url: APP_BASE_URL + "api/tickets",
    });
  },
  getEmailsInfo(id){
    return axios({
      method: "get",
      url: APP_BASE_URL + `api/tickets/${id}/emails`,
    });
  },
  getActiveTickets(){
    return axios({
      method: "get",
      url: APP_BASE_URL + "api/tickets/active",
    })
  },
  getBodyText(path){
    return axios({
      method: "get",
      url: APP_BASE_URL + path,
    })
  },
  updateTicket(user) {
    return axios({
      method: "put",
      url: APP_BASE_URL + "api/tickets",
      data: user,
    });
  },
};
