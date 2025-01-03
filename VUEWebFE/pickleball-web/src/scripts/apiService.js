import { getData, postData, putData, deleteData } from './api.js';

// Quản lý các endpoint ở một nơi duy nhất
const endpoints = {
    searchCourtClusters: '/api/CourtCluster/SearchCourtClusterWithFilter',
    getAddress: '/api/Address',
    getListTime: '/api/GetListTime',
    CourtCluster: '/api/CourtCluster',
    getProvinces: '/api/Locations/provinces',
    getCourtsOfCourtCluster: (courtClusterId) => `/api/CourtCluster/${courtClusterId}/Courts`,
    getCustomerInfo: (customerId) => `/api/User/Customer/${customerId}/info`,

    // Endpoint đăng nhập
    login: '/api/Auth/Login',
    //Register
    registerUser: '/api/Auth/Register',

    //CourtOwner
    getBookingStatus: (sts) => `/api/Booking/Status/${sts}`,
    getCourtClusterByCourtOwner: '/api/CourtCluster/GetCourtClusterByCourtOwner',
    getCourtTimeSlotsByBookingId: (bookingId) => `/api/Booking/${bookingId}/CourTimeSlots`

};

export const searchCourtClusters = (queryParams) => {
    return getData(endpoints.searchCourtClusters, queryParams);
};

/**
 * CuongLM (14/12/2024)
 * Lấy thông tin về địa chỉ
 * @param {String} courtClusterId id của địa chỉ 
 * @returns response của api
 */
export const getCourtClusterById = (courtClusterId) => {
    return getData(endpoints.CourtCluster + `/${courtClusterId}`);
};

/**
 * CuongLM (14/12/2024)
 * Lấy thông tin về địa chỉ
 * @param {String} addressId id của địa chỉ 
 * @returns response của api
 */
export const getAddressByid = (addressId) => {
    return getData(endpoints.getAddress + `/${addressId}`);
};

export const getListTime = () => {
    return getData(endpoints.getListTime);
};

export const getCourtsOfCourtCluster = (courtClusterId) => {
    return getData(endpoints.getCourtsOfCourtCluster(courtClusterId));
}

export const getProvinces = () => {
    return getData(endpoints.getProvinces);
}

/**
 * Đăng nhập với tài khoản và mật khẩu
 * @param {String} username Tên đăng nhập
 * @param {String} password Mật khẩu
 * @returns {Promise} Promise chứa kết quả từ API
 */
export const login = (username, password) => {
    const body = {
        username,
        password,
    };
    return postData(endpoints.login, body);
};

export const registerUser = (user) => {
    return postData(endpoints.registerUser, user);
}

/**
 * 
 * @param {String} sts (Pending, CourtOwnerConfirmed, Completed, Canceled, All) 
 * @returns Danh sách booking của user này
 */
export const getBookingStatus = (sts) => {
    return getData(endpoints.getBookingStatus(sts));
}

export const getCourtClusterByCourtOwner = () => {
    return getData(endpoints.getCourtClusterByCourtOwner)
}

export const getCourtTimeSlotsByBookingId = (bookingId) => {
    return getData(endpoints.getCourtTimeSlotsByBookingId(bookingId))
}

export const getCustomerInfo = (customerId) => {
    return getData(endpoints.getCustomerInfo(customerId))
}