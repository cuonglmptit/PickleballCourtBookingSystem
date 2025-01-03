<template>
  <div class="search">
    <div class="inputs-part">
      <TranparentSearch
        v-model:keyword="searchData.courtClusterName"
        :placeholder="'Tên sân...'"
      />
      <div class="filters">
        <div class="filter-container">
          Khung giờ muốn tìm
          <div class="filter-icon-c">
            <div class="f-icon p-icon-clock"></div>
            <InputSelect
              v-model="searchData.startTime"
              :suggestions="['Tất cả', ...timeSuggestionList]"
              :placeHoder="'Giờ bắt đầu'"
            />
            <div class="separate"></div>
            <InputSelect
              v-if="searchData.startTime !== 'Tất cả'"
              v-model="searchData.endTime"
              :suggestions="filteredEndTimeList"
              :placeHoder="'Giờ kết thúc'"
            />
          </div>
        </div>
        <div class="filter-container">
          Ngày chơi
          <DatePicker v-model:date="searchData.date" />
        </div>
        <div class="filter-container">
          Khu vực
          <div class="filter-icon-c">
            <div class="f-icon p-icon-location"></div>
            <InputSelect
              v-model="searchData.cityName"
              :suggestions="['Tất cả', ...provinesSuggestionList]"
              :placeHoder="'Nhập khu vực'"
            />
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
// import DropCheckboxes from "../../components/inputs/DropCheckboxes.vue";
import InputSelect from "../../components/inputs/InputSelect.vue";
import TriangleButton from "../../components/buttons/TriangleButton.vue";
import { getListTime, getProvinces } from "../../scripts/apiService.js";
export default {
  components: {
    TranparentSearch,
    DatePicker,
    // DropCheckboxes,
    InputSelect,
    TriangleButton,
  },
  async created() {
    try {
      //Lấy gợi ý list time
      const listTimeResposne = await getListTime();
      this.timeSuggestionList = listTimeResposne.data;
      this.updateEndTimeList(this.searchData.startTime);
      // console.log(this.timeSuggestionList);

      //Lấy gợi ý list khu vực
      const listProvinesRes = await getProvinces();
      this.provinesSuggestionList = listProvinesRes.data.map(
        (item) => item.name
      );
      // console.log(this.provinesSuggestionList);
    } catch (error) {
      console.log(error);
    }
  },
  watch: {
    "searchData.startTime"(newStartTime) {
      this.updateEndTimeList(newStartTime);
      if (newStartTime === "Tất cả") {
        this.searchData.endTime = ""; // Reset endTime nếu startTime là 'Tất cả'
      } else {
        this.updateEndTimeList(newStartTime);
        // Kiểm tra xem endTime có lớn hơn startTime không, nếu không, reset endTime
        if (
          newStartTime &&
          this.searchData.endTime &&
          this.searchData.endTime <= newStartTime
        ) {
          this.searchData.endTime = ""; // Xóa giá trị endTime nếu không hợp lệ
        }
      }
    },
  },
  methods: {
    submitSearch() {
      console.log("Dữ liệu tìm kiếm:", this.searchData);
      this.searchData.forceUpdate = Date.now();
      this.$router.push({
        name: "search-result",
        query: { ...this.searchData },
      });
    },
    updateEndTimeList(startTime) {
      try {
        if (startTime) {
          // Lọc thời gian để chỉ giữ lại những thời gian lớn hơn startTime
          this.filteredEndTimeList = this.timeSuggestionList.filter(
            (time) => time > startTime
          );
        } else {
          this.filteredEndTimeList = [...this.timeSuggestionList];
        }
      } catch (error) {
        console.log(`err updateEndTimeList: ${error}`);
      }
    },
  },
  updated() {
    // console.log(this.searchData);
  },
  props: {
    initialData: {
      type: Object,
      default: () => ({
        courtClusterName: "",
        date: new Date().toISOString().split("T")[0],
        startTime: "Tất cả",
        endTime: "",
        cityName: "Tất cả",
        pageSize: "",
        pageIndex: "",
        orderByColumn: "",
        DESC: "",
        forceUpdate: Date.now(),
      }),
    },
  },
  data() {
    return {
      searchData: { ...this.initialData },
      timeSuggestionList: [],
      filteredEndTimeList: [],
      provinesSuggestionList: [],
    };
  },
};
</script>

<style scoped>
.search {
  background-color: white;
  width: 100%;
  height: 224px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  border-radius: 4px;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.5);
  display: flex;
}
.inputs-part {
  width: 70%;
  /* background-color: yellowgreen; */
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  /* min-width: fit-content; */
  padding: 0 36px;
}
.buttons-part {
  /* background-color: steelblue; */
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 30%;
  /* min-width: fit-content; */
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
  flex-wrap: nowrap;
  align-items: center;
  column-gap: 4px;
  flex-shrink: 0;
}

.f-icon {
  width: 36px;
  height: 36px;
  background-repeat: no-repeat;
  background-size: 36px;
  flex-shrink: 0;
}

.separate {
  background-color: rgba(0, 0, 0, 0.6);
  flex-shrink: 0;
  width: 2px;
  margin-right: 12px;
  height: 60%;
}
</style>