<template>
  <div class="container">
    <div class="form-container" action="">
      <div class="left-part">
        <div class="left-top">
          <div class="font-size-32">Đăng ký người dùng</div>
        </div>
        <form class="signup-form" action="">
          <div class="p-input-container">
            <label for="">Tên của bạn</label>
            <input type="text" id="name" placeholder="" />
          </div>
          <div class="p-input-container">
            <label for="">Email</label>
            <input type="text" id="email" placeholder="example@gmail.com" />
          </div>
          <div class="p-input-container">
            <label for="">Số điện thoại</label>
            <input type="text" id="phonenumber" placeholder="" />
          </div>
          <div class="p-input-container">
            <label for="username">Tài khoản</label>
            <input type="text" id="username" placeholder="" />
          </div>
          <div class="p-input-container">
            <label for="">Mật khẩu</label>
            <input
              type="password"
              id="password"
              placeholder="Tối thiểu 6 ký tự..."
            />
            <div class="p-input-container">
              <input
                type="password"
                id="confirmPassword"
                placeholder="Nhập lại mật khẩu..."
              />
            </div>
          </div>
          <div class="checkbox-input">
            <input type="checkbox" id="courtOwner" name="courtOwner" />
            <label for="courtOwner"> Tôi muốn trở thành chủ sân</label>
          </div>
        </form>
        <div class="left-bottom">
          <button class="p-button this-scoped-btn">Đăng ký</button>
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
import PrimaryButtonBorder from "../../components/buttons/PrimaryButtonBorder.vue";
export default {
  components: {
    PrimaryButtonBorder,
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

.checkbox-input{
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
</style>