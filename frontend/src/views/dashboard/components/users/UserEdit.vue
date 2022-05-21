<template>
  <v-dialog v-model="dialog" max-width="700px">
    <template v-slot:activator="{ on, attrs }">
      <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on" x-small>
        Dodaj
      </v-btn>
    </template>
    <v-card>
      <v-card-title>
        <span class="text-h5">title</span>
      </v-card-title>
      <v-card-text>
        <v-form ref="form">
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  dense
                  label="Login"
                  required
                  :error-messages="isLoginInUse"
                  :disabled="!isNewUser"
                  v-model="user.username"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  dense
                  label="Nazwisko"
                  hint="Nazwisko"
                  v-model="user.lastname"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  dense
                  label="Imię"
                  hint=""
                  v-model="user.firstname"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12">
                <v-text-field
                  dense
                  label="Email*"
                  v-model="user.email"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row v-if="isNewUser">
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  dense
                  label="Haslo"
                  v-model="password"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  dense
                  label="Powtórz haslo"
                  type="password"
                  v-model="user.password"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-checkbox
                dense
                label="Admin"
                value="admin"
                v-model="user.roles"
              ></v-checkbox>
              <v-checkbox
                dense
                label="Dyrektor"
                value="director"
                v-model="user.roles"
              ></v-checkbox>
              <v-checkbox
                dense
                label="Pracownik"
                value="employee"
                v-model="user.roles"
              ></v-checkbox>
            </v-row>
          </v-container>
        </v-form>
      </v-card-text>

      <v-card-actions>
        <update-password v-if="!isNewUser" :user="user"></update-password>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="close"> Anuluj </v-btn>
        <ConfirmSave @save-confirmed="save"></ConfirmSave>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import UpdatePassword from "./UpdatePassword";
import ConfirmSave from "../../../components/ConfirmSave";
export default {
  name: "UserEdit",
  components: {
    UpdatePassword,
    ConfirmSave,
  },
  data: () => ({
    password: "",
  }),
  computed: {
    ...mapGetters({
      user: "userUI/editedUser",
      isUser: "users/isUser",
    }),
    dialog: {
      get() {
        return this.$store.getters["userUI/dialog"];
      },
      set(value) {
        this.$store.dispatch("userUI/setdialog", value);
      },
    },
    isNewUser() {
      return this.user.id === undefined;
    },
    isLoginInUse() {
      return this.isUser(this.user.username) && this.isNewUser ? "Zajęty login" : "";
    }
  },
  methods: {
    ...mapActions({
      update: "users/updateUser",
      create: "users/addUser",
    }),
    close() {
      this.$store.dispatch("userUI/setdialog", false);
    },
    save() {
      if(this.isNewUser){
        this.create(this.user)
      }
      else {
        this.update(this.user);
      }
      this.$store.dispatch("userUI/setdialog", false);
    },

  },
};
</script>

<style scoped></style>
