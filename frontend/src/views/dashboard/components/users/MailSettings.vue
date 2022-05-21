<template>
  <div class="text-center">
    <v-dialog v-model="dialog" width="500">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="blue darken-1" text v-bind="attrs" v-on="on">
          Ustawienia poczty
        </v-btn>
      </template>
      <v-card class="fill-height">
        <v-card-title class="text-h5 grey lighten-2">
          Ustawienia Poczty{{ user.username }}
        </v-card-title>
        <v-card-text>
          <v-row
            ><v-col cols="12" sm="6" md="4">
              <v-text-field
                dense
                label="Login"
                v-model="u.euh"
                required
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row
            ><v-col cols="12" sm="6" md="4">
              <v-text-field
                dense
                label="Hasło"
                v-model="u.eph"
                required
                type="password"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="close"> anuluj </v-btn>
          <confirm-save @save-confirmed="updateClose"></confirm-save>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import usersApi from "@/api/users";
import ConfirmSave from "../../../components/ConfirmSave";
export default {
  name: "MailSettings",
  components: {
    ConfirmSave,
  },
  data: () => ({
    dialog: false,
    u: {
      euh: "",
      eph: "",
    }
  }),
  computed: {
    ...mapGetters({
      user: "app/currentUser",
    })
  },
  methods: {
    close() {
      this.dialog = false;
    },
    updateClose() {
      usersApi.updateConfig(this.u).then( () => { this.dialog = false;}).catch();

    },
  },
};
</script>

<style scoped></style>
