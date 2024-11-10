// api.js
import axios from "axios";
import commonFunction from "@/until/commonFunction";
import store from '@/store'

function initHandleAuth(controllerName) {
    const api = init(controllerName);

    api.interceptors.response.use(
        response => response,
        async error => {
            if (error.response.status === 401) {
                store.commit('common/setIsLoading', false);
                await commonFunction.showDialog('Thông báo', 'Phiên làm việc của bạn đã hết. Vui lòng đăng nhập lại.', [{text: 'Đồng ý', value: true, color: 'primary'}])
                window.location.href = '/login';
            }
        }
    );

    return api;
}

function init(controllerName) {
    // Tạo một instance Axios
    const api = axios.create({
        baseURL: `${window.appConfig.baseUrl}${controllerName? `/${controllerName}`: ''}`, // Thay đổi baseURL theo ứng dụng của bạn
        headers: {
            'Content-Type': 'application/json'
        }
    });

    // Thêm interceptor cho các request
    api.interceptors.request.use(config => {
        const token = localStorage.getItem('jwt-token'); // Lấy token từ local storage
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }

        return config;
    });
    return api;
}

// Xuất instance để sử dụng trong ứng dụng
export {initHandleAuth, init};
