<template>
  <v-dialog v-model="dialog" persistent max-width="290">
    <template v-slot:activator="{ on, attrs }">
      <v-btn icon v-bind="attrs" v-on="on">
        <v-icon>mdi-logout</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title></v-card-title>
      <v-card-text class="headline">
        Czy chcesz zakończyć pracę z programem?
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="green darken-1" text @click="dialog = false"> Nie </v-btn>
        <v-btn
          color="green darken-1"
          text
          @click="
            dialog = false;
            logout();
          "
        >
          Tak
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapActions} from "vuex";
import userApi from "@/api/app";
export default {
  name: "Logout",
  data: () => ({
    dialog: false,
  }),
  methods: {
    ...mapActions({
      reset: "reset"
    }),
    logout() {
      userApi.logout().then(() => {
        this.$store.dispatch("reset")
        this.$router.push("/login");
      });
    },
  },
};
</script>

<style scoped></style>
