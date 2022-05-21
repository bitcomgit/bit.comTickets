<template>
  <v-app>
    <v-main>
      <v-card>
        <v-card-text>
          <v-form ref="form">
            <v-row>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  dense
                  label="Haslo"
                  v-model="passowrd1"
                  type="password"
                  :rules="passwordrules"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  dense
                  v-model="password2"
                  label="Powtórz haslo"
                  type="password"
                  :rules="passwordrules"
                  :error-messages="matchedPasswords"
                ></v-text-field>
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn @click="submit">submit</v-btn>
        </v-card-actions>
      </v-card>
    </v-main>
  </v-app>
</template>

<script>
// import rules from "./rules/rules"
export default {
  name: "Test",
  data: () => ({
    passowrd1: "",
    password2: "",
    passwordrules: [

    ],
  }),
  computed: {
    matchedPasswords() {
      return this.passowrd1 == this.password2 ? "" : "Niezgodne hasła";
    },
  },
  components: {},
  methods: {
    submit() {
      this.setRules();
      setTimeout(() => {
        if (this.$refs.form.validate()) console.log("valid");
        else console.log("invalid");

      }, 100);

    },
    // matchedPasswords: () => { this.password1 === this.password2 ? "" : "Niezgodne hasła"},
    setRules() {
      this.passwordrules = [
        (v) => !!v || "Pole wymagane",
        (v) => v.length > 1 || "Długość hasła",
        // (v) =>
        //   /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}/.test(v) ||
        //   "Za proste hasło",
      ];
    },
  },
};
</script>

<style scoped></style>
