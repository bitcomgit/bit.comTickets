<template>
  <v-app>
    <v-main>

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
