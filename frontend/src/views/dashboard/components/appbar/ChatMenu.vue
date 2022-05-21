<template>
  <v-card class="mx-auto" style="height: 80vh" fill-height min-width="200">
    <v-list nav dense>
      <v-list-item
        dense
        v-for="chat in chats"
        :key="chat.id"
        @click="setChat(chat.id)"
        link
      >
        {{ getChatName(chat) }}
<!--        <v-chip-->
<!--          v-if="unread(user.id, chat.id) > 0"-->
<!--          class="ma-2"-->
<!--          color="primary"-->
<!--          outlined-->
<!--          pill-->
<!--          x-small-->
<!--        >-->
<!--          {{ unread(user.id, chat.id) }}-->
<!--        </v-chip>-->
      </v-list-item>
    </v-list>
    <v-divider></v-divider>
  </v-card>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
export default {
  computed: {
    ...mapGetters({
      chats: "chat/getChats",
      user: "app/currentUser",
      //unread: "chat/getNumberOfUnreadMessages",
    }),
  },
  methods: {
    ...mapActions({
      setChatActive: "chat/setActiveChatId",
      //setReadChat: "chat/setReadMessages",
    }),
    setChat(chatId) {
      this.setChatActive(chatId);
      //this.setReadChat({userId: this.user.id, chatId: chatId } )
    },
    getChatName(chat) {
      if (chat.id === 1) return chat.name;
      return (
        chat.members.find((m) => m.id !== this.user.id).lastname +
        " " +
        chat.members.find((m) => m.id !== this.user.id).firstname
      );
    },
  },
};
</script>

<style scoped></style>
