import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'; // Import store


createApp(App)
    .use(router)
    .use(store) // Sử dụng Vuex Store
    .mount('#app');
