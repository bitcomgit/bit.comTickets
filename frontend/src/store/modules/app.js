import app from "@/api/app";
export default {
  namespaced: true,
  state: {
    user: null,
    emailsStatus: null,
  },

  getters: {
    currentUser(state) {
      return state.user;
    },
    hasRole: (state) => (role) => {
      return state.user.roles.indexOf(role) >= 0;
    },
  },

  actions: {
    fetchUser ({ commit }) {
      return new Promise((resolve) => {
        app.getCurrentUser().then((response) => {
          commit("setUser", response.data);
        });
        resolve();
      });
    },
    fetchEmailsStatus ({ commit }) {
      return new Promise((resolve) => {
        app.getEmailsStatus().then((response) => {
          commit("setEmailsStatus", response.data);
        });
        resolve();
      });
    },
    setDefaults({ commit }) {
      commit("setDefaults");
    },
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    setEmailsStatus(state,status) {
      state.emailsStatus = status;
    },
    setDefaults(state) {
      state.user = null;
    },
  },
};
