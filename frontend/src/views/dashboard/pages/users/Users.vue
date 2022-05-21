<template>
  <div>
    <users-list></users-list>
  </div>
</template>

<script>
import { mapActions } from "vuex";
export default {
  name: "Users",
  components: {
    UsersList: () => import("../../components/users/UsersList")
  },
  methods: {
    ...mapActions({
      fetchUsers: "users/fetchUsers",
    }),
  },
  created() {
    this.fetchUsers();
  },
};
</script>

<style scoped></style>
