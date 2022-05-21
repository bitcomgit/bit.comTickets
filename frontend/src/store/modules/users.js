import usersApi from "@/api/users";
export default {
  namespaced: true,
  state: {
    users: [],
    employees: [],
  },
  getters: {
    getUsers: (state) => state.users,
    getEmployees: (state) => state.employees,
    getUserByEmail: (state) => (email) => {
      return state.users.find((user) => user.email === email);
    },
    isUser: (state) => (username)  => { return state.users.find((u) => u.username === username ) !== undefined }
  },
  actions: {
    fetchUsers({ commit }) {
      usersApi.getUsers().then((response) => {
        commit("setUsers", response.data);
      });
    },
    fetchEmployees({ commit }) {
      usersApi.getEmployees().then((response) => {
        commit("setEmployees", response.data);
      });
    },
    addUser({ commit }, user) {
      return new Promise((resolve) => {
        usersApi.addUser(user).then((response) => {
          commit("pushUserToState", response.data);
          resolve();
        });
      });
    },
    updateUser({ commit }, user) {
      return new Promise((resolve) => {
        usersApi.updateUser(user).then((response) => {
          commit("updateUser", response.data);
          resolve();
        });
      });
    },
    setDefaults({ commit }) {
      commit("setDefaults");
    },
  },
  mutations: {
    setUsers(state, users) {
      state.users = users;
    },
    setEmployees(state, employees) {
      state.employees = employees;
    },
    pushUserToState(state, user) {
      state.users.push(user);
    },
    updateUser(state, user) {
      const idx = state.users.findIndex((u) => u.id === user.id);
      state.users.splice(idx, 1, user);
    },
      setDefaults(state) {
      state.users = [];
      state.employees = [];
    },
  },
};
