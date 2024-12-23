<template>
  <div class="header p-long-banner banner">
    <router-link :to="{ name: 'home' }" class="logo p-logo"></router-link>
    <div class="menu-options">
      <div class="option">
        <PrimaryButton :isRouterLink="true" :to="{ name: 'home' }">
          <template v-slot:name> Đặt sân </template>
        </PrimaryButton>
      </div>
      <div class="option">
        <PrimaryButton>
          <template v-slot:name> Lịch đặt </template>
        </PrimaryButton>
      </div>
      <div class="option">
        <PrimaryButton :isRouterLink="true" :to="{ name: 'login' }">
          <template v-slot:name> Đăng nhập </template>
        </PrimaryButton>
        <div class="slash">|</div>
      </div>
      <div class="option">
        <PrimaryButton :isRouterLink="true" :to="{ name: 'register' }">
          <template v-slot:name> Đăng ký </template>
        </PrimaryButton>
      </div>
    </div>
  </div>
</template>

<script>
import PrimaryButton from "./buttons/PrimaryButton.vue";
export default {
  components: {
    PrimaryButton,
  },
};
</script>

<style>
.header {
  /* background: greenyellow; */
  position: fixed;
  z-index: 9999;
  display: flex;
  height: 96px;
  width: 100%;
  top: 0px;
}
.menu-options {
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: end;
  align-items: center;
  padding-right: 100px;
  column-gap: 4px;
}
.option {
  width: 100px;
  position: relative;
}

.option .slash {
  font-size: 28px;
  font-family: roboto-regular;
  color: white;
  position: absolute; /* Vị trí tuyệt đối so với phần tử cha */
  top: 45%; /* Căn giữa theo chiều dọc */
  right: -5px;
  transform: translateY(-50%); /* Điều chỉnh để căn giữa hoàn toàn */
}

.logo {
  display: flex;
  /* background-color: crimson; */
  background-size: contain;
  width: 250px;
  flex-shrink: 0;
  background-repeat: no-repeat;
  background-position: center;
}

.banner {
  background-position-x: right;
  background-size: auto 100%;
  background-repeat: no-repeat;
  background-color: var(--banner-background-color);
}

.right-part {
  background-color: beige;
  width: 100%;
}
</style>