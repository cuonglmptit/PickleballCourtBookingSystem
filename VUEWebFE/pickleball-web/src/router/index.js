import { createRouter, createWebHistory } from 'vue-router';
import HomeSearch from '../views/search/HomeSearch.vue';
import SearchResult from '../views/search/SearchResult.vue';
import MainLayout from '../views/layouts/MainLayout.vue';
import EmptyLayout from '../views/layouts/EmptyLayout.vue';
import CourtClusterDetail from '../views/court/CourtClusterDetail.vue';
import CourtOwnerManage from '../views/owner/CourtOwnerManage.vue';
import CourtOwnerBookings from '../views/owner/CourtOwnerBookings.vue';
import CourtOwnerDashboard from '../views/owner/CourtOwnerDashboard.vue';
import NotFound from '../views/NotFound.vue';
import RegisterView from '../views/auth/RegisterView.vue';
import LoginView from '../views/auth/LoginView.vue';
import CustomerBookings from '@/views/customer/CustomerBookings.vue';
import OwnerCourtClusterDetail from '@/views/owner/OwnerCourtClusterDetail.vue';
import AdminManage from '@/views/admin/AdminView.vue';

import store from '@/store';  // Import store

const routes = [
  {
    path: '/',
    component: MainLayout,
    children: [
      {
        path: "",
        name: "home",
        component: HomeSearch,
        meta: { requiresAuth: false, allowedRoles: ['Customer', undefined], title: 'Đặt sân Pickleball' },
      },
      {
        path: '/customer/bookings',
        name: 'customer-bookings',
        component: CustomerBookings,
        meta: { requiresAuth: true, allowedRoles: ['Customer'], title: 'Lịch đặt' },
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
      {
        path: '/manage/court-cluster',
        name: 'manage-court-cluster',
        component: CourtOwnerManage,
        meta: { requiresAuth: true, allowedRoles: ['CourtOwner'], title: 'Quản lý sân' },
      },
      {
        path: '/manage/court-cluster/:id',
        name: 'manage-court-cluster-detail',
        component: OwnerCourtClusterDetail,
        meta: { requiresAuth: true, allowedRoles: ['CourtOwner'], title: 'Quản lý sân' },
      },
      {
        path: '/manage/bookings',
        name: 'owner-manage-booking',
        component: CourtOwnerBookings,
        meta: { requiresAuth: true, allowedRoles: ['CourtOwner'], title: 'Quản lý booking' },
      },
      {
        path: '/dashboard',
        name: 'owner-dashboard',
        component: CourtOwnerDashboard,
        meta: { requiresAuth: true, allowedRoles: ['CourtOwner'], title: 'Thống kê' },
      },
      {
        path: '/admin',
        name: 'admin',
        component: AdminManage,
        meta: { requiresAuth: true, allowedRoles: ['Admin'], title: 'Admin' },
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

router.beforeEach((to, from, next) => {
  const { isAuthenticated, getUser: user } = store.getters;
  // Nếu người dùng đã đăng nhập và cố gắng vào trang login hoặc trang đăng ký, chuyển hướng về trang chủ
  if ((to.name === 'login' || to.name === 'register') && isAuthenticated) {
    return next({ name: 'home' });  // Hoặc bạn có thể điều hướng đến bất kỳ trang nào khác bạn muốn
  }

  // Nếu route yêu cầu auth và người dùng chưa đăng nhập
  if (to.meta.requiresAuth && !isAuthenticated) {
    return next({ name: 'login' });
  }

  // Nếu route yêu cầu quyền người dùng và role của người dùng không có trong allowedRoles
  if (to.meta.allowedRoles && !to.meta.allowedRoles.includes(user?.role)) {
    if (user?.role === 'CourtOwner') {
      return next({ name: 'owner-manage-booking' })
    }
    if (user?.role === 'Admin') {
      return next({ name: 'admin' })
    }
    return next({ name: 'home' });  // Hoặc điều hướng tới trang khác
  }

  next();  // Cho phép truy cập route
});

router.afterEach((to) => {
  document.title = to.meta.title || "PickleBall Booking";
});

export default router
