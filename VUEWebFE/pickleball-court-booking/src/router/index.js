import { createWebHistory, createRouter } from 'vue-router'

import NotFound from '../view/NotFound.vue'
import CourtList from '../view/court/CourtList.vue'
import CourtDetail from '../view/court/CourtDetail.vue'

const routes = [
    {
        path: '/',
        name: 'Home',
        component: CourtList
    },
    {
        path: '/bookings',
        name: 'Bookings',
        component: CourtDetail
    },
    {
        path: '/:catchAll(.*)',
        name: 'NotFound',
        component: NotFound
    },
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router