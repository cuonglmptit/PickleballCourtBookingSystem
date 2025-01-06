<template>
  <div class="selectinput-container">
    <input
      type="text"
      class="select-input"
      :value="displayValue"
      @input="$emit('update:modelValue', $event.target.value)"
      :placeholder="placeHoder"
      @focus="toggleSuggestions(true)"
      @blur="handleBlur()"
      @keyup="inputKeyupHandler"
    />
    <div
      class="list-container"
      v-if="showSuggestions && filteredSuggestions.length > 0"
    >
      <div
        class="suggestion-row"
        v-for="(item, index) in filteredSuggestions"
        :key="index"
        @mouseenter="highlight(index)"
        @mouseleave="highlight(-1)"
        @mousedown="selectSuggestion(item)"
        :class="{ 'row-selected': selectedIndex === index }"
      >
        {{ item[labelKey] }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "InputSelect",
  props: {
    placeHoder: String,
    modelValue: [Object, String, Number], // Cho phép object nếu cần
    suggestions: Array,
    labelKey: {
      type: String,
      default: "name",
    },
  },
  emits: ["update:modelValue"],
  computed: {
    displayValue() {
      return this.modelValue ? this.modelValue[this.labelKey] : "";
    },
    filteredSuggestions() {
      return this.suggestions.filter((item) =>
        item[this.labelKey]
          .toLowerCase()
          .includes(this.modelValue.toLowerCase())
      );
    },
  },
  methods: {
    selectSuggestion(item) {
      this.$emit("update:modelValue", item);
      this.showSuggestions = false;
    },
    toggleSuggestions(show) {
      this.showSuggestions = show;
    },
    highlight(index) {
      this.selectedIndex = index;
    },
    handleBlur() {
      this.showSuggestions = false;
    },
    inputKeyupHandler(event) {
      // Xử lý khi nhấn các phím mũi tên và enter
    },
  },
};
</script>
<style scoped>
.selectinput-container {
  position: relative;
  width: 100%;
}
.select-input {
  display: flex;
  justify-content: center;
  border-radius: 4px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  padding: 2px 12px;
  width: 100%;
}
.list-container {
  width: 100%;
  position: absolute;
  top: 100%;
  background-color: white;
  border: 1px solid rgba(0, 0, 0, 0.3);
  box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
  border-radius: 4px;
  max-height: 300px;
  overflow: auto;
  padding: 4px;
}

.suggestion-row {
  padding: 4px;
  font-size: 15px;
  cursor: pointer;
  border-radius: 2px;
}

.row-selected {
  background-color: var(--topic-color-200);
}
</style>