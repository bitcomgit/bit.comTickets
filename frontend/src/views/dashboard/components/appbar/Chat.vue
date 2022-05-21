<template>
  <div>
    <v-menu
      offset-y
      :close-on-content-click="false"
      :close-on-click="false"
      v-model="isOpen"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn v-bind="attrs" v-on="on" icon>
          <v-icon>mdi-message</v-icon>
        </v-btn>
      </template>
      <v-card class="d-flex justify-end">
        <chat-window
          :hubConnection="this.hubConnection"
        ></chat-window>
        <div ref="menu">
          <chat-menu></chat-menu>
        </div>
      </v-card>
    </v-menu>
  </div>
</template>

<script>
import ChatMenu from "./ChatMenu";
import ChatWindow from "./ChatWindow";

import { mapActions, mapGetters } from "vuex";
import { HubConnectionBuilder } from "@microsoft/signalr";
export default {
  components: {
    ChatWindow,
    ChatMenu,
  },
  name: "Chat",
  data: () => ({
    isOpen: false,
    hubConnection: null,
  }),
  computed: {
    ...mapGetters({
      activeChat: "chat/getActiveChat"
    })
  },
  methods: {
    ...mapActions({
      fetchChats: "chat/fetchChats",
      addMessage: "chat/addMessage",
    }),
  },
  watch: {
    isOpen: function() {

    },
  },
  created() {
    this.fetchChats();
    this.hubConnection = new HubConnectionBuilder().withUrl("/chat").build();
    this.hubConnection.start().then(() => {
      console.log("chat connected");
    });
    this.hubConnection.on("newMessage", (data) => {
      this.addMessage(data);
    });

  },
};
</script>

<style scoped></style>
