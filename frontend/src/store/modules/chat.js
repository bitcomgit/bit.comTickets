import chatApi from "@/api/chat";
export default {
  namespaced: true,
  state: {
    chats: [],
    activeChatId: 1,
  },
  getters: {
    getChats(state) {
      return state.chats;
    },
    getActiveChat(state) {
      return state.chats.find((c) => c.id === state.activeChatId);
    },
    // getNumberOfUnreadMessages: (state) => (userId, chatId) => {
    //   let chat = state.chats.find((c) => c.id === chatId);
    //   if (chatId === 1) {
    //     if (localStorage.bcMessages != null)
    //       return chat.messages.length - localStorage.bcMessages;
    //     return chat.messages.length;
    //   }
    //
    //   let unread = chat.messages.filter(
    //     (m) => m.authorId != userId && m.isReaded == false
    //   );
    //   return unread.length;
    // },
    // getTotalUnreadMessages: (state) => (userId) => {
    //
    //   let bcChat = state.chats.find((c) => c.id === 1);
    //   let len = state.chats.length;
    //   for (let i = 0; i < len; i++) {
    //
    //   }
    //   },
  },
  actions: {
    fetchChats({ commit }) {
      chatApi.getChats().then((response) => {
        commit("setChats", response.data);
      });
    },
    setActiveChatId({ commit }, id) {
      commit("setActiveChatId", id);
    },
    addMessage({ commit }, message) {
      commit("addMessage", message);
    },
    setDefaults({ commit }) {
      commit("setDefaults");
    },
  },
  mutations: {
    setChats(state, chats) {
      state.chats = chats;
    },
    setActiveChatId(state, id) {
      state.activeChatId = id;
    },
    addMessage(state, message) {
      let chat = state.chats.find((c) => c.id === message.chatId);
      chat.messages.push(message);
    },
    setRead(state, payload) {
      let chat = state.chats.find((c) => c.id === payload.chatId);
      let len = chat.messages.length;
      for (let i = 0; i < len; i++) {
        if (chat.messages[i].authorId != payload.userId)
          chat.messages[i].isReaded = true;
      }
    },
    setDefaults(state) {
      state.chats = [];
      state.activeChatId = 1;
    },
  },
};
