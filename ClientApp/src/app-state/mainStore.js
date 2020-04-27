import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";

Vue.use(Vuex);

let authorization = {
    state: {
        jwt: "",
        expiration:  new Date(),
        redirect: ""
    },
    mutations: {
        setTokenInfo(state, tokenInfo) {
            state.jwt = tokenInfo.token;
            state.expiration = new Date(tokenInfo.expiration);
        }
    },
    actions: {
        attemptToLogin(context, creds) {
            return new Promise( function (resolve, reject) {
                axios.post("/account/createtoken",creds)
                    .then((res) => {
                        context.commit("setTokenInfo", res.data);
                        resolve();
                    })
                    .catch(() => reject() 
                );
            });
        }
    },
    getters: {
        isAuthenticated(state) {
            return state.jwt && state.expiration > new Date();
        },
        token: (state) => state.jwt,
        redirect: (state) => state.redirect 
    }
}

export default new Vuex.Store({
    modules: {
        auth: authorization
    }
});