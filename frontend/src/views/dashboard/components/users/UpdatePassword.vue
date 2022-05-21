<template>
  <div class="text-center">
    <v-dialog v-model="dialog" width="500" >
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="blue darken-1" text v-bind="attrs" v-on="on">
          Zmień hasło
        </v-btn>
      </template>
      <v-card class="fill-height">
        <v-card-title class="text-h5 grey lighten-2">
          Zmiana hasła {{ user.username }}
        </v-card-title>
        <v-card-text>
          <v-row
            ><v-col cols="12" sm="6" md="4">
              <v-text-field
                v-if="isCurrentUser"
                dense
                label="Stare hasło"
                v-model="user.oldPassword"
                type="password"
                required
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row
            ><v-col cols="12" sm="6" md="4">
              <v-text-field dense label="Nowe hasło" required type="password"></v-text-field>
            </v-col>
          </v-row>
          <v-row
            ><v-col cols="12" sm="6" md="4">
              <v-text-field
                dense
                label="Powtórz hasło"
                v-model="user.password"
                type="password"
                required
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
import usersApi from "../../../../api/users";
import ConfirmSave from "../../../components/ConfirmSave";
export default {
  name: "UpdatePassword",
  components: {
     ConfirmSave,
  },
  data: () => ({
    dialog: false,
    userHelper: null,
  }),
  props: {
    user: null,
  },
  computed: {
    ...mapGetters({
      currentuser: "app/currentUser",
    }),
    isCurrentUser() {
      return this.currentuser.id === this.user.id;
    },
  },
  methods: {
    updateClose() {
      usersApi.updatePassword(this.user).then(() => {
        //this.snackbar = true;
        this.dialog = false;
      });
    },
    close() {
      this.dialog = false;
    },
  },
  created() {
    this.user.password = "";
    this.user.oldPassword = "";
  },
};
</script>

<style scoped></style>
