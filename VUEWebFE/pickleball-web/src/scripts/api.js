import axios from 'axios';
import store from '@/store'; // Import Vuex store

// Khai báo base URL
const api = axios.create({
    baseURL: 'http://localhost:5039',
    timeout: 10000, // Thời gian timeout cho mỗi request
});


// Thêm interceptor để thêm token vào mỗi request
api.interceptors.request.use(
    (config) => {
        const token = store.getters['getToken']; // Sử dụng getter để lấy token
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Thêm interceptor để luôn trả về data
api.interceptors.response.use(
    (response) => {
        // Luôn trả về response.data
        return response.data;
    },
    (error) => {
        // Nếu lỗi vẫn có response, trả về phần data từ response
        if (error.response) {
            return Promise.resolve(error.response.data);
        }
        // Nếu không có response (lỗi kết nối, timeout), ném lỗi
        return Promise.reject(error);
    }
);

// Hàm gọi API GET
export const getData = (endpoint, params = {}) => {
    return api.get(endpoint, { params });
};

// Hàm gọi API POST
export const postData = (endpoint, data) => {
    return api.post(endpoint, data);
};

// Hàm gọi API PUT
export const putData = (endpoint, data) => {
    return api.put(endpoint, data);
};

// Hàm gọi API DELETE
export const deleteData = (endpoint) => {
    return api.delete(endpoint);
};

// Hàm gọi API POST với multipart/form-data
export const postDataMultipart = (endpoint, formData) => {
    return api.post(endpoint, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};