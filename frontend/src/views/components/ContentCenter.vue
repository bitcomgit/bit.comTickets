<template>
  <div>
    <v-row justify="center">
      <div
          class="d-flex align-center mb-6"
          style="height: 100vh" fill-height
          flat
          tile
          height="100%"
      >
       <slot></slot>
      </div>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "ContentCenter"
};
</script>

<style scoped>

</style>