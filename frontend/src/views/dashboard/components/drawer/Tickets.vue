<template>
  <v-list-group :value="true">
    <template v-slot:activator>
      <v-list-item-title>Zgłoszenia</v-list-item-title>
    </template>
    <v-list-item link :to="{ name: 'activetickets' }">
      <v-list-item-title v-text="activeText"></v-list-item-title>
    </v-list-item>
  </v-list-group>
</template>2

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
      countActive: "tickets/getNumberOfActiveTickets",
    }),
    activeText() {
      return `Aktywne (${this.countActive})`;
    },
  },
  methods: {
    ...mapActions({
      addTicket: "tickets/addTicket",
    }),
  },
  created() {
    this.hubConnection = new HubConnectionBuilder().withUrl("/ticket").build();
    this.hubConnection.start().then(() => {
      console.log("ticket connected");
    });
    this.hubConnection.on("newTicket", (data) => {
      this.addTicket(data);
    });
  },
};
</script>
<style scoped></style>
