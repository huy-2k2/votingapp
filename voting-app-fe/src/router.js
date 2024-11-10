// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from './page/LoginPage.vue';
import MainLayout from './layout/MainLayout.vue';
import VotingPage from './page/VotingPage.vue';
import AddNew from './page/AddNew.vue';
const routes = [
  {
    path: '/',
    name: 'MainLayout',
    component: MainLayout,
    redirect: 'list',
    children: [
      {
        path: 'list',
        name: 'list',
        component: VotingPage,
      },
      {
        path: 'new',
        name: 'new',
        component: AddNew,
      },
    ],
  },
  {
    path: '/login',
    name: 'LoginPage',
    component: LoginPage,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
