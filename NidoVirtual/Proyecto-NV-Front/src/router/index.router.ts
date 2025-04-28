import { createRouter, createWebHashHistory, createWebHistory } from 'vue-router';
import controlAccesoRoutes from './app/NidoVirtual/nidoVirtualRoutes';
import { useUserStore } from '@/stores/Acceso.store';

const routes = [
  ...controlAccesoRoutes,
  { path: '/:pathMatch(.*)*', name: 'NotFound', component: () => import('@/views/NotFoundView.vue') }
];

export const router = createRouter({
  history: createWebHistory('/'),
  routes
});

router.beforeEach((to, from, next) => {
  const pageTitle = to.meta.title as string;
  if (pageTitle) {
    document.title = pageTitle;
  } else {
    document.title = 'Sistemas-app';
  }
  if (to.matched.some(record => record.meta.requiresAuth)) {
    const authUser = useUserStore();
    const user = authUser.getUser();
    if (!user?.token) {
      next({ name: 'Login' });
    } else {
      next();
    }
  } else {
    next();
  }
});

export default router;