import Vue from 'vue'
import App from './App.vue'
import router from './routerMike'
import BootstrapVue from 'bootstrap-vue'
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-vue/dist/bootstrap-vue.css"
import store from './state_store/appState'
///


Vue.config.productionTip = false
Vue.use(BootstrapVue)

new Vue({
  render: h => h(App),
  store,
  router,
  //mixins: [theMixin]
}).$mount('#app')
