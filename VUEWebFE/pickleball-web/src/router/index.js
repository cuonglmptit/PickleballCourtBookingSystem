import { createRouter, createWebHistory } from 'vue-router';
import HomeSearch from '../views/search/HomeSearch.vue';
import SearchResult from '../views/search/SearchResult.vue';
import MainLayout from '../views/layouts/MainLayout.vue';
import EmptyLayout from '../views/layouts/EmptyLayout.vue';
import CourtClusterDetail from '../views/court/CourtClusterDetail.vue';
import NotFound from '../views/NotFound.vue';
import RegisterView from '../views/auth/RegisterView.vue';
import LoginView from '../views/auth/LoginView.vue';

const routes = [
  {
    path: '/',
    component: MainLayout,
    children: [
      {
        path: "",
        name: "home",
        component: HomeSearch,
      },
      {
        path: '/search/result',
        name: 'search-result',
        component: SearchResult
      },
      {
        path: '/court-cluster/:id',
        name: 'court-cluster-detail',
        component: CourtClusterDetail
      },
    ],
  },

  {
    path: "/login",
    component: EmptyLayout, // Layout tối giản
    children: [
      {
        path: "",
        name: "login",
        component: LoginView,
        meta: { title: "Đăng Nhập" },
      },
    ],
  },
  {
    path: "/register",
    component: EmptyLayout, // Layout tối giản
    children: [
      {
        path: "",
        name: "register",
        component: RegisterView,
        meta: { title: "Đăng Ký" },
      },
    ],
  },
  {
    path: '/:catchAll(.*)',
    name: 'NotFound',
    component: NotFound
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.afterEach((to) => {
  document.title = to.meta.title || "PickleBall Booking";
});

export default router
