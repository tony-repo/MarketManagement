import Vue from 'vue';
import App from './App.vue';
import Element from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import axios from 'axios';
import "babel-polyfill";

import router from "./router.js";


Vue.config.productionTip = false

axios.defaults.baseURL = "https://marketmanagement:8088"

Vue.use(Element)


new Vue({
  render: h => h(App),
  router
}).$mount('#app')

