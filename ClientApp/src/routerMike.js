import VueRouter from "vue-router";
import Vue from "vue";
import Login from "./components/Login"
import HelloWorld from "./components/HelloWorld"

Vue.use(VueRouter);

const routes = [
    { path: '/login', component: Login, props: {centerFormVertically: "true"} },
    { path: '/home', component: HelloWorld }

]

let router = new VueRouter({
    routes,
    mode: 'history'
})
export default router