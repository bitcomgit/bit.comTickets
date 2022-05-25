<template>
  <forwarded-table
    :items="tickets"
    :tableconfig="ticketActiveTableConfig"
  >
    <template v-slot:tableactions="props">
      <v-icon small class="mr-2" @click="editItem(props.item)"> mdi-pencil </v-icon>
    </template>
  </forwarded-table>
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
      title: "Nowe zgłoszenia",
    },
  }),
  methods: {
    editItem(x) {
      console.log(x)
    },
  },
};
</script>

<style scoped></style>
