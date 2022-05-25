<template>
  <processed-table
    :tableconfig="ticketActiveTableConfig"
    :items="items"
  ></processed-table>
</template>

<script>
import CrudDataTable from "../../../components/CrudDataTable";
import { mapGetters } from "vuex";
export default {
  name: "ProcessedTickets",
  computed: {
    ...mapGetters({
      user: "app/currentUser",
      tickets: "tickets/getProcessedTickets",
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
        { text: "Numer", value: "number", sortable: false },
        {
          text: "Od",
          sortable: false,
          value: "emails[0].from",
        },
        { text: "Actions", value: "actions", sortable: false, align: "end" },
      ],
      title: "Przetważane zgłoszenia",
    },
  }),
  components: {
    ProcessedTable: CrudDataTable,
  },
};
</script>

<style scoped></style>
