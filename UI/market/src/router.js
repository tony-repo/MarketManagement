import Vue from "vue";
import VueRouter from "vue-router";

// 引入组件
import Login from "./components/Login.vue";
import MarketManagement from "./components/MarketManagement.vue";
import UserManagement from "./components/UserManagement.vue";
import OrderManagement from "./components/OrderManagement.vue";
import Settings from "./components/Settings";
import AccountManagement from "./components/AccountManagement";


// 要告诉 vue 使用 vueRouter
Vue.use(VueRouter);

const routes = [
    { path: '/', redirect: 'login/Jwt Login' },
    { path: '/login/:title', name: 'login', component: Login },
    {
        name: 'MarketManagement',
        path: '/MarketManagement',
        component: MarketManagement,
        children: [
            {
                name: "UserManagement",
                path: "/UserManagement",
                component: UserManagement
            },
            {
                name: "OrderManagement",
                path: "/OrderManagement",
                component: OrderManagement
            },
            {
                name: "AccountManagement",
                path: "/AccountManagement",
                component: AccountManagement
            },
            {
                name: "Settings",
                path: "/Settings",
                component: Settings
            },
        ]
    }
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
