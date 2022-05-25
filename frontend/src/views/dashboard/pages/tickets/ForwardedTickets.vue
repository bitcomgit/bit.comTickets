<template>
  <forwarded-table :items="items" :tableconfig="ticketActiveTableConfig"></forwarded-table>
</template>

<script>
import CrudDataTable from "../../../components/CrudDataTable";
import { mapGetters } from "vuex";
export default {
  name: "ForwardedTickets",
  components: {
    ForwardedTable: CrudDataTable,
  },
  computed: {
    ...mapGetters({
      user: "app/currentUser",
      tickets: "tickets/getForwardedTickets",
    }),
    items() {
      return this.tickets(this.user.username);
    },
  },
  data: () => ({
    ticketActiveTableConfig: {
      headers: [
        {
          text: "Tytuł",
          align: "start",
          sortable: false,
          value: "title",
        },
        {
          text: "Kontrahent",
          sortable: false,
          value: "contractor",
        },
        { text: "Odpowiedzialny", value: "executor.username", sortable: false },
        { text: "Numer", value: "number", sortable: false },
        {
          text: "Od",
          sortable: false,
          value: "emails[0].from",
        },
        { text: "Actions", value: "actions", sortable: false, align: "end" },
      ],
      title: "Przekazane zgłoszenia",
    },
  }),
};
</script>

<style scoped></style>
