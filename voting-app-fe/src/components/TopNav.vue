<template>
  <div class="topnav">
    <div class="flex items-center justify-start">
      <div class="w-12 mt-[-4px]">
        <img :src="require('@/assets/image/logo.png')" alt="">
      </div>
      <h1 class="font-medium text-[24px]">Voting App</h1>
    </div>
    <div class="flex items-center">
      <v-btn @click="handleLogout" color="secondary">ĐĂNG XUẤT</v-btn>
    </div>
  </div>
</template>
<script>

import { useRouter } from 'vue-router';
export default {
  name: 'App',
  components: {},
  setup() {
    const router = useRouter();
    function handleLogout() {
      localStorage.removeItem('jwt-token');
      router.push('/login');
    }

    return {
      handleLogout
    }

  },
};
</script>

<style scoped>
.topnav {
  height: 100%;
  border-bottom: 1px solid #cccccc9d;
  padding: 0 12px;
  display: flex;
  align-items: stretch;
  justify-content: space-between;
}
</style>
