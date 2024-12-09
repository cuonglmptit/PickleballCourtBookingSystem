<template>
  <div class="selectinput-container">
    <input
      type="text"
      class="select-input"
      v-model="searchText"
      :placeholder="placeHoder"
      @focus="toggleSuggestions(true)"
      @blur="toggleSuggestions(false)"
      @keyup="inputKeyupHandler"
    />
    <div class="list-container" v-if="showSuggestions">
      <div
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
      searchText: "", // Text nhập vào input
      selectedIndex: -1, // Chỉ số mục đang chọn
      showSuggestions: false, // Kiểm soát hiển thị danh sách gợi ý
      suggestions: [
        "Nhan vien 1",
        "Nhan vien 2",
        "Nhan vien 3",
        "Nhan vien 4",
        "A vien 5",
        "A vien 6",
        "B vien 7",
        "C vien 8",
        "D vien 9",
      ], // Dữ liệu gốc
    };
  },
  props: {
    placeHoder: String,
  },
  computed: {
    filteredSuggestions() {
      // Lọc danh sách dựa trên giá trị nhập vào
      return this.suggestions.filter((item) =>
        item.toLowerCase().includes(this.searchText.toLowerCase())
      );
    },
  },
  methods: {
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
        this.searchText = this.filteredSuggestions[index];
        this.showSuggestions = false; // Ẩn danh sách gợi ý
        this.selectedIndex = -1; // Reset chỉ số
      } catch (error) {
        console.log(error);
      }
    },
    inputKeyupHandler(event) {
      try {
        if (event.key === "ArrowUp") {
          if (this.selectedIndex >= 0) {
            this.selectedIndex--;
          }
        } else if (event.key === "ArrowDown") {
          if (this.selectedIndex < this.filteredSuggestions.length - 1) {
            this.selectedIndex++;
          } else {
            this.selectedIndex = -1;
          }
        } else if (event.key === "Enter") {
          this.isManuallySelected = true;
          this.searchText = this.filteredSuggestions[this.selectedIndex];
          this.toggleSuggestions(false);
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    searchText(newVal, oldVal) {
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
}
.list-container {
  width: 100%;
  position: absolute;
  background-color: slategray;
}
.row-selected {
  background-color: var(--topic-color-100);
}
</style>
