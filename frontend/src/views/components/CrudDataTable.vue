<template>
  <v-data-table
    :headers="tableconfig.headers"
    :items="items"
    :hide-default-footer="true"
    :items-per-page="-1"
    sort-by="calories"
    class="elevation-1"
    dense
  >
    <template v-slot:top>
      <v-toolbar flat dense>
        <v-toolbar-title>{{ tableconfig.title }}</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <slot name="lolbaractions"> Fallback Title </slot>
      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
      <slot name="tableactions" :item="item">Fallback actions</slot>
    </template>
  </v-data-table>
</template>
<script>
export default {
  name: "CrudDataTable",
  props: {
    tableconfig: null,
    items: null,
  },
};
</script>
<style scoped></style>
