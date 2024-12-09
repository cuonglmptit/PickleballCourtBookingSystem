<template>
  <div ref="container" @click="toggleDropdown" class="drop-container">
    <div class="selected-text">
      {{ selectedValues.length > 0 ? "Đã tùy chọn" : "Tất cả" }}
    </div>
    <div v-if="isShowCboxes" ref="dropdown" class="checkbox-drop">
      <div
        class="checkbox-container"
        v-for="option in options"
        :key="option.value"
      >
        <input
          type="checkbox"
          :id="option.value"
          :value="option.value"
          v-model="selectedValues"
        />
        <label :for="option.value">{{ option.label }}</label>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isShowCboxes: false,
      options: [
        { label: "I have a bike", value: "Bike" },
        { label: "I have a car", value: "Car" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boatADSAD", value: "Boat" },
        { label: "I have a boat asd", value: "Boat" },
        { label: "I have a boat asdasd  asdas", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
        { label: "I have a boat", value: "Boat" },
      ],
      selectedValues: [], // Array chứa các giá trị đã chọn
    };
  },
  methods: {
    toggleDropdown(event) {
      if (!event.target.closest(".checkbox-drop")) {
        this.isShowCboxes = !this.isShowCboxes;
      }
    },
    handleClickOutside(event) {
      const container = this.$refs.container;
      if (!container.contains(event.target)) {
        this.isShowCboxes = false;
      }
    },
  },
  mounted() {
    document.addEventListener("click", this.handleClickOutside);
  },
  beforeUnmount() {
    document.removeEventListener("click", this.handleClickOutside);
  },
};
</script>


<style scoped>
.drop-container {
  position: relative;
  /* background-color: cadetblue; */
  min-width: 124px;
  max-width: 100%;
  cursor: pointer;
}
.selected-text {
  font-family: roboto-medium;
  font-size: 18px;
}
.checkbox-drop {
  position: absolute;
  top: calc(100% + 12px);
  border: 1px solid rgba(0, 0, 0, 0.1);
  background-color: white;
  width: 100%;
  border-radius: 4px;
  box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.3);
  padding: 4px;
  display: flex;
  flex-direction: column;
  row-gap: 4px;
  max-height: 300px;
  width: fit-content;
  overflow-y: auto;
}

.checkbox-container label {
  white-space: nowrap;
  width: 100%;
}

.checkbox-container {
  display: flex;
  justify-content: start;
  column-gap: 6px;
  align-items: center;
  border-radius: 2px;
}
.checkbox-container:hover {
  background-color: rgba(0, 122, 255, 0.3);
}
.checkbox-container input[type="checkbox"],
.checkbox-container label {
  cursor: pointer;
}
</style>
