<template>
  <div>

  <content-center>Dupa {{x}}</content-center>

  </div>
</template>
<script>
export default {
  components: {
    ContentCenter: () => import("./components/ContentCenter"),
    
  },
  data: () => ({
    x: 5,
  }),
  created() {
    setInterval(()=>{
      if( this.x == 0) this.$router.push("/");
      this.x = ( this.x > 0 )? this.x-1 : 0;
    },1000)
  }
};
</script>
