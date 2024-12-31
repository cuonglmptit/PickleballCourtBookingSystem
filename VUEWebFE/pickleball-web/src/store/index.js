import { createStore } from 'vuex';
import createPersistedState from "vuex-persistedstate";
import auth from './modules/auth'; // Module auth của bạn

// Tạo store
const store = createStore({
  modules: {
    auth,
  },
  plugins: [createPersistedState({
    // Cấu hình nơi lưu trữ
    storage: window.localStorage, // Hoặc window.sessionStorage
  })],
});

export default store;
