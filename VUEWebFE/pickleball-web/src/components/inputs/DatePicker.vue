<template>
  <div class="datepicker-container">
    <input 
      v-model="selectedDate" 
      class="datepk-custom" 
      type="date" 
      :min="minDate" 
      @change="checkDate"
    />
    <!-- Div thông báo dưới input khi ngày chọn nhỏ hơn hôm nay -->
    <div v-if="showAlert" class="alert-message">
      Ngày chọn không được nhỏ hơn hôm nay!
    </div>
  </div>
</template>

<script>
export default {
  data() {
    const today = new Date().toISOString().split('T')[0];
    return {
      selectedDate: today,
      minDate: today,
      showAlert: false, // Biến để điều khiển việc hiển thị thông báo
    };
  },
  methods: {
    checkDate() {
      const selected = new Date(this.selectedDate);
      const today = new Date(this.minDate);
      if (selected < today) {
        this.showAlert = true; // Hiển thị thông báo nếu ngày chọn nhỏ hơn hôm nay
      } else {
        this.showAlert = false; // Ẩn thông báo nếu ngày chọn hợp lệ
      }
    }
  }
};
</script>

<style>
input[type="date"] {
  cursor: text;
}

.datepk-custom::-webkit-calendar-picker-indicator {
  position: absolute;
  margin: 0;
  width: 36px; /* Kích thước biểu tượng */
  height: 36px;
  cursor: pointer;
  background-image: url(../../assets/icon/calendar1.png);
  background-repeat: no-repeat;
  background-size: 36px;
  left: 0px;
  /* top: 0px; */
}

.datepk-custom {
  padding-left: 42px;
  -webkit-appearance: none;
  width: fit-content;
  border: none;
  font-size: 18px;
  font-family: roboto-medium;
  height: 36px;
  /* width: 0px; */
  background-color: transparent;
  outline: none;
}

.datepicker-container {
  position: relative;
}

.alert-message {
  color: var(--topic-color3-500);
  font-size: 12px;
  position: absolute;
  width: fit-content;
}
</style>
