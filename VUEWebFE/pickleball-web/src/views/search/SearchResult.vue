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
            <input type="radio" name="sortType" id="default" />
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
        <CourtRsItem />
        <CourtRsItem />
        <CourtRsItem />
        <CourtRsItem />
        <CourtRsItem />
      </div>
    </div>
  </div>
</template>

<script>
import SearchFrame from "./SearchFrame.vue";
import CourtRsItem from "./CourtRsItem.vue";
export default {
  components: {
    SearchFrame,
    CourtRsItem,
  },
  computed: {
    searchData() {
      return {
        keyword: this.$route.query.keyword || "",
        date: this.$route.query.date || "",
      };
    },
  },
};
</script>

<style scoped>
.container {
  padding: 12px 224px 0 224px;
}
.search-container {
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.custom-search-f {
  height: 200px;
  width: 100%;
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