<template>
  <div class="search">
    <div class="inputs-part">
      <TranparentSearch />
      <div class="filters">
        <div class="filter-container">
          Khung giờ muốn đặt
          <div class="filter-icon-c">
            <div class="f-icon p-icon-clock"></div>
            <DropCheckboxes />
          </div>
        </div>
        <div class="filter-container">
          Ngày chơi
          <DatePicker />
        </div>
        <div class="filter-container">
          Khu vực
          <div class="filter-icon-c">
            <div class="f-icon p-icon-location"></div>
            <InputSelect :placeHoder="'Nhập khu vực'" />
          </div>
        </div>
      </div>
    </div>
    <div class="buttons-part">
      <div class="search-btn-container">
        <TriangleButton @click="submitSearch">
          <template v-slot:name> Tìm kiếm </template>
        </TriangleButton>
      </div>
    </div>
  </div>
</template>

<script>
import TranparentSearch from "../../components/inputs/TranparentSearch.vue";
import DatePicker from "../../components/inputs/DatePicker.vue";
import DropCheckboxes from "../../components/inputs/DropCheckboxes.vue";
import InputSelect from "../../components/inputs/InputSelect.vue";
import TriangleButton from "../../components/buttons/TriangleButton.vue";
export default {
  components: {
    TranparentSearch,
    DatePicker,
    DropCheckboxes,
    InputSelect,
    TriangleButton,
  },
  methods: {
    submitSearch() {
      this.$router.push({
        name: "search-result",
        query: {
          keyword: this.searchData.keyword,
          date: this.searchData.date,
        },
      });
    },
  },
  data() {
    return {
      searchData: {
        keyword: "",
        date: "",
      },
    };
  },
};
</script>

<style scoped>
.search {
  background-color: white;
  width: 70%;
  height: 224px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  border-radius: 4px;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.5);
  display: flex;
  min-width: fit-content;
}
.inputs-part {
  width: 70%;
  /* background-color: yellowgreen; */
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  min-width: fit-content;
  padding: 0 36px;
}
.buttons-part {
  /* background-color: steelblue; */
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 30%;
  min-width: fit-content;
  padding: 36px;
}
.search-btn-container {
  width: 100%;
}

.filters {
  display: flex;
  justify-content: space-between;
  width: 100%;
  /* background-color: bisque; */
  align-items: center;
  box-sizing: border-box;
  column-gap: 24px;
}
.filter-container {
  height: 100%;
  display: flex;
  row-gap: 4px;
  justify-content: space-between;
  flex-direction: column;
  width: 100%;
}
.filter-icon-c {
  display: flex;
  justify-content: start;
  align-items: center;
  column-gap: 4px;
}
.f-icon {
  width: 36px;
  height: 36px;
  background-repeat: no-repeat;
  background-size: 36px;
}
</style>