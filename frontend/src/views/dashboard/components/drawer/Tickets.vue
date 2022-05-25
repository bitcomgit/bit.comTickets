<template>
  <v-list-group :value="true">
    <template v-slot:activator>
      <v-list-item-title>Zgłoszenia</v-list-item-title>
    </template>
    <v-list-item link :to="{ name: 'processedtickets' }">
      <v-list-item-title v-text="processedText"></v-list-item-title>
    </v-list-item>
    <v-list-item link :to="{ name: 'forwardedtickets' }">
      <v-list-item-title v-text="forwardedText"></v-list-item-title>
    </v-list-item>
    <v-list-item link :to="{ name: 'newtickets' }">
      <v-list-item-title v-text="newText"></v-list-item-title>
    </v-list-item>
  </v-list-group>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import { HubConnectionBuilder } from "@microsoft/signalr";

export default {
  name: "DrawerTickets",
  props: {},
  data: () => ({
    hubConnection: null,
  }),
  computed: {
    ...mapGetters({
      user: "app/currentUser",
      processed: "tickets/getNumberOfProcessedTickets",
      forwarded: "tickets/getNumberOfForwardedTickets",
      new: "tickets/getNumberOfNewTickets"
    }),
    processedText() {
      let txt = this.processed(this.user.username) > 0 ? `(${this.processed(this.user.username)})`:"";
      return `Przetwarzane ${txt} `;
    },
    forwardedText() {
      let txt = this.forwarded(this.user.username) > 0 ? `(${this.forwarded(this.user.username)})`:"";
      return `Przekazane ${txt}`;
    },
    newText() {
      let txt = this.new > 0 ? `(${this.new})`:"";
      return `Nowe ${txt}`;
    },
  },
  methods: {
    ...mapActions({
      addTicket: "tickets/addTicket",
    }),
  },
  created() {
    this.hubConnection = new HubConnectionBuilder().withUrl("/ticket").build();
    this.hubConnection.start().then(() => {});
    this.hubConnection.on("newTicket", (data) => {
      this.addTicket(data);
    });
  },
};
</script>
<style scoped></style>
