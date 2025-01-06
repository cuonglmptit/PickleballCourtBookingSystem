<template>
  <div class="selectinput-container">
    <input
      type="text"
      class="select-input"
      :value="modelValue"
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
        :ref="(el) => (suggestionRefs[index] = el)"
        v-for="(item, index) in filteredSuggestions"
        @mouseenter="highlight(index)"
        @mouseleave="highlight(-1)"
        @mousedown="selectSuggestion(index)"
        :key="index"
        :class="{ 'row-selected': selectedIndex === index }"
      >
        {{ item }}
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "InputSelect",
  data() {
    return {
      selectedIndex: -1, // Chỉ số mục đang chọn
      showSuggestions: false, // Kiểm soát hiển thị danh sách gợi ý
      suggestionRefs: [], // Mảng chứa các tham chiếu của từng mục
    };
  },
  props: {
    placeHoder: {
      type: String,
      default: "Nhập...",
    },
    modelValue: {
      type: String,
      required: true,
    },
    suggestions: {
      type: Array,
      required: false,
      default: () => [],
    },
  },
  emits: {
    "update:modelValue": null,
  },
  computed: {
    filteredSuggestions() {
      const query = this.removeVietnameseTones(this.modelValue.toLowerCase());
      return this.suggestions.filter((item) =>
        this.removeVietnameseTones(item.toLowerCase()).includes(query)
      );
    },
  },
  methods: {
    removeVietnameseTones(str) {
      try {
        return str
          .normalize("NFD") // Tách các ký tự đặc biệt khỏi chữ cái
          .replace(/[\u0300-\u036f]/g, "") // Loại bỏ dấu
          .replace(/đ/g, "d") // Thay chữ "đ" thành "d"
          .replace(/Đ/g, "D"); // Thay chữ "Đ" thành "D"
      } catch (error) {
        console.log(`removeVietnameseTones InputSelect: ${error}`);
      }
    },
    handleBlur() {
      try {
        if (
          this.selectedIndex >= 0 &&
          this.selectedIndex <= this.suggestions.length
        ) {
          this.$emit("update:modelValue", this.suggestions[this.selectedIndex]);
        } else {
          // Kiểm tra xem giá trị hiện tại có nằm trong danh sách gợi ý không
          if (!this.suggestions.includes(this.modelValue)) {
            if (this.filteredSuggestions.length > 0) {
              this.$emit("update:modelValue", this.filteredSuggestions[0]); // Gán giá trị đâu tiên của filteredSuggestion
            } else {
              this.$emit("update:modelValue", this.suggestions[0]); // Gán giá trị đâu tiên của filteredSuggestion
            }
          }
        }
        this.toggleSuggestions(false);
        this.isManuallySelected = true;
      } catch (error) {
        console.log(error);
      }
    },
    highlight(index) {
      try {
        this.selectedIndex = index; // Đánh dấu mục đang hover
      } catch (error) {
        console.log(error);
      }
    },
    toggleSuggestions(io) {
      try {
        this.showSuggestions = io;
        this.selectedIndex = -1;
      } catch (error) {
        console.log(error);
      }
    },
    selectSuggestion(index) {
      try {
        this.$emit("update:modelValue", this.filteredSuggestions[index]);
        this.showSuggestions = false; // Ẩn danh sách gợi ý
        this.selectedIndex = -1; // Reset chỉ số
      } catch (error) {
        console.log(error);
      }
    },
    inputKeyupHandler(event) {
      try {
        if (event.key === "ArrowUp") {
          if (this.selectedIndex > 0) {
            this.selectedIndex--;
          } else {
            this.selectedIndex = this.suggestions.length - 1;
          }
        } else if (event.key === "ArrowDown") {
          if (this.selectedIndex < this.filteredSuggestions.length - 1) {
            this.selectedIndex++;
          } else {
            this.selectedIndex = 0;
          }
        } else if (event.key === "Enter") {
          this.isManuallySelected = true;
          if (this.selectedIndex === -1) {
            this.$emit("update:modelValue", "");
          } else {
            this.$emit(
              "update:modelValue",
              this.filteredSuggestions[this.selectedIndex]
            );
          }
          this.toggleSuggestions(false);
        }
        // Cuộn danh sách theo selectedIndex
        this.scrollToSelected();
      } catch (error) {
        console.log(error);
      }
    },
    scrollToSelected() {
      try {
        const selectedElement = this.suggestionRefs[this.selectedIndex];
        if (selectedElement) {
          selectedElement.scrollIntoView({
            behavior: "smooth",
            block: "nearest",
          });
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    modelValue(newVal, oldVal) {
      if (this.isManuallySelected) {
        this.isManuallySelected = false; // Reset cờ
      } else {
        this.toggleSuggestions(true); // Hiển thị gợi ý khi giá trị thay đổi
      }
    },
  },
};
</script>
<style scoped>
.selectinput-container {
  position: relative;
}
.select-input {
  background: none;
  outline: none;
  border: none;
  font-family: roboto-medium;
  font-size: 18px;
  box-sizing: border-box;
  width: 100%;
}
.list-container {
  z-index: 1000;
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
