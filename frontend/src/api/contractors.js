import axios from "axios";
import APP_BASE_URL from "./config";


export default {
  getContractors() {
    return axios({
      method: "get",
      url: APP_BASE_URL + "api/contractors",
    });
  },
}