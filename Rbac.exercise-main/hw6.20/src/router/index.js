import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  {
    path: '/',
    name: '登录',
    component: () => import('../views/Login.vue')
  },
  {
    path: '/home',
    name: 'Home',
    component: () => import('../views/Home.vue'),
    redirect: "/menu",
    children: [
      { path: '/menu', component: () => import('../views/Menu.vue') },
      { path: '/user', component: () => import('../views/User.vue') },
      { path: '/role', component: () => import('../views/Role.vue') }
    ]
  },
  {
    path: '/menulist',
    name: 'menulist',
    component: () => import('../views/MenuList.vue'),
  },
]

const router = new VueRouter({
  routes
})

router.beforeEach((a, b, c) => {
  let username = sessionStorage.getItem("username") ?? ""
  if (a.name != "登录" && username.length == 0) {
    c({ name: "登录" })
  }
  else {
    c()
  }
})

export default router
