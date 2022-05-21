import axios from "axios";
import APP_BASE_URL from "./config";

export default {
  getTicketOrders(id) {
    return axios({
      method: "get",
      url: APP_BASE_URL + `api/tickets/${id}/orders`,
    });
  },
  addOrder(order) {
    return axios({
      method: "post",
      url: APP_BASE_URL + "api/orders",
      data: order,
    });
  },
};
