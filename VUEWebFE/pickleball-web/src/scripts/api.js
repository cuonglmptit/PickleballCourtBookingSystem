import axios from 'axios';

// Khai báo base URL
const api = axios.create({
    baseURL: 'http://localhost:5039',
    timeout: 10000, // Thời gian timeout cho mỗi request
});

// Hàm gọi API GET
export const getData = (endpoint, params = {}) => {
    return api.get(endpoint, { params })
        .then(response => response.data)
        .catch(error => {
            console.error('API GET error: ', error);
            throw error;
        });
};

// Hàm gọi API POST
export const postData = (endpoint, data) => {
    return api.post(endpoint, data)
        .then(response => response.data)
        .catch(error => {
            console.error('API POST error: ', error);
            throw error; // Ném lỗi để các component có thể xử lý
        });
};

// Hàm gọi API PUT
export const putData = (endpoint, data) => {
    return api.put(endpoint, data)
        .then(response => response.data)
        .catch(error => {
            console.error('API PUT error: ', error);
            throw error;
        });
};

// Hàm gọi API DELETE
export const deleteData = (endpoint) => {
    return api.delete(endpoint)
        .then(response => response.data)
        .catch(error => {
            console.error('API DELETE error: ', error);
            throw error;
        });
};
