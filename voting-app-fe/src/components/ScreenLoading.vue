<template>
    <div v-show="isLoading" class="screen-loading">
        <div class="spinner"></div>
    </div>
</template>

<script>
import { computed } from 'vue';
import { useStore } from 'vuex';

export default {
    name: 'ScreenLoading',
    components: {},
    setup() {
        const store = useStore();
        const isLoading = computed(() => store.getters['common/isLoading']);

        return {
            isLoading
        }
    },
};
</script>

<style scoped>
.screen-loading {
    position: fixed;
    inset: 0;
    z-index: 99999999;
    background-color: rgba(58, 177, 246, 0.17);
    display: flex;
    align-items: center;
    justify-content: center;
}

.spinner {
    width: 40px;
    height: 40px;
    border: 4px solid rgba(0, 0, 0, 0.1);
    /* Màu nền của vòng tròn */
    border-top-color: #3498db;
    /* Màu của phần quay */
    border-radius: 50%;
    animation: spin 1s linear infinite;
    /* Animation quay liên tục */
}

@keyframes spin {
    to {
        transform: rotate(360deg);
        /* Quay 360 độ */
    }
}
</style>
