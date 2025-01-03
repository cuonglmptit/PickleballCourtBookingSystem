<template>
  <div class="header p-long-banner banner">
    <router-link :to="{ name: 'home' }" class="logo p-logo"></router-link>
    <div class="menu-options">
      <div
        class="option"
        v-if="!isAuthenticated || (user && user.role === 'Customer')"
      >
        <PrimaryButton :isRouterLink="true" :to="{ name: 'home' }">
          <template v-slot:name> Đặt sân </template>
        </PrimaryButton>
      </div>
      <div
        class="option"
        v-if="!isAuthenticated || (user && user.role === 'Customer')"
      >
        <PrimaryButton :isRouterLink="true" :to="{ name: 'customer-bookings' }">
          <template v-slot:name> Lịch đặt </template>
        </PrimaryButton>
      </div>
      <div
        class="option"
        v-if="isAuthenticated && user && user.role === 'CourtOwner'"
      >
        <PrimaryButton
          :isRouterLink="true"
          :to="{ name: 'manage-court-cluster' }"
          v-if="
            isRouteActive('manage-court-cluster') ||
            (!isRouteActive('manage-court-cluster') &&
              !isRouteActive('owner-manage-booking'))
          "
        >
          <template v-slot:name> Quản lý </template>
        </PrimaryButton>
        <PrimaryButton
          :isRouterLink="true"
          :to="{ name: 'owner-manage-booking' }"
          v-if="isRouteActive('owner-manage-booking')"
        >
          <template v-slot:name> Quản lý </template>
        </PrimaryButton>
      </div>
      <div
        class="option"
        v-if="isAuthenticated && user && user.role === 'CourtOwner'"
      >
        <PrimaryButton :isRouterLink="true" :to="{ name: 'owner-dashboard' }">
          <template v-slot:name> Thống kê </template>
        </PrimaryButton>
      </div>
      <div class="option" v-if="!isAuthenticated">
        <PrimaryButton :isRouterLink="true" :to="{ name: 'login' }">
          <template v-slot:name> Đăng nhập </template>
        </PrimaryButton>
        <div class="slash" v-if="!isAuthenticated">|</div>
      </div>
      <div class="option" v-if="!isAuthenticated">
        <PrimaryButton :isRouterLink="true" :to="{ name: 'register' }">
          <template v-slot:name> Đăng ký </template>
        </PrimaryButton>
      </div>
      <div class="option-profile" v-if="isAuthenticated">
        <div class="profile-img" @click="toggleDropdown">
          <img :src="user?.avatarUrl || require('@/assets/icon/user.png')" />
        </div>
        <div class="profile-name">{{ user.name }}</div>

        <!-- Dropdown menu -->
        <div class="dropdown-menu" v-if="isDropdownVisible">
          <div class="dropdown-item" @click="editAccount">Sửa tài khoản</div>
          <div class="dropdown-item" @click="changePassword">Đổi mật khẩu</div>
          <div class="dropdown-item" @click="logout">Đăng xuất</div>
        </div>
      </div>
      <div
        class="role"
        v-if="isAuthenticated && user && user.role !== 'Customer'"
      >
        <template v-if="user.role === 'CourtOwner'">Chủ sân</template>
        <template v-if="user.role === 'Admin'">Admin</template>
      </div>
    </div>
  </div>
</template>

<script>
import PrimaryButton from "./buttons/PrimaryButton.vue";
import { mapGetters } from "vuex";
export default {
  components: {
    PrimaryButton,
  },
  data() {
    return {
      isDropdownVisible: false, // Trạng thái hiển thị menu
    };
  },
  computed: {
    ...mapGetters({
      isAuthenticated: "isAuthenticated",
      user: "getUser",
    }),
  },
  methods: {
    isRouteActive(routeName) {
      return this.$route.name === routeName; // Kiểm tra nếu tên route hiện tại bằng với routeName được truyền vào
    },
    toggleDropdown(event) {
      // Ngừng sự kiện click để không bị nhận diện là click ra ngoài
      event.stopPropagation();
      this.isDropdownVisible = !this.isDropdownVisible; // Đổi trạng thái
    },
    closeDropdown() {
      this.isDropdownVisible = false; // Đóng dropdown
    },
    editAccount() {
      // Điều hướng tới trang sửa tài khoản
      this.$router.push({ name: "edit-account" });
    },
    changePassword() {
      // Điều hướng tới trang đổi mật khẩu
      this.$router.push({ name: "change-password" });
    },
    logout() {
      this.$store.dispatch("logout");
      this.$router.push({ name: "home" }); // Chuyển hướng về trang chủ sau khi đăng xuất
    },
  },
  mounted() {
    // Lắng nghe sự kiện click ra ngoài
    document.addEventListener("click", this.closeDropdown);
  },
  beforeUnmount() {
    // Hủy bỏ sự kiện khi component bị hủy
    document.removeEventListener("click", this.closeDropdown);
  },
};
</script>

<style>
.header {
  /* background: greenyellow; */
  position: fixed;
  z-index: 5;
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

.option-profile {
  position: relative;
  min-width: 100px;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  row-gap: 6px;
  align-items: center;
}

.role {
  color: var(--topic-color-700);
  background: white;
  padding: 2px 6px 2px 6px;
  border-radius: 0 0 0px 8px;
  position: fixed;
  top: 0;
  right: 0;
  font-family: roboto-bold;
}

.option-profile .profile-name {
  font-family: roboto-bold;
  color: white;
}

.profile-img {
  height: 42px;
  width: 42px;
  border-radius: 100px;
  background-color: white;
  overflow: hidden;
  cursor: pointer;
}

.profile-img:hover {
  filter: brightness(80%);
}

.profile-img:active {
  filter: brightness(50%);
}

.profile-img img {
  object-fit: cover;
  width: 100%;
  height: 100%;
}

.dropdown-menu {
  position: absolute;
  top: 100%;
  right: 0;
  background-color: white;
  border-radius: 4px;
  z-index: 1;
  display: flex;
  flex-direction: column;
  min-width: 150px;
  overflow: hidden;
  border: 1px solid var(--topic-color-300);
  box-shadow: 1px 1px 4px var(--topic-color-300);
}

.dropdown-item {
  font-size: 15px;
  cursor: pointer;
  padding: 6px;
  border: 1px solid white;
}

.dropdown-item:hover {
  border: 1px solid var(--topic-color-300);
  background-color: var(--topic-color-600);
  color: white;
}
</style>