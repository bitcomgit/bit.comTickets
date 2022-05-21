<template>
  <crud-data-table :items="users" :tableconfig="userstable">
    <template v-slot:lolbaractions><user-edit></user-edit></template>
    <template v-slot:tableactions="props">
      <v-icon small class="mr-2" @click="editItem(props.item)"> mdi-pencil </v-icon>
    </template>
  </crud-data-table>
</template>

<script>
import { mapGetters } from "vuex";
import UserEdit from "./UserEdit";
import CrudDataTable from "../../../components/CrudDataTable";

export default {
  name: "UsersList",
  components: {
    UserEdit: UserEdit,
    CrudDataTable: CrudDataTable
  },
  data: () => ({
    userstable: {
      headers: [
        {
          text: "Login",
          align: "start",
          sortable: false,
          value: "username",
        },
        { text: "email", sortable: false, value: "email" },
        { text: "firstname", sortable: false, value: "firstname" },
        { text: "Actions", value: "actions", sortable: false, align: "end" },
      ],
      title: "Użytkownicy",
    },
  }),
  computed: {
    ...mapGetters({
      users: "users/getUsers",
    })
  },
  methods: {
    editItem(item) {
      this.$store.dispatch("userUI/setEditedUser", {... item });
      this.$store.dispatch("userUI/setdialog", true);
    }
  },
}

</script>

<style scoped></style>
