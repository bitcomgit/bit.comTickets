import Vue from "vue";
import VueRouter from "vue-router";
import store from "../store";
import axios from "axios";
import APP_BASE_URL from "../api/config";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    component: () =>
      import(/* webpackChunkName: "dashboard" */ "../views/dashboard/Index"),
    meta: {
      auth: true,
    },

    children: [
      {
        name: "activetickets",
        path: "pages/activetickets",
        component: () =>
          import(
            /* webpackChunkName: "dashboard/users" */ "../views/dashboard/pages/tickets/ActiveTickets"
          ),
      },
      {
        name: "users",
        path: "pages/users",
        component: () =>
          import(
            /* webpackChunkName: "dashboard/users" */ "../views/dashboard/pages/users/Users"
          ),
      },

    ],
  },
  {
    path: "/initiation",
    name: "Initiation",
    component: () =>
      import(/* webpackChunkName: "dashboard" */ "../views/DataLoad"),
    meta: {
      auth: true,
    },
    props: true,
  },
  {
    path: "/login",
    name: "Login",
    component: () => import(/* webpackChunkName: "Login" */ "../views/Login"),
  },
  {
    path: "/test",
    name: "test",
    component: () => import(/* webpackChunkName: "Login" */ "../views/Test"),
    meta: {
      auth: true,
    },
  },
  {
    path: "/404",
    alias: "*",
    name: "NotFound",
    component: () =>
      import(/* webpackChunkName: "NotFound" */ "../views/NotFound"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach(async (to, from, next) => {
  if (to.matched.some((record) => record.meta.auth)) {
    if (!store.state.app.user) {
      try {
        let userResponse = await axios.get(
          APP_BASE_URL + "api/users/currentuser"
        );
        if (userResponse.data.username != null)
          store.state.app.user = userResponse.data;
        next({ name: "Initiation" });
      } catch {
        next({ name: "Login" });
      }
    }

    if (!store.state.app.user) {
      next({ name: "Login" });
    } else {
      next();
    }
  } else {
    next();
  }
});

export default router;

