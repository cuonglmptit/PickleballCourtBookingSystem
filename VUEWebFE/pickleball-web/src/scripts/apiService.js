import { getData, postData, putData, deleteData, postDataMultipart } from './api.js';

// Quản lý các endpoint ở một nơi duy nhất
const endpoints = {
    searchCourtClusters: '/api/CourtCluster/SearchCourtClusterWithFilter',
    getAddress: '/api/Address',
    getListTime: '/api/GetListTime',
    CourtCluster: '/api/CourtCluster',
    CourtClusterMultipart: '/api/CourtCluster/multipart',
    getProvinces: '/api/Locations/provinces',
    getDistricts: '/api/Locations/districts',
    getWards: '/api/Locations/wards',
    getStreets: '/api/Locations/streets',
    getCourtsOfCourtCluster: (courtClusterId) => `/api/CourtCluster/${courtClusterId}/Courts`,
    getCustomerInfo: (customerId) => `/api/User/Customer/${customerId}/info`,
    getImageCourtUrl: (id) => `/api/ImageCourtUrl/Cluster/${id}`,
    getTimeSlotOfCourt: `/api/CourtTimeSlot/Court`,

    //Put
    putAddress: (addressId) => `/api/Address/${addressId}`,
    putCluster: (clusterId) => `/api/CourtCluster/${clusterId}`,

    // Endpoint đăng nhập
    login: '/api/Auth/Login',
    //Register
    registerUser: '/api/Auth/Register',

    //CourtOwner
    getBookingStatus: (sts) => `/api/Booking/Status/${sts}`,
    getCourtClusterByCourtOwner: '/api/CourtCluster/GetCourtClusterByCourtOwner',
    getCourtTimeSlotsByBookingId: (bookingId) => `/api/Booking/${bookingId}/CourTimeSlots`,
    courtOwnerConfirmBooking: `/api/Booking/court-owner-confirm-booking`,
    courtOwnerConfirmPaid: `/api/Booking/court-owner-confirm-paid`,
    cancelBooking: `/api/Booking/cancel-booking`,
    getCourtPricesByCourtClusterId: (courtClusterId) => `/api/CourtPrice/GetCourtPricesByCourtClusterId/${courtClusterId}`,
    createDefaultPrice: '/api/CourtPrice/multiple',
    autoCreateCourtTimeSlot: '/api/CourtCluster/AutoCreateCourtTimeSlot',

    //Customer
    addBooking: `/api/Booking/add-booking`
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

export const getDistricts = () => {
    return getData(endpoints.getDistricts);
}

export const getWards = () => {
    return getData(endpoints.getWards);
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

export const courtOwnerConfirmBooking = (bookingId) => {
    const body = {
        "bookingId": bookingId
    };
    return postData(endpoints.courtOwnerConfirmBooking, body);
}

export const courtOwnerConfirmPaid = (bookingId) => {
    const body = {
        "bookingId": bookingId
    };
    return postData(endpoints.courtOwnerConfirmPaid, body);
}

export const cancelBooking = (bookingId) => {
    const body = {
        "bookingId": bookingId,
        "reason": ""
    };
    return postData(endpoints.cancelBooking, body);
}

export const getCourtPricesByCourtClusterId = (courtClusterId) => {
    return getData(endpoints.getCourtPricesByCourtClusterId(courtClusterId))
}

export const createDefaultPrice = (prices) => {
    return postData(endpoints.createDefaultPrice, prices)
}

export const autoCreateCourtTimeSlot = (courtClusterId, listDate) => {
    const body = {
        "courtClusterId": courtClusterId,
        "dates": listDate
    }
    console.log(body);

    return postData(endpoints.autoCreateCourtTimeSlot, body)
}

/**
 * Tạo mới một Court Cluster
 * @param {Object} courtClusterData Dữ liệu Court Cluster cần tạo
 * @returns {Promise} Promise chứa kết quả từ API
 */
export const createCourtCluster = (formData) => {
    return postDataMultipart(endpoints.CourtClusterMultipart, formData);
};

export const getImageCourtUrl = (clusterId) => {
    return getData(endpoints.getImageCourtUrl(clusterId))
}

export const putAddress = (addressId, addressEntity) => {
    return putData(endpoints.putAddress(addressId), addressEntity)
}
export const putCluster = (clusterId, clusterEntity) => {
    return putData(endpoints.putCluster(clusterId), clusterEntity)
}

export const getTimeSlotOfCourt = (courtId, date) => {
    const body = {
        "courtId": courtId,
        "date": date
    }
    console.log(body);

    return postData(endpoints.getTimeSlotOfCourt, body)
}

export const addBooking = (courtId, timesSlotIds) => {
    const body = {
        "courtTimeSlotsIds": timesSlotIds,
        "courtId": courtId,
    }
    console.log(body);

    return postData(endpoints.addBooking, body)
}