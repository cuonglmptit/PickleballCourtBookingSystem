<template>
  <div>
    <label for="start-date">Từ ngày:</label>
    <input
      id="start-date"
      type="date"
      v-model="startDate"
      :min="minDate"
      @change="emitDates"
    />

    <label for="end-date">Đến ngày:</label>
    <input
      id="end-date"
      type="date"
      v-model="endDate"
      :min="startDate || minDate"
      @change="emitDates"
    />

    <div v-if="!datesValid" class="invalid-input">
      Vui lòng chọn cả ngày bắt đầu và ngày kết thúc, đảm bảo ngày kết thúc sau
      ngày bắt đầu.
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      startDate: "",
      endDate: "",
      minDate: new Date().toISOString().split("T")[0],
      datesValid: true,
    };
  },
  methods: {
    emitDates() {
      this.datesValid =
        !!this.startDate && !!this.endDate && this.startDate <= this.endDate;
      this.$emit("date-range-selected", {
        startDate: this.startDate,
        endDate: this.endDate,
      });
    },
  },
};
</script>
<style scoped>
.invalid-input {
  margin-top: 4px;
  color: var(--topic-color3-500);
}

label {
  padding: 0 12px;
}
</style>
