<template>
  <div class="text-center">
  <v-menu bottom offset-y>
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="ma-2" v-bind="attrs" v-on="on" icon>
        <v-icon>mdi-account</v-icon>
      </v-btn>
    </template>
    <v-list>
      <v-list-item>
        <v-list-item-content><update-password :user="user"></update-password></v-list-item-content>
      </v-list-item>
      <v-list-item>
        <v-list-item-content><MailSettings :user="user"></MailSettings></v-list-item-content>
      </v-list-item>
    </v-list>
  </v-menu>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import UpdatePassword from "../users/UpdatePassword";
import MailSettings from "../users/MailSettings";
export default {
  name: "UserSettings",
  components: {
    UpdatePassword,
    MailSettings,
  },
  computed: {
    ...mapGetters({
      user: "app/currentUser",
    }),
  },
};
</script>

<style scoped></style>
