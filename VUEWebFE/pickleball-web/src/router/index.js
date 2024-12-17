import { createRouter, createWebHistory } from 'vue-router';
import HomeSearch from '../views/search/HomeSearch.vue';
import SearchResult from '../views/search/SearchResult.vue';
import MainLayout from '../views/layouts/MainLayout.vue';
import EmptyLayout from '../views/layouts/EmptyLayout.vue';
import CourtClusterDetail from '../views/court/CourtClusterDetail.vue';
import NotFound from '../views/NotFound.vue';

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
        name: "Login",
        // component: LoginPage,
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

export default router
