<template>
    <div>
        <div class="h-12 bg-white">
            <top-nav-vue></top-nav-vue>
        </div>
        <div style="height: calc(100vh - 48px)" class="flex items-stretch justify-start flex-wrap overflow-hidden">
            <div style="width: 204px;">
                <side-bar-vue></side-bar-vue>
            </div>
            <div style="width: calc(100% - 204px);">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>
<script>
import TopNavVue from '@/components/TopNav.vue';
import SideBarVue from '@/components/SideBar.vue';
import { onBeforeMount } from 'vue';
import authApi from '@/api/authApi';

import { Chart, registerables } from 'chart.js';

Chart.register(...registerables); // Đăng ký các thành phần cần thiết

export default {
    name: 'MainLayout',
    components: { TopNavVue, SideBarVue },
    setup() {
        onBeforeMount(async () => {
            const token = localStorage.getItem('jwt-token') ?? "";
            await authApi.checkSigninedHandleAuth(token);
        })
    },
};
</script>

<style></style>
