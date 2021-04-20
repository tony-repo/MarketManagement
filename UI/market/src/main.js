import Vue from 'vue';
import App from './App.vue';
import Element from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import axios from 'axios';
import "babel-polyfill";


Vue.config.productionTip = false

axios.defaults.baseURL = "https://localhost:44388"

Vue.use(Element)

new Vue({
  render: h => h(App),
}).$mount('#app')

