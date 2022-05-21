<template>
<v-app>
  <loading></loading>
</v-app>
</template>

<script>
import { mapActions } from "vuex";
export default {
  name: "DataLoad",
  data: () => ({
    toRoute: "/",
  }),
  components: {
    Loading: () => import("./components/Loading"),

  },
  methods: {
    ...mapActions({
      fetchActiveTickets: "tickets/fetchActiveTickets",
      fetchEmployees: "users/fetchEmployees",
      fetchContractors: "contractors/fetchContractors",
      fetchEmailsStatus: "app/fetchEmailsStatus",
    }),
  },
  created() {
    // this.fetchEmailsStatus();
    this.fetchActiveTickets();
    this.fetchEmployees();
    this.fetchContractors();

    setTimeout(() => {
      this.$router.push(this.toRoute);
    }, 1500);
  },
};
</script>

<style scoped></style>
