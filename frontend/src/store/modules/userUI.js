export default {
  namespaced: true,
  state: {
    editedUser: {},
    dialog: false,

  },
  getters: {
    dialog: (state) => state.dialog,
    editedUser: (state) => state.editedUser,
  },
  actions: {
    setdialog({ commit }, value) {
      commit("setdialog",value);
      if(!value) commit("resetUser");
    },
    setEditedUser({ commit }, value) {
      commit("setEditedUser",value)
    },
    resetUser({ commit } ) {
      commit("resetUser");
    },
    setDefaults({ commit }) {
      commit("setDefaults");
    },
  },
  mutations: {
    setdialog(state,value){
      state.dialog = value;
    },
    setEditedUser(state,value) {
      state.editedUser = value;
    },
    resetUser(state) {
      state.editedUser.id = undefined;
      state.editedUser.username = "";
      state.editedUser.firstname = "";
      state.editedUser.lastname = "";
      state.editedUser.email = "";
      state.editedUser.password = "";
      state.editedUser.roles = ["employee"];
    },
    setDefaults(state) {
      state.editedUser = {};
      state.dialog = false;
      state.userstable = {}
    },
  },
};
