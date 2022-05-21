import Vue from "vue";
import Vuex from "vuex";
import app from "./modules/app";
import users from "./modules/users";
import chat from "./modules/chat";
import tickets from "./modules/tickets";
import contractors from "./modules/contractors";
import userUI from "./modules/userUI";
Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    app,
    users,
    chat,
    tickets,
    contractors,
    userUI,
  },
  state: {},
  actions: {
    reset({ commit }) {
      commit("reset");
    },
  },
  mutations: {
    reset(modules) {
      Object.keys(modules).forEach((moduleName) => {
        this.dispatch(`${moduleName}/setDefaults`);
      });
    },
  },
});
