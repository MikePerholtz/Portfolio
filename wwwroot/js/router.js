import Vue from "vue";
import VueRouter from "vue-router";

import Login from "./login"

Vue.use(VueRouter);

let router = new VueRouter({
    mode: 'history',
    routes: [
        {
            path:"/login",
            component: Login
        }
    ]
})

// router.beforeEach((to, from, next) => {
//     if (to.meta.authRequired) {
        
//     }
//     // to and from are both route objects. must call `next`.
// })

export default router;