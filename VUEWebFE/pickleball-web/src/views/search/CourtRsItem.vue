<template>
  <div class="court-rs-container">
    <div class="court-header">
      <div class="court-title">Cụm sân hà hông 1</div>
      <div class="court-title">Đánh giá 5/5</div>
    </div>
    <div class="court-content">
      <div class="court-thumbnail">
        <img src="../../assets/img/picklleball_court_1.webp" alt="" />
      </div>
      <div class="court-brief">
        <div class="court-inf">0862200319</div>
        <div class="court-action">Chi tiết đặt sân</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
.court-rs-container {
  width: 100%;
  height: 350px;
  min-width: fit-content;
  display: flex;
  /* background-color: tomato; */
  flex-direction: column;
  border-radius: 4px;
  background-color: white;
  box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.2);
  padding: 12px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  row-gap: 12px;
}
.court-header {
  display: flex;
  justify-content: space-between;
  width: 100%;
  flex-wrap: nowrap;
  /* background-color: cornsilk; */
  height: 24px;
}

.court-header .court-title {
  font-size: 20px;
  white-space: nowrap;
  font-family: roboto-bold;
}

.court-content {
  display: flex;
  height: 100%;
  width: 100%;
  min-width: fit-content;
  column-gap: 12px;
  /* background-color: palevioletred; */
}

.court-thumbnail {
  min-width: 250px;
  width: 60%;
  height: 100%;
  border-radius: 4px;
  overflow: hidden;
  /* background-color: skyblue; */
}

.court-thumbnail img {
  object-fit: cover;
  width: 100%;
  height: 100%;
}

.court-brief {
  display: flex;
  flex-direction: column; /* Nội dung xếp theo cột */
  justify-content: space-between; /* Căn chỉnh nội dung */
  overflow: hidden;
}
</style>