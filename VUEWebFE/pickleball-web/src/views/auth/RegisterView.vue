<template>
  <div class="container">
    <div class="form-container" action="">
      <div class="left-part">
        <div class="left-top">
          <div class="font-size-32">Đăng ký người dùng</div>
        </div>
        <form class="signup-form" @submit.prevent="handleRegister">
          <div class="p-input-container">
            <label for="">Tên của bạn</label>
            <input
              type="text"
              id="name"
              v-model="user.Name"
              :class="{ 'input-error': nameError }"
              @input="validateField('name')"
            />
          </div>
          <div class="p-input-container">
            <label for="">Email</label>
            <input
              type="email"
              id="email"
              v-model="user.Email"
              :class="{ 'input-error': emailError }"
              @input="validateField('email')"
            />
          </div>
          <div class="p-input-container">
            <label for="">Số điện thoại</label>
            <input
              type="text"
              id="phonenumber"
              v-model="user.PhoneNumber"
              :class="{ 'input-error': phoneError }"
              @input="validateField('phonenumber')"
            />
          </div>
          <div class="p-input-container">
            <label for="username">Tài khoản</label>
            <input
              type="text"
              id="username"
              v-model="user.Username"
              :class="{ 'input-error': usernameError }"
              @input="validateField('username')"
            />
          </div>
          <div class="p-input-container">
            <label for="">Mật khẩu</label>
            <input
              id="password"
              :type="showPassword ? 'text' : 'password'"
              v-model="user.Password"
              :class="{ 'input-error': passwordError }"
              @input="validateField('password')"
            />
            <div
              v-if="user.Password"
              class="toggle-password"
              :class="showPassword ? 'p-icon-view' : 'p-icon-hide'"
              @click="showPassword = !showPassword"
            ></div>
          </div>
          <div class="p-input-container">
            <label for=""
              >Nhập lại mật khẩu
              <span v-if="confirmPasswordError" class="error-message">
                Mật khẩu nhật lại chưa khớp
              </span>
            </label>
            <input
              :type="showConfirmPassword ? 'text' : 'password'"
              id="confirmPassword"
              v-model="user.ConfirmPassword"
              @input="validateField('confirmPassword')"
            />
            <div
              v-if="user.ConfirmPassword"
              class="toggle-password"
              :class="showConfirmPassword ? 'p-icon-view' : 'p-icon-hide'"
              @click="showConfirmPassword = !showConfirmPassword"
            ></div>
          </div>
          <div class="checkbox-input">
            <input
              type="checkbox"
              id="courtOwner"
              name="courtOwner"
              v-model="isCourtOwner"
            />
            <label for="courtOwner"> Tôi muốn trở thành chủ sân</label>
          </div>
          <button type="submit" style="display: none"></button>
        </form>
        <div class="left-bottom">
          <button class="p-button this-scoped-btn" @click="handleRegister">
            Đăng ký
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
          <div class="text-white">Bạn đã có tài khoản?</div>
          <PrimaryButtonBorder
            :replace="true"
            :isRouterLink="true"
            :to="{ name: 'login' }"
            style="width: 168px"
          >
            <template v-slot:name> Đăng nhập </template>
          </PrimaryButtonBorder>
        </div>
        <div class="right-bot">
          <router-link class="text-white" :to="{ name: 'home' }"
            >Trang chủ</router-link
          >
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import PrimaryButtonBorder from "../../components/buttons/PrimaryButtonBorder.vue";
import { registerUser } from "@/scripts/apiService.js";
export default {
  components: {
    PrimaryButtonBorder,
  },
  data() {
    return {
      user: {
        Username: "",
        Password: "",
        ConfirmPassword: "",
        Name: "",
        Email: "",
        PhoneNumber: "",
        Role: "Customer",
      },
      isCourtOwner: false, // Ràng buộc với checkbox
      nameError: false,
      emailError: false,
      phoneError: false,
      usernameError: false,
      passwordError: false,
      confirmPasswordError: false,
      errors: "", // Thêm phần để hiển thị lỗi tổng thể nếu có
      showPassword: false,
      showConfirmPassword: false,
    };
  },
  methods: {
    validateEmail(email) {
      if (/^[\w.-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/.test(email)) {
        return true;
      } else {
        return false;
      }
    },
    validateField(field) {
      if (field === "name") {
        this.nameError = !this.user.Name.trim();
      } else if (field === "email") {
        this.emailError =
          !this.user.Email.trim() || !this.validateEmail(this.user.Email);
      } else if (field === "phonenumber") {
        this.phoneError = !this.user.PhoneNumber.trim();
      } else if (field === "username") {
        this.usernameError = !this.user.Username.trim();
      } else if (field === "password") {
        this.passwordError = !this.user.Password.trim();
      } else if (field === "confirmPassword") {
        this.confirmPasswordError =
          this.user.ConfirmPassword !== this.user.Password;
      }
    },

    async handleRegister() {
      // Xác định vai trò dựa trên checkbox
      this.user.Role = this.isCourtOwner ? "CourtOwner" : "Customer";
      // Kiểm tra tất cả các trường
      this.nameError = !this.user.Name.trim();
      this.emailError =
        !this.user.Email.trim() || !this.validateEmail(this.user.Email);
      this.phoneError = !this.user.PhoneNumber.trim();
      this.usernameError = !this.user.Username.trim();
      this.passwordError = !this.user.Password.trim();
      this.confirmPasswordError =
        this.user.ConfirmPassword !== this.user.Password;
      // Nếu có lỗi, không tiếp tục
      if (
        this.nameError ||
        this.emailError ||
        this.phoneError ||
        this.usernameError ||
        this.passwordError ||
        this.confirmPasswordError
      ) {
        return;
      }

      try {
        // Thực hiện yêu cầu đăng ký (Giả sử có hàm đăng ký API)
        const response = await registerUser(this.user);
        if (response.success) {
          // Nếu đăng ký thành công, có thể chuyển hướng hoặc thông báo
          this.$router.push("/login");
          Swal.fire({
            title: "Thành công!",
            text: "Đăng ký thành công!",
            icon: "success",
          });
        } else {
          this.errors = response.data;
          const errorResult = Object.entries(this.errors)
            .map(([key, value]) => `${key}: ${value}`)
            .join("<br>"); // Sử dụng <br> thay vì \n
          Swal.fire({
            title: "Thất bại!",
            html: errorResult, // Dùng html thay vì text
            icon: "error",
          });
        }
      } catch (error) {
        console.log(`handleRegister error: ${error}`);
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

.signup-form {
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

.p-input-container input {
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

.checkbox-input {
  display: flex;
  align-items: center;
  column-gap: 4px;
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
  row-gap: 12px;
  width: 100%;
  margin-bottom: 124px;
}

.right-bot {
  display: flex;
  justify-content: end;
  width: 100%;
  padding: 0 12px 12px 12px;
}

.error-message {
  color: var(--topic-color3-400);
}
</style>