import Vue from "vue";
import VueRouter from "vue-router";

// 引入组件
import Login from "./components/Login";
import SignUp from "./components/Auth/SignUp"

//import Index from "./components/Index";
import UserManagement from "./components/UserManagement";
import OrderManagement from "./components/OrderManagement";
import Settings from "./components/Settings";
import AccountManagement from "./components/AccountManagement";
import SiderBar from "./components/SiderBar/Index";

Vue.use(VueRouter);

const routes = [
    { path: '/', redirect: 'login/Jwt Login' },
    { path: '/login/:title', name: 'login', component: Login },
    { path: '/signup', name: 'signUp', component: SignUp },

]

export const siderRoutes = [
    {
        name: 'Market Management',
        path: '/UserManagement',
        component: SiderBar,
        children: [
            {
                name: "User Management",
                path: "/UserManagement",
                component: UserManagement
            },
            {
                name: "Order Management",
                path: "/OrderManagement",
                component: OrderManagement
            },
            {
                name: "Account Management",
                path: "/AccountManagement",
                component: AccountManagement
            },
            {
                name: "Settings",
                path: "/Settings",
                component: Settings
            },
        ]
    },
]

const router = new VueRouter({
    mode: 'history',
    routes,
})

router.addRoutes(siderRoutes);

const originalPush = VueRouter.prototype.push

VueRouter.prototype.push = function push(location) {
    return originalPush.call(this, location).catch(err => err)
}


export default router;
