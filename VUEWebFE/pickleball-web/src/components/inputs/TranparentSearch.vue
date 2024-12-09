<template>
  <div class="p-input-container">
    <input
      class="p-trans-input"
      type="text"
      placeholder="Tên sân hoặc địa chỉ..."
    />
  </div>
</template>

<script>
export default {};
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