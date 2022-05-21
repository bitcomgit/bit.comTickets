<template>
  <v-card style="height: 80vh" fill-height class="d-flex justify-end">
    <v-card
      style="max-width: 500px; min-width: 500px"
      class="overflow-y-auto mr-1"
    >
      <div ref="chatCard">
        <div ref="banner">
          <v-banner
            id="banner"
            class="justify-center text-h5 font-weight-light"
            sticky
          >
            {{ this.chat != null ? this.getChatName(this.chat) : "" }}
          </v-banner>
        </div>
        <div>
          <v-card-text
            id="chat"
            class="overflow-y-auto"
            v-bind:style="heightSytle"
          >
            <v-alert
              v-for="message in chat.messages"
              :key="message.id"
              outlined
              dense
              color="grey darken-1"
            >
              {{ message.message }}
            </v-alert>
          </v-card-text>
        </div>
        <v-footer id="footer" absolute padless>
          <v-card width="100%" class="d-flex justify-end align-end">
            <v-textarea v-model="message" rows="2" class="ml-1" v-on:keyup.enter="sendMessage"></v-textarea>
            <v-btn icon class="float-right mb-3" @click="sendMessage">
              <v-icon>mdi-send</v-icon>
            </v-btn>
          </v-card>
        </v-footer>
      </div>
    </v-card>
  </v-card>
</template>

<script>
//
// style="max-height: 500px"
//
import { mapGetters } from "vuex";

export default {
  data: function () {
    return {
      message: "",
      maxHeight: "500px",
    };
  },
  props: {
    hubConnection: null,
  },
  computed: {
    heightSytle() {
      return {
        height: `${this.maxHeight}px`,
      };
    },
    ...mapGetters({
      chat: "chat/getActiveChat",
      user: "app/currentUser",
    }),
  },
  methods: {
    getChatName(chat) {
      if (chat.id === 1) return chat.name;
      return (
        chat.members.find((m) => m.id !== this.user.id).lastname +
        " " +
        chat.members.find((m) => m.id !== this.user.id).firstname
      );
    },
    scrollToEnd() {
      let container = this.$el.querySelector("#chat");
      container.scrollTop = container.scrollHeight;
    },
    sendMessage() {
      if (this.message != "")
        this.hubConnection.invoke("SendMessage", this.getMessageObject());
      this.message = "";
    },
    getMessageObject() {
      return {
        authorId: this.user.id,
        chatId: this.chat.id,
        message: this.message,
      };
    },
    setHeight() {
      let footerHeight = this.$el.querySelector("#footer").clientHeight;
      let bannerHeight = this.$el.querySelector("#banner").clientHeight;
      this.maxHeight =
        this.$parent.$el.clientHeight - footerHeight - bannerHeight;
    },
    resizeHendler() {
      this.setHeight();
    },
  },
  updated() {
    this.scrollToEnd();
  },
  mounted: function () {
    setTimeout(
      function () {
        this.$forceUpdate();
        this.setHeight();
      }.bind(this),
      100
    );
  },
  created() {
    window.addEventListener("resize", this.resizeHendler);
  }
};
</script>

<style scoped></style>
