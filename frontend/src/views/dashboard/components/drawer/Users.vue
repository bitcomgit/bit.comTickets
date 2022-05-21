<template>
  <div>
    <v-list-item :to="{ name: target }" link dense>
      <v-list-item-title class="text-center" v-text="title"></v-list-item-title>
    </v-list-item>
    <v-list-item :to="{ name: 'test' }" link dense>
      <v-list-item-title class="text-center">Test</v-list-item-title>
    </v-list-item>
  </div>
</template>

<script>
export default {
  name: "DrawerUsers",
  data: () => ({
    target: "users",
    title: "Użytkownicy",
  }),
};
</script>

<style scoped></style>
