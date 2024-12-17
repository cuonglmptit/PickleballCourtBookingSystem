<template>
  <div class="container">
    <div class="search-container p-search-background">
      <h1 class="search-sologan">Đặt sân nhanh, chiến ngay hôm nay!</h1>
      <div class="search-frame-container">
        <SearchFrame></SearchFrame>
      </div>
    </div>
    <div class="informations">
      <div class="inf-container">
        <div class="inf-img p-icon-court-active-hori"></div>
        <div class="des-title">Tìm kiếm và đặt sân</div>
        <div class="inf-des">
          Tìm kiếm sân phù hợp nhất với nhu cầu của bạn
        </div>
      </div>
      <div class="inf-separate"></div>
      <div class="inf-container">
        <div class="inf-img p-icon-calendar"></div>
        <div class="des-title">Xem lịch đặt</div>
        <div class="inf-des">
          Xem lại những lịch đặt sân mà bạn đã thực hiện
        </div>
      </div>
      <div class="inf-separate"></div>
      <div class="inf-container">
        <div class="inf-img p-icon-dashboard1"></div>
        <div class="des-title">Quản lý và thống kê</div>
        <div class="inf-des">
          Bạn là chủ sân và muốn dùng phần mềm quản lý? Đăng ký ngay để sử dụng! 
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import SearchFrame from "./SearchFrame.vue";
export default {
  components: {
    SearchFrame,
  },
};
</script>

<style scoped>
.container {
  width: 100%;
  height: fit-content;
  display: flex;
  flex-direction: column;
  justify-content: start;
  /* background-color: brown; */
}
.search-container {
  width: 100%;
  height: 560px;
  background-size: cover;
  background-repeat: no-repeat;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background-position: center;
}
.search-frame-container {
  width: 70%;
  min-width: fit-content;
}
.search-sologan {
  font-size: 32px;
  color: white;
  font-family: roboto-bolditalic;
  margin: 12px 0px;
  white-space: nowrap;
}

/* CSS cho thông tin thêm */
.informations {
  height: calc(100vh - 560px - 96px);
  width: 100%;
  /* background-color: palevioletred; */
  display: flex;
  justify-content: space-around;
  align-items: center;
  column-gap: 72px;
  padding: 0 72px;
}
.inf-container {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  row-gap: 8px;
}
.inf-img {
  /* background-color: aquamarine; */
  width: 250px;
  height: 150px;
  background-size: 150px;
  background-repeat: no-repeat;
  background-position: center;
}

.inf-separate {
  width: 2px;
  height: 50%;
  flex-shrink: 0;
  background-color: black;
}

.des-title {
  font-size: 24px;
  font-family: roboto-medium;
  text-align: center;
}
</style>