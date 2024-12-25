import { getData, postData, putData, deleteData } from './api.js';

// Quản lý các endpoint ở một nơi duy nhất
const endpoints = {
    searchCourtClusters: '/api/CourtCluster/SearchCourtClusterWithFilter',
    getAddress: '/api/Address',
    getListTime: '/api/GetListTime',
    CourtCluster: '/api/CourtCluster',
    getCourtsOfCourtCluster: (courtClusterId) => `/api/CourtCluster/${courtClusterId}/Courts`,
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