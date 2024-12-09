<template>
  <header class="header">
    <div class="header-part1 p-logo"></div>
    <div class="header-part2">
      <div class="header-part2-top">
        <div class="left"></div>
        <div class="mid">
          <MenuOption
            :optionTitle="'Sân pickleball'"
            :isRouterLink="true"
            :to="{ name: 'Home' }"
            :isActive="isActive('Home')"
            :iconClass="
              isActive('Home')
                ? 'div-option-icon p-icon-field p-icon-24'
                : 'div-option-icon p-icon-field-bw p-icon-24'
            "
          />
          <MenuOption
            :optionTitle="'Lịch đặt'"
            :isRouterLink="true"
            :to="{ name: 'Bookings' }"
            :isActive="isActive('Bookings')"
            :iconClass="
              isActive('Bookings')
                ? 'div-option-icon p-icon-shcedule p-icon-24'
                : 'div-option-icon p-icon-schedule-bw p-icon-24'
            "
          />

          <MenuOption
            v-if="false"
            :optionTitle="'Thống kê'"
            :isRouterLink="true"
          />
        </div>
        <div class="right"></div>
      </div>
      <div class="header-part2-bot">
        <div class="p-input-container">
          <input
            class="p-input p-input-icon p-icon-search p-icon-24"
            placeholder="Tìm kiếm..."
            type="text"
          />
        </div>
      </div>
    </div>
    <div class="header-part3">
      <div class="profile"></div>
      <div>User 1</div>
    </div>
  </header>
</template>
<script>
import MenuOption from "../base/MenuOption.vue";
export default {
  components: {
    MenuOption,
  },
  methods: {
    isActive(routeName) {
      // Kiểm tra xem route hiện tại có khớp với route được truyền vào không
      return this.$route.name === routeName;
    },
  },
};
</script>
<style scoped>
/* CSS CHO HEADER */
.header-part1 {
  /* background-color: red; */
  height: 100%;
  width: 20%;
  background-repeat: no-repeat;
  background-size: contain;
  background-position: center;
}

.header-part2 {
  /* background-color: chartreuse; */
  height: 100%;
  width: 100%;
}

.header-part3 {
  /* background-color: yellow; */
  height: 100%;
  width: 20%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.header-part3 div {
  font-family: roboto-bold;
}

.header-part2-top {
  /* background-color: antiquewhite; */
  height: 56px;
  width: 100%;
  display: flex;
  border-bottom: 2px solid rgba(255, 255, 255, 1);
}

.header-part2-top .left {
  /* background-color: lavender; */
  width: 100%;
}

.header-part2-top .mid {
  /* background-color: thistle; */
  width: 100%;
  display: flex;
  justify-content: space-around;
  align-items: center;
}

.header-part2-top .right {
  /* background-color: lightsalmon; */
  width: 100%;
}

.header-part3 .profile {
  width: 48px;
  height: 48px;
  margin: 4px;
  border-radius: 24px;
  background-color: rgb(0, 0, 0);
}

.header-part2-bot {
  /* background-color: royalblue; */
  height: 56px;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>