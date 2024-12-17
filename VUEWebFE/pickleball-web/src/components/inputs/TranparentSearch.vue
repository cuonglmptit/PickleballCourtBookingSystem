<template>
  <div class="p-input-container">
    <input
      class="p-trans-input"
      type="text"
      :placeholder="placeholder"
      :value="keyword"
      @input="$emit('update:keyword', $event.target.value)"
    />
  </div>
</template>

<script>
export default {
  props: {
    // Định nghĩa props tại đây
    keyword: {
      type: String, // keyword là chuỗi
      required: true,
    },
    placeholder: {
      type: String, // Đặt kiểu dữ liệu
      required: false, // Không bắt buộc
      default: "Nhập từ khóa...", // Giá trị mặc định
    },
  },
  emits: ["update:keyword"],
};
</script>

<style scoped>
input[type="text"]::-webkit-calendar-picker-indicator,
input[type="email"]::-webkit-calendar-picker-indicator,
input[type="number"]::-webkit-calendar-picker-indicator,
input[type="search"]::-webkit-calendar-picker-indicator {
  opacity: 0;
  width: 16px;
}
.p-input-container {
  position: relative;
  display: flex;
  flex-direction: column;
  row-gap: 8px;
}
.p-trans-input {
  position: relative;
  height: 42px;
  padding-left: 8px;
  padding-right: 8px;
  box-sizing: border-box;
  border: none;
  border-bottom: 2px solid rgba(0, 0, 0, 1);
  font-family: roboto-regular;
  font-size: 24px;
  text-align: left;
  outline: none;
  background: transparent;
}
</style>