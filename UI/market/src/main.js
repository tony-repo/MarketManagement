import Vue from 'vue';
import App from './App.vue';
import Element from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import axios from 'axios';
import "babel-polyfill";

import router from "./router.js";
import Vuex from 'vuex';


Vue.use(Element)
Vue.use(Vuex)

Vue.config.productionTip = false

// Vuex store
const store = new Vuex.Store({
  state: {
    token: ''
  },
  mutations: {
    set_token(state, token) {
      state.token = token
    },

    del_token(state) {
      state.token = ''
    }
  }
});


// axios
//axios.defaults.baseURL = "https://marketmanagement:8088"
axios.defaults.baseURL = "https://localhost:49155"
Vue.prototype.$axios = axios

axios.interceptors.request.use(config => {
  if (store.state.token) {
    config.headers.common['Authorization'] = "Bearer " + store.state.token;
  }

  return config;
}, error => {
  return Promise.reject(error);
});

axios.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    if (error.response) {
      switch (error.response.status) {
        case 401:
          this.$store.commit('del_token');
          router.replace({
            path: '/login',
            query: { redirect: router.currentRoute.fullPath }// jump up after login successfully
          })
          break;
        default:
          break;
      }
      return Promise.reject(error.response.data)
    }
    return Promise.reject(error)
  });



new Vue({
  render: h => h(App),
  router,
  store: store
}).$mount('#app')

