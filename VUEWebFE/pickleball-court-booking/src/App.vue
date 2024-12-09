<template>
  <div class="containter">
    <TheHeader />
    <TheContent />
  </div>
</template>
<script>
//Bước 1: import
import TheHeader from "./components/layout/TheHeader.vue";
import TheContent from "./components/layout/TheContent.vue";
// import TheNav from "./components/layout/TheNav.vue";
export default {
  name: "App",
  components: {
    //Bước 2: register
    TheHeader,
    TheContent,
  },
};
</script>

<style>
/* #app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
} */
@import url("./style/main.css");
</style>
