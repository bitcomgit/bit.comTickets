<template>
  <v-app>
    <dashboard-bar></dashboard-bar>
    <dashboard-drawer></dashboard-drawer>
    <dashboard-view></dashboard-view>
  </v-app>
</template>

<script>
export default {
  name: "Index",
  components: {
    dashboardBar: () => import("./core/AppBar"),
    dashboardDrawer: () => import("./core/Drawer"),
    dashboardView: () => import("./core/View"),
  },
};
</script>

<style scoped></style>
