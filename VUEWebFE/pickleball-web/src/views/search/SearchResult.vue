<template>
  <div class="container">
    <div class="search-container">
      <SearchFrame class="custom-search-f" :initialData="searchData" />
    </div>
    <div class="result-container">
      <div class="sorting-part">
        <div class="sort-container">
          <div class="sort-title">Sắp sếp theo</div>
          <div class="radio-container">
            <input type="radio" name="sortType" id="default" checked />
            <label for="default">Mặc định</label>
          </div>
          <div class="radio-container">
            <input type="radio" name="sortType" id="priceAsc" />
            <label for="priceAsc">Giá tăng dần</label>
          </div>
          <div class="radio-container">
            <input type="radio" name="sortType" id="priceDesc" />
            <label for="priceDesc">Giá giảm dần</label>
          </div>
          <div class="radio-container">
            <input type="radio" name="sortType" id="bestRated" />
            <label for="bestRated">Đánh giá tốt nhất</label>
          </div>
        </div>
      </div>
      <div class="result-part">
        <CourtRsItem
          v-for="(court, index) in searchResults"
          :key="index"
          :courtClusterData="court"
        />
      </div>
    </div>
  </div>
</template>

<script>
import SearchFrame from "./SearchFrame.vue";
import CourtRsItem from "./CourtRsItem.vue";
import { searchCourtClusters } from "../../scripts/apiService.js";
export default {
  components: {
    SearchFrame,
    CourtRsItem,
  },
  data() {
    return {
      searchData: {
        courtClusterName: this.$route.query.courtClusterName || "",
        date: this.$route.query.date || new Date().toISOString().split("T")[0],
        startTime: this.$route.query.startTime || "",
        endTime: this.$route.query.endTime || "",
        cityName: this.$route.query.cityName || "",
        pageSize: this.$route.query.pageSize || "",
        pageIndex: this.$route.query.pageIndex || "",
        orderByColumn: this.$route.query.orderByColumn || "name",
        DESC: this.$route.query.DESC === "true" || false,
        forceUpdate: Date.now(),
      },
      searchResults: [], // dữ liệu kết quả tìm kiếm
    };
  },
  watch: {
    // Khi $route.query thay đổi thì sẽ tìm kiếm lại
    "$route.query": {
      handler() {
        this.searchData = { ...this.$route.query, forceUpdate: Date.now() }; // Cập nhật lại dữ liệu tìm kiếm
        this.fetchSearchResults(); // Gọi lại hàm tìm kiếm
      },
      immediate: true, // Gọi ngay khi component được khởi tạo
      deep: true, // Theo dõi tất cả các thuộc tính trong query
    },
  },
  methods: {
    async fetchSearchResults() {
      try {
        // Tạo bản sao của searchData để tránh ảnh hưởng đến dữ liệu gốc
        const searchQuery = { ...this.searchData };

        // Nếu startTime là "Tất cả", xóa startTime và endTime khỏi searchQuery
        if (searchQuery.startTime === "Tất cả") {
          delete searchQuery.startTime;
          delete searchQuery.endTime;
        }
        // Nếu cityName là "Tất cả", xóa
        if (searchQuery.cityName === "Tất cả") {
          delete searchQuery.cityName;
        }
        // Gửi request đến API với query parameters
        const response = await searchCourtClusters(searchQuery);
        this.searchResults = response.data;
        console.log(response);
      } catch (error) {
        console.error("Lỗi khi tải kết quả tìm kiếm:", error);
      }
    },
  },
  mounted() {},
};
</script>

<style scoped>
.container {
  padding: 12px 224px 0 224px;
  width: 100%;
}
.search-container {
  width: 100%;
  min-width: fit-content;
  display: flex;
  justify-content: center;
  align-items: center;
}
.custom-search-f {
  height: 200px;
  width: 100%;
  min-width: fit-content;
  box-sizing: border-box;
}

/* css cho phần result */

.result-container {
  width: 100%;
  height: fit-content;
  display: flex;
  flex-wrap: nowrap;
  /* background-color: seagreen; */
  column-gap: 12px;
  padding-top: 12px;
}

.sorting-part {
  /* background-color: rebeccapurple; */
  width: 20%;
  min-width: fit-content;
  height: 100%;
}
.result-part {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
  width: 100%;
}

.sort-container {
  width: 100%;
  height: fit-content;
  display: flex;
  flex-direction: column;
  row-gap: 12px;
  background: white;
  border-radius: 4px;
  box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.2);
  padding: 16px;
  border: 1px solid rgba(0, 0, 0, 0.3);
}
.sort-container .radio-container {
  display: flex;
  column-gap: 6px;
}
.sort-container .sort-title {
  font-size: 16px;
  font-family: roboto-medium;
  white-space: nowrap;
}
.sort-container label {
  white-space: nowrap;
  width: 100%;
}

@media (max-width: 1200px) {
  .result-part {
    grid-template-columns: 1fr;
  }
}

/*  */
</style>