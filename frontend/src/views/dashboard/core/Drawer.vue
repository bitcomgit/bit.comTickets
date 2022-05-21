<template>
  <v-navigation-drawer app>
    <v-list nav dense>
      <v-list-item  dense>
        <v-list-item-title class="text-center" dense>bit.com</v-list-item-title>
      </v-list-item>
      <drawer-tickets></drawer-tickets>
      <drawer-users v-if="hasRole('admin')"></drawer-users>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
//import DrawerUsersVue from '../components/drawer/DrawerUsers.vue';
import { mapActions, mapGetters } from "vuex";
export default {
  name: "DashboardCoreDrawer",
  components: {
    // DrawerMails: () => import("../components/drawer/DrawerMails"),
    DrawerTickets: () => import("../components/drawer/Tickets"),
    // DrawerOrders: () => import("../components/drawer/DrawerOrders"),
    // DrawerTasks: () => import("../components/drawer/DrawerTasks"),
    DrawerUsers: () => import("../components/drawer/Users"),
  },

  props: {},
  data: () => ({}),
  computed: {
    ...mapGetters({
      hasRole: "app/hasRole",
    }),
  },
  methods: {
    ...mapActions({}),
  },
  created() {},
};
</script>
