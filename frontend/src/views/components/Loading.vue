<template>
<content-center>
  <v-progress-circular
      :size="70"
      :width="7"
      color="purple"
      indeterminate
  ></v-progress-circular>
</content-center>
</template>

<script>
export default {
  name: "Loading",
  components:{
    ContentCenter: () => import("./ContentCenter"),
  },
};
</script>

<style scoped>

</style>