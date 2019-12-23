<template>
  <div id="app" class="h-100">
    <MainMenu></MainMenu>
    <router-view></router-view>
  </div>
</template>

<script>
import MainMenu from './components/nav/MainMenu.vue'

export default {
  name: 'app',
  data() {
    return {
      centerFormVertically: true
    }
  },
  components: {
    MainMenu
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
