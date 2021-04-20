<template>
  <div id="app">
    <div class="header">
      <img src="./assets/logo.png" class="logo" alt="logo" />
    </div>
    <Login />

    <div class="footer">
      <div class="version">Version 1.0.1</div>
    </div>
  </div>
</template>

<script>
import Login from "./components/Login.vue";

export default {
  name: "App",
  components: {
    Login,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

.logo {
  width: 150px;
  height: 150px;
}
</style>
