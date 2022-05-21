import contractorsApi from "@/api/contractors";

export default {
  namespaced: true,
  state: {
    contractors: [],
  },
  getters: {
    getContractors(state) {
      return state.contractors;
    },
  },
  actions: {
    fetchContractors({ commit }) {
      contractorsApi.getContractors().then((response) => {
        commit("setContractors", response.data);
      });
    },
    setDefaults({ commit }) {
      commit("setDefaults");
    },

  },
  mutations: {
    setContractors(state, contractors) {
      state.contractors = contractors;
    },
    setDefaults(state) {
      state.contractors = [];
    },
  },
};
