<template>
    <div class="login-container flex items-center justify-center h-[100vh]">
        <div class="login-content flex items-stretch justify-start rounded-lg overflow-hidden">
            <div class="w-[384px]">
                <img class="object-contain" :src="require('@/assets/image/login.png')" alt="voting image">
            </div>
            <div class="flex items-center justify-center bg-white px-8">
                <div class="flex flex-col items-center">
                    <h1 class="text-intro mb-5">Phầm mềm bỏ phiếu online</h1>
                    <div class="relative">
                        <button class="w-80" id="button-login-google" @click="loginWithGoogle"></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { onMounted } from 'vue';

import { useStore } from 'vuex';
import authApi from '@/api/authApi'
import { useRouter } from 'vue-router'

export default {
    name: 'LoginPage',
    components: {},
    setup() {
        const router = useRouter()

        const store = useStore();


        async function handleCredentialResponse(response) {

            const id_token = response.credential;

            store.commit('common/setIsLoading', true);
            const token = await authApi.loginWithGoogle(id_token);

            // // set token backend trả về vào local storage
            localStorage.setItem('jwt-token', token);
            store.commit('common/setIsLoading', false);

            router.push('/');
        }


        onMounted(async () => {
            const token = localStorage.getItem('jwt-token')

            // nếu có token rồi thì redirect về tran chủ
            if (token) {
                try {
                    await authApi.checkSignined();
                    router.push('/');
                    return;
                } catch (ex) {
                    console.log('token cũ');
                }
            }

            // người dùng chưa có token
            window.google.accounts.id.initialize({
                client_id: window.appConfig.googleClientKey,
                callback: handleCredentialResponse
            });

            window.google.accounts.id.renderButton(
                document.getElementById("button-login-google"),
                {}
            );
            window.google.accounts.id.prompt(); // Hiện hộp thoại đăng nhập nếu cần

        })



        return {
        }
    },
};
</script>

<style scoped>
.login-container {
    background-color: #e0f7fa;
    /* Xanh dương rất nhạt */
    background-image: radial-gradient(circle at 50% 50%, rgba(255, 255, 255, 0.5) 10%, rgba(224, 247, 250, 0) 60%);
    background-repeat: no-repeat;
    background-size: cover;
}

.login-content {
    box-shadow: 1px 1px 8px 0 #0000003b
}

.text-intro {
    font-size: 2rem;
    /* Kích thước chữ nhỏ hơn */
    color: #80d8ff;
    /* Màu chữ xanh dương nhạt */
    text-align: center;
    /* Căn giữa chữ */
    text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);
    /* Đổ bóng nhẹ cho chữ */
    padding: 20px;
    /* Khoảng cách trong */
    border-radius: 10px;
    /* Bo góc */
    background-color: rgba(255, 255, 255, 0.7);
    /* Nền trắng trong suốt */
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
    /* Đổ bóng cho toàn bộ */
    transform: rotate(-1deg);
    /* Uốn lượng nhẹ cho chữ */
    transition: transform 0.3s ease;
    /* Hiệu ứng chuyển động khi hover */
}
</style>
