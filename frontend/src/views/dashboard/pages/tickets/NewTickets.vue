<template>
  <forwarded-table
    :items="tickets"
    :tableconfig="ticketActiveTableConfig"
  ></forwarded-table>
</template>

<script>
import CrudDataTable from "../../../components/CrudDataTable";
import { mapGetters } from "vuex";
export default {
  name: "NewTickets",
  components: {
    ForwardedTable: CrudDataTable,
  },
  computed: {
    ...mapGetters({
      user: "app/currentUser",
      tickets: "tickets/getNewTickets",
    }),
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
      ],
      title: "Nowe zgłoszenia",
    },
  }),
};
</script>

<style scoped></style>
