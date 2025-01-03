<template>
  <div class="container">
    <div class="form-container" action="">
      <div class="left-part">
        <div class="left-top">
          <div class="font-size-32">Đăng nhập</div>
        </div>
        <form class="signin-form" @submit.prevent="handleLogin">
          <div class="p-input-container">
            <label for="username">Tài khoản</label>
            <input
              type="text"
              id="username"
              v-model="username"
              :class="{ 'input-error': usernameError }"
              @input="validateField('username')"
            />
          </div>
          <div class="p-input-container">
            <label for="password">Mật khẩu</label>
            <input
              :type="showPassword ? 'text' : 'password'"
              id="password"
              v-model="password"
              :class="{ 'input-error': passwordError }"
              @input="validateField('password')"
            />
            <div
              v-if="password"
              class="toggle-password"
              :class="showPassword ? 'p-icon-view' : 'p-icon-hide'"
              @click="togglePasswordVisibility"
            ></div>
          </div>
          <div class="error-message" v-if="errors">
            {{ errors }}
          </div>
          <button type="submit" style="display: none"></button>
        </form>
        <div class="left-bottom">
          <button class="p-button this-scoped-btn" @click="handleLogin">
            Đăng nhập
          </button>
        </div>
      </div>
      <div class="right-part p-banner">
        <div class="right-top">
          <div class="big-title text-white backdrop-filter-blur-10">
            Đặt sân Pickleball
          </div>
        </div>
        <div class="right-mid backdrop-filter-blur-5">
          <div class="big-title text-white">Chào mừng!</div>
          <div class="customer-register">
            <div class="text-white">Bạn chưa có tài khoản?</div>
            <PrimaryButtonBorder
              :replace="true"
              :isRouterLink="true"
              :to="{ name: 'register' }"
              style="width: 168px"
            >
              <template v-slot:name> Đăng ký </template>
            </PrimaryButtonBorder>
          </div>
          <div class="text-white">
            Hoặc bạn là chủ sân và muốn dùng hệ thống?
            <router-link
              class="text-white roboto-italic"
              :to="{ name: 'register' }"
            >
              Đăng ký thành chủ sân
            </router-link>
          </div>
        </div>
        <div class="right-bot">
          <router-link class="text-white" :to="{ name: 'home' }">
            Trang chủ
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PrimaryButtonBorder from "../../components/buttons/PrimaryButtonBorder.vue";
import { login } from "@/scripts/apiService.js";
export default {
  components: {
    PrimaryButtonBorder,
  },
  data() {
    return {
      username: "",
      password: "",
      errors: "",
      usernameError: false,
      passwordError: false,
      showPassword: false,
    };
  },
  methods: {
    togglePasswordVisibility() {
      this.showPassword = !this.showPassword;
    },
    validateField(field) {
      if (field === "username") {
        this.usernameError = !this.username.trim();
      } else if (field === "password") {
        this.passwordError = !this.password.trim();
      }
    },
    async handleLogin() {
      // Kiểm tra xem username và password đã được nhập chưa
      this.usernameError = !this.username.trim();
      this.passwordError = !this.password.trim();

      // Nếu có lỗi, không tiếp tục
      if (this.usernameError || this.passwordError) {
        return;
      }

      try {
        // Gọi service login
        const response = await login(this.username, this.password);
        console.log(response);
        // Kiểm tra kết quả trả về (ví dụ token)
        if (response.success) {
          // Lưu token vào Vuex
          this.$store.dispatch("login", {
            user: response.data.user,
            token: response.data.token,
          });
          // Điều hướng đến trang chính
          this.$router.push("/");
        } else {
          this.errors = response.userMsg;
        }
      } catch (error) {
        console.log(`handleLogin ${error}`);
      }
    },
  },
};
</script>

<style scoped>
.container {
  width: 100%;
  height: 100vh;
  background-color: var(--gray-background);
  position: relative;
}

.form-container {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 1124px;
  height: 80%;
  /* background-color: burlywood; */
  display: flex;
  border-radius: 4px;
  overflow: hidden;
  box-shadow: 2px 2px 12px rgba(0, 0, 0, 0.5);
}

.left-part {
  background-color: white;
  width: 55%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  row-gap: 32px;
  flex-shrink: 0;
}

.signin-form {
  padding: 24px 72px 24px 72px;
  width: 100%;
  flex-shrink: 0;
  display: flex;
  flex-direction: column;
  justify-content: center;
  row-gap: 24px;
}

.p-input-container {
  position: relative;
  display: flex;
  flex-direction: column;
  row-gap: 6px;
}

input {
  height: 36px;
  border: 1px solid rgba(0, 0, 0, 0.05);
  border-radius: 1000px;
  outline: none;
  padding: 0px 18px 0px 18px;
  background-color: var(--gray-background);
  text-align: start;
}

input:focus {
  border: 1px solid var(--topic-color-500);
  background-color: white;
}

.input-error {
  background: url("@/assets/icon/error.png");
  background-repeat: no-repeat;
  background-position-x: calc(100% - 12px);
  background-position-y: center;
  background-size: 24px;
  border: 1px solid var(--topic-color3-400);
}

.toggle-password {
  width: 16px;
  height: 16px;
  position: absolute;
  right: 12px;
  bottom: 10px;
  border: none;
  background-color: transparent;
  cursor: pointer;
  background-repeat: no-repeat;
  background-size: 16px;
}
/* input:not(:placeholder-shown):not(:focus) {
  background-color: white;
  border: 1px solid rgba(0, 0, 0, 0.5);
} */

.left-top {
  display: flex;
  justify-content: start;
  padding: 0 72px 0 72px;
}

.left-bottom {
  display: flex;
  justify-content: center;
}

.big-title {
  font-size: 32px;
  font-family: roboto-bold;
}

.this-scoped-btn {
  width: 212px;
}

/* CSS phần bên phải */
.right-part {
  width: 45%;
  height: 100%;
  background-color: thistle;
  background-repeat: no-repeat;
  background-size: cover;
  background-position-x: calc(100% + 64px);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
}

.right-top {
  margin-top: 72px;
}

.right-mid {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  row-gap: 16px;
  width: 100%;
  margin-bottom: 124px;
}

.right-bot {
  display: flex;
  justify-content: end;
  width: 100%;
  padding: 0 12px 12px 12px;
}

.right-mid .customer-register {
  display: flex;
  justify-content: center;
  row-gap: 6px;
  flex-direction: column;
  align-items: center;
}

.error-message {
  color: var(--topic-color3-400);
}
</style>