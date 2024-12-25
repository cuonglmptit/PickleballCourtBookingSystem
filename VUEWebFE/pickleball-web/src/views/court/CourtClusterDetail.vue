<template>
  <div class="container">
    <div class="left-part">
      <div class="left-top">
        <div class="choose-date">Chọn ngày và thời gian</div>
        <DatePicker v-model:date="courtClusterSearchData.searchDate" />
      </div>
      <div class="left-mid">
        <table class="booking-table">
          <thead>
            <tr>
              <th>Thời gian</th>
              <th v-for="field in courts" :key="field.id">
                Sân {{ field.name }}
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="timeSlot in timeSlots" :key="timeSlot.id">
              <td>{{ timeSlot.time }}</td>
              <td v-for="field in courts" :key="field.id">150k</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="left-form"></div>
      <div class="booking-form"></div>
    </div>
    <div class="right-part">
      <div class="court-title">Cụm sân hà đông 1</div>
      <div class="court-photos"></div>
      <div class="court-des">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Perferendis
        laboriosam veniam distinctio ex reiciendis quis asperiores libero quidem
        quae, repellat temporibus sapiente quos impedit fugit architecto ad,
        dolor sit illum!
      </div>
      <div class="google-maps"></div>
    </div>
  </div>
</template>

<script>
import {
  getCourtsOfCourtCluster,
  getCourtClusterById,
} from "../../scripts/apiService.js";
import DatePicker from "../../components/inputs/DatePicker.vue";

export default {
  components: {
    DatePicker,
  },
  data() {
    return {
      courtCluster: null,
      courts: [
      ],
      timeSlots: [
        { id: 1, time: "05:30-07:00" },
        { id: 2, time: "07:00-08:30" },
        { id: 3, time: "08:30-10:00" },
        { id: 4, time: "10:00-11:30" },
        { id: 5, time: "11:30-13:00" },
      ],
      selectedSlots: [], // Lưu các ô đã chọn
      courtClusterSearchData: {
        searchDate: new Date().toISOString().split("T")[0],
      },
    };
  },
  async created() {
    try {
      const courtClusterRes = await getCourtClusterById(
        this.$route.params.id
      );
      this.courtCluster = courtClusterRes.data;
      console.log(this.courtCluster);
      const courtsResponse = await getCourtsOfCourtCluster(
        this.$route.params.id
      );
      this.courts = courtsResponse.data;
      console.log(this.courts);
    } catch (error) {
      console.log(`CourtClusterDetail created(): `+error);
    }
  },
};
</script>

<style scoped>
.container {
  padding: 12px 112px 0 112px;
  display: flex;
  column-gap: 12px;
  justify-content: space-between;
  min-height: calc(100vh - 96px - 12px);
}

.left-part {
  position: relative;
  border: 1px solid rgba(0, 0, 0, 0.3);
  background-color: white;
  width: 65%;
  border-radius: 4px;
  box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.3);
  padding: 24px;
}

.left-top {
  display: flex;
  justify-content: start;
  align-content: center;
  column-gap: 32px;
}

.left-top .choose-date {
  font-family: roboto-medium;
  font-size: 24px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.left-mid {
  background-color: yellowgreen;
  height: calc(100% - 24px - 150px);
}

.booking-table {
  border: 1px solid rgba(0, 0, 0, 0.3);
  border-radius: 4px;
  border-collapse: collapse;
  padding: 12px;
  width: 100%;
  background-color: yellow;
}
.booking-table th,
.booking-table td,
.booking-table tr {
  border: 1px solid rgba(0, 0, 0, 0.3);
  /* border-bottom: 1px solid #ddd; */
}

.booking-form {
  width: calc(100% - 2 * 24px);
  height: 150px;
  background-color: white;
  border: 1px solid var(--topic-color-200);
  box-shadow: 2px 2px 8px var(--topic-color-200);
  border-radius: 4px;
  position: absolute;
  bottom: 12px;
}

/* CSS phần bên phải */
.right-part {
  border: 1px solid rgba(0, 0, 0, 0.3);
  background-color: white;
  flex: 1;
  border-radius: 4px;
  box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.3);
  padding: 24px;
  display: flex;
  flex-direction: column;
  row-gap: 12px;
  justify-content: start;
}

.court-title {
  font-family: roboto-medium;
  font-size: 24px;
}

.court-photos {
  height: 35%;
  width: 100%;
  background-color: whitesmoke;
}

.google-maps {
  background-color: wheat;
  height: 35%;
}
</style>