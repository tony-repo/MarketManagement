import Vue from "vue";
import VueRouter from "vue-router";

// 引入组件
import Login from "./components/Login.vue";
import HelloWorld from "./components/HelloWorld.vue";


// 要告诉 vue 使用 vueRouter
Vue.use(VueRouter);

const routes = [
    { path: '/', redirect: '/login' },
    { path: '/login', name: 'login', component: Login },
    { path: '/hello', name: 'hello', component: HelloWorld }
]

const router = new VueRouter({
    mode: 'history',
    routes
})

//获取原型对象上的push函数
const originalPush = VueRouter.prototype.push
//修改原型对象中的push方法
VueRouter.prototype.push = function push(location) {
    return originalPush.call(this, location).catch(err => err)
}

export default router;
