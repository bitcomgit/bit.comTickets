import ticketsApi from "@/api/tickets";
export default {
  namespaced: true,
  state: {
    tickets: [],
    activeTickets: [],
  },
  getters: {
    getTickets(state) {
      return state.tickets;
    },
    getActiveTickets(state) {
      return state.activeTickets;
    },
    getProcessedTickets: (state) => (username) => {
      return state.activeTickets
        .filter((t) => t.executor !== null)
        .filter((t) => t.executor.username == username);
    },
    getForwardedTickets: (state) => (username) => {
      return state.activeTickets
        .filter((t) => t.owner.username == username)
        .filter((t) => t.executor !== null)
        .filter((t) => t.executor.username !== username);
    },
    getNewTickets: (state)  => {
      return state.activeTickets
        .filter((t) => t.executor == null);
    },
    getNumberOfActiveTickets(state) {
      return state.activeTickets.length;
    },
    getNumberOfProcessedTickets: (state) => (username) => {
      return state.activeTickets
        .filter((t) => t.executor !== null)
        .filter((t) => t.executor.username == username).length;
    },
    getNumberOfForwardedTickets: (state) => (username) => {
      return state.activeTickets
        .filter((t) => t.owner.username == username)
        .filter((t) => t.executor !== null)
        .filter((t) => t.executor.username !== username)
        .length;
    },
    getNumberOfNewTickets: (state)  => {
      return state.activeTickets
        .filter((t) => t.executor == null)
        .length;
    },
    // getActiveTicketById: (state) => (id) => {
    //   return state.activeTickets.find((t) => t.id === id)
    // },
  },
  actions: {
    fetchTickets({ commit }) {
      ticketsApi.getTickets().then((response) => {
        commit("setTickets", response.data);
      });
    },
    addTicket({ commit }, ticket) {
      commit("addTicket", ticket);
    },
    closeTicket({ commit }, ticket) {
      return new Promise((resolve) => {
        ticketsApi.updateTicket(ticket).then((response) => {
          commit("removeFromActive", response.data);
          resolve();
        });
      });
    },
    fetchActiveTickets({ commit }) {
      ticketsApi.getActiveTickets().then((response) => {
        commit("setActiveTickets", response.data);
      });
    },
    updateTicket({ commit }, ticket) {
      return new Promise((resolve) => {
        ticketsApi.updateTicket(ticket).then((response) => {
          commit("updateTicket", response.data);
          resolve();
        });
      });
    },
    setDefaults({ commit }) {
      commit("setDefaults");
    },
  },
  mutations: {
    addTicket(state, ticket) {
      state.activeTickets.push(ticket);
    },
    setTickets(state, tickets) {
      state.tickets = tickets;
    },
    setActiveTickets(state, tickets) {
      state.activeTickets = tickets;
    },
    updateTicket(state, ticket) {
      let idx = state.activeTickets.findIndex((t) => t.id === ticket.id);
      state.activeTickets[idx] = ticket;
    },
    removeFromActive(state, ticket) {
      const i = state.activeTickets.map((item) => item.id).indexOf(ticket.id);
      state.activeTickets.splice(i, 1);
    },
    setDefaults(state) {
      state.tickets = [];
      state.activeTickets = [];
    },
  },
};
