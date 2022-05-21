<template>
  <v-app-bar app elevate-on-scroll height="30">
    <v-spacer></v-spacer>
    <user-settings-menu></user-settings-menu>
    <chat></chat>
    <logout></logout>
  </v-app-bar>
</template>

<script>
import chat from "../components/appbar/Chat";
import logout from "../components/appbar/Logout"
import UserSettingsMenu from "../components/appbar/UserSettingsMenu";
import { mapActions } from "vuex";
export default {
  name: "DashboardCoreAppBar",
  components: {
    logout: logout,
    chat: chat,
    UserSettingsMenu,
  },
  props: {},
  computed: {},
  methods: {
    ...mapActions({
      removeUser: "app/removeUser",
      removeUsers: "users/removeUsers",
    }),
  },
};
</script>
