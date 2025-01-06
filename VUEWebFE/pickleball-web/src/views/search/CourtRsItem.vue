<template>
  <div class="court-rs-container">
    <div class="court-header">
      <div class="court-title">
        <!-- <button class="delete-btn p-icon-delete-btn" v-if="manageMode"></button> -->
        {{ courtClusterData.name }}
      </div>
      <div class="court-title">Đánh giá 5/5</div>
    </div>
    <div class="court-content">
      <div class="court-thumbnail">
        <img src="../../assets/img/picklleball_court_3.webp" alt="" />
      </div>
      <div class="court-brief">
        <div class="court-inf">
          <div v-if="courtClusterAddress">
            {{ courtClusterAddress.street }},
            {{ courtClusterAddress.district }},
            {{ courtClusterAddress.city }}
          </div>
          <div v-if="courtClusterData.description">
            {{ courtClusterData.description }}
          </div>
          <div
            v-if="courtClusterData.openingTime && courtClusterData.closingTime"
          >
            {{ courtClusterData.openingTime }} -
            {{ courtClusterData.closingTime }}
          </div>
        </div>
        <router-link
          :to="
            manageMode === true
              ? {
                  name: 'manage-court-cluster-detail',
                  params: { id: courtClusterData.id },
                }
              : {
                  name: 'court-cluster-detail',
                  params: { id: courtClusterData.id },
                }
          "
          class="court-action-button"
        >
          Chi tiết
          <div class="action-icon p-icon-right-round-arrow"></div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { getAddressByid } from "../../scripts/apiService.js";

export default {
  props: {
    courtClusterData: {
      type: Object,
      reqired: true,
    },
    manageMode: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      courtClusterAddress: null,
    };
  },
  async created() {
    try {
      const response = await getAddressByid(this.courtClusterData.addressId);
      this.courtClusterAddress = response.data;
      // console.log(this.courtClusterAddress);
    } catch (error) {
      console.log(`CourtRsItem created: ` + error);
    }
  },
};
</script>

<style scoped>
.court-rs-container {
  width: 100%;
  height: 350px;
  min-width: fit-content;
  display: flex;
  align-items: center;
  flex-direction: column;
  border-radius: 4px;
  background-color: white;
  box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.2);
  padding: 12px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  row-gap: 12px;
  /* background-color: tomato; */
}

.court-header {
  display: flex;
  justify-content: space-between;
  width: 100%;
  flex-wrap: nowrap;
  height: 24px;
  /* background-color: cornsilk; */
}

.court-header .court-title {
  font-size: 20px;
  white-space: nowrap;
  font-family: roboto-bold;
}

.court-content {
  display: flex;
  width: 100%;
  min-width: fit-content;
  column-gap: 12px;
  height: calc(100% - 24px - 12px);
  /* background-color: palevioletred; */
}

.court-inf {
  display: flex;
  flex-direction: column;
  row-gap: 12px;
}

.court-inf div {
  display: -webkit-box;
  -webkit-line-clamp: 5;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
}

.court-thumbnail {
  min-width: 250px;
  width: 65%;
  height: 100%;
  border-radius: 4px;
  overflow: hidden;
  /* background-color: skyblue; */
}

.court-thumbnail img {
  object-fit: cover;
  width: 100%;
  height: 100%;
}

.court-brief {
  display: flex;
  flex-direction: column; /* Nội dung xếp theo cột */
  justify-content: space-between; /* Căn chỉnh nội dung */
  overflow: hidden;
  width: 35%;
}

.court-action-button {
  border-radius: 4px;
  border: none;
  height: 32px;
  width: 100%;
  background-color: var(--topic-color-500);
  color: white;
  font-family: roboto-medium;
  font-size: 16px;
  cursor: pointer;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
  align-items: center;
  column-gap: 4px;
  white-space: nowrap;
  padding: 0 4px 0 4px;
  text-decoration: none;
}

.action-icon {
  width: 24px;
  height: 24px;
  background-size: 24px;
  background-repeat: no-repeat;
  flex-shrink: 0;
  filter: invert(100%) sepia(100%) saturate(0%) hue-rotate(315deg)
    brightness(104%) contrast(101%);
}

.delete-btn {
  width: 24px;
  height: 24px;
  background-size: 24px;
  background-repeat: no-repeat;
  background-position: center;
  cursor: pointer;
  border: none;
  outline: none;
}
</style>