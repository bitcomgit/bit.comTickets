<template>
  <v-main>
    <v-card class="mx-auto my-12" max-width="500">
      <v-card-title>bit.com</v-card-title>
      <v-container>
        <v-form ref="form">
          <v-text-field
            v-model="name"
            label="Użytkownik"
            required
          ></v-text-field>

          <v-text-field
            v-model="password"
            append-icon="mdi-eye"
            :type="show ? 'text' : 'password'"
            :error-messages="isValidForm()"
            label="Hasło"
            @mousedown="show = true"
            @click:append="show = false"
          ></v-text-field>
        </v-form>
      </v-container>
      <v-card-actions class="d-flex flex-row-reverse">
        <v-btn color="deep-purple lighten-2" text v-on:click="submit()">
          Login
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-main>
</template>
<script>
import app from "../api/app";
import { mapActions } from "vuex";

export default {
  name: "Login",
  components: {

  },
  methods: {
    ...mapActions({
      fetchUser: "app/fetchUser",
    }),
    isValidForm: function () {
      return this.isValid ? "" : "Nieprawidłowy login lub hasło";
    },
    submit: function () {
      app
        .login(this.name, this.password)
        .then(() => {
          this.fetchUser().then(() => {
            this.$router.push("/initiation").catch(() => {});
          });
        })
        .catch(() => {
          this.isValid = false;
        });
    },
  },
  data: () => ({
    name: "",
    password: "",
    show: false,
    isValid: true,
  }),
};
</script>

<style scoped></style>
