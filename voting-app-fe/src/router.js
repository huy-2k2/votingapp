// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from './page/LoginPage.vue';
import MainLayout from './layout/MainLayout.vue';
import VotingPage from './page/VotingPage.vue';
import AddNew from './page/AddNew.vue';
import SubmitPage from './page/SubmitPage.vue';


const routes = [
  {
    path: '/',
    name: 'root',
    redirect: '/app',
  },
  {
    path: '/app',
    name: 'MainLayout',
    component: MainLayout,
    redirect: '/app/list',
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
  { 
    path: '/submit/:id',
    name: 'SubmitPage',
    component: SubmitPage,
  }
];

const router = createRouter({
  history: createWebHistory("/"),
  routes,
});

export default router;
