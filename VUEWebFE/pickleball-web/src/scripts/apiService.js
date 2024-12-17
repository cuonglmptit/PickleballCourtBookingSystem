import { getData, postData, putData, deleteData } from './api.js';

// Quản lý các endpoint ở một nơi duy nhất
const endpoints = {
    searchCourtClusters: '/api/CourtCluster/SearchCourtClusterWithFilter',
    getAddress: '/api/Address',
    getListTime: '/api/GetListTime',
};

export const searchCourtClusters = (queryParams) => {
    return getData(endpoints.searchCourtClusters, queryParams);
};

/**
 * CuongLM (14/12/2024)
 * Lấy thông tin về địa chỉ
 * @param {String} addressId id của địa chỉ 
 * @returns response của api
 */
export const getAddressByid = (addressId) => {
    return getData(endpoints.getAddress+`/${addressId}`);
};

export const getListTime = () => {
    return getData(endpoints.getListTime);
};