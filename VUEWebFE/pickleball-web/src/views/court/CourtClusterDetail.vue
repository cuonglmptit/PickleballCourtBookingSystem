<template>
  <div class="container">
    <div class="left-part">
      <div class="left-top">
        <div class="choose-date">Chọn ngày và thời gian:</div>
        <DatePicker v-model:date="courtClusterSearchData.searchDate" />
      </div>
      <div class="left-mid">
        <table class="booking-table">
          <thead>
            <tr>
              <th style="width: 156px">Thời gian</th>
              <th
                class="court-th p-icon-court-active-hori vertical-align-top"
                v-for="court in courts"
                :key="court.id"
              >
                Sân {{ court.courtNumber }}
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(timeSlot, index) in timeSlots" :key="index">
              <td class="text-align-center">{{ timeSlot }}</td>
              <td
                class="text-align-center slot-available"
                v-for="court in courts"
                :key="court.id"
                @click="clickCourtTimeSlot($event)"
              >
                150k
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="booking-form">
        <div class="font-size-18 roboto-bold">Đã chọn:</div>
        <div class="form-selected-timeslot">
          <div class="font-size-18">Sân 1: 06:00 26/12/2024</div>
          <div class="font-size-18">Sân 1: 07:00 26/12/2024</div>
        </div>
        <div class="form-summary">
          <div class="summary-money">
            <div class="font-size-18 roboto-bold">Tổng:</div>
            <div class="font-size-18">300.000đ</div>
          </div>
          <TriangleButton>
            <template v-slot:name> Đặt sân </template>
          </TriangleButton>
        </div>
      </div>
    </div>
    <div class="right-part">
      <div class="court-title">{{ courtCluster.name }}</div>
      <div class="court-photos">
        <img src="../../assets/img/picklleball_court_4.webp" alt="abc" />
      </div>
      <div class="court-des">
        {{ courtCluster.description }}
      </div>
      <div class="court-title">Địa chỉ</div>
      <div v-if="courtClusterAddress">
        {{ courtClusterAddress.street }}, {{ courtClusterAddress.district }},
        {{ courtClusterAddress.city }}
      </div>
      <div class="google-maps"></div>
    </div>
  </div>
</template>

<script>
import {
  getCourtsOfCourtCluster,
  getCourtClusterById,
  getAddressByid,
} from "../../scripts/apiService.js";
import DatePicker from "../../components/inputs/DatePicker.vue";
import TriangleButton from "../../components/buttons/TriangleButton.vue";

export default {
  components: {
    DatePicker,
    TriangleButton,
  },
  data() {
    return {
      courtCluster: {},
      courtClusterAddress: null,
      courts: [],
      timeSlots: [
        "00:00",
        "01:00",
        "02:00",
        "03:00",
        "04:00",
        "05:00",
        "06:00",
        "07:00",
        "08:00",
        "09:00",
        "10:00",
        "11:00",
        "12:00",
        "13:00",
        "14:00",
        "15:00",
        "16:00",
        "17:00",
        "18:00",
        "19:00",
        "20:00",
        "21:00",
        "22:00",
        "23:00",
      ],
      slotsState: [], // Trạng thái các slot, mảng hai chiều
      selectedSlots: [], // Lưu các ô đã chọn
      courtClusterSearchData: {
        searchDate: new Date().toISOString().split("T")[0],
      },
      courtClusterPhotoUrls:[
        
      ]
    };
  },
  async created() {
    try {
      //fetch data
      const courtClusterRes = await getCourtClusterById(this.$route.params.id);
      this.courtCluster = courtClusterRes.data;
      console.log(this.courtCluster);
      const courtsResponse = await getCourtsOfCourtCluster(
        this.$route.params.id
      );
      this.courts = courtsResponse.data.sort(
        (a, b) => a.courtNumber - b.courtNumber
      );

      // console.log(this.courts);
      const addressRes = await getAddressByid(this.courtCluster.addressId);
      this.courtClusterAddress = addressRes.data;

      // Cập nhật Google Map
      this.updateGoogleMap();
    } catch (error) {
      console.log(`CourtClusterDetail created(): ` + error);
    }
  },

  methods: {
    clickCourtTimeSlot(event) {
      try {
        const target = event.currentTarget;

        // Kiểm tra xem ô này đã được chọn chưa
        if (target.classList.contains("slot-selected")) {
          // Nếu đã chọn, bỏ chọn
          target.classList.remove("slot-selected");
        } else {
          // Nếu chưa chọn, thêm trạng thái được chọn
          target.classList.add("slot-selected");
        }
      } catch (error) {
        console.log(`clickCourtTimeSlot CourtClusterDetail ${error}`);
      }
    },
    //Gán google map
    updateGoogleMap() {
      try {
        const address = `${this.courtClusterAddress.street}, ${this.courtClusterAddress.district}, ${this.courtClusterAddress.city}`;
        const encodedAddress = encodeURIComponent(address);
        const embedUrl = `https://maps.google.com/maps?&q=${encodedAddress}&output=embed`;

        // Gắn iframe vào phần tử với class 'google-maps'
        const googleMapsElement = this.$el.querySelector(".google-maps");
        if (googleMapsElement) {
          googleMapsElement.innerHTML = `<iframe width="100%" height="100%" frameborder="0" 
          scrolling="no" marginheight="0" marginwidth="0" 
          src="${embedUrl}"></iframe>`;
        }
      } catch (error) {
        console.log(`updateGoogleMap ${error}`);
      }
    },
  },
};
</script>

<style scoped>
.container {
  padding: 12px 112px 0 112px;
  display: flex;
  column-gap: 12px;
  justify-content: space-between;
  height: calc(100vh - 96px - 12px);
}

.left-part {
  position: relative;
  border: 1px solid rgba(0, 0, 0, 0.3);
  background-color: white;
  width: 65%;
  border-radius: 4px;
  box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.3);
  padding: 24px;
  height: 100%;
}

.left-top {
  display: flex;
  justify-content: start;
  align-content: center;
  column-gap: 32px;
  margin-bottom: 6px;
}

.left-top .choose-date {
  font-family: roboto-medium;
  font-size: 24px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.left-mid {
  /* background-color: yellowgreen; */
  height: calc(100% - 24px - 150px - 8px - 2px);
  box-sizing: border-box;
  overflow: auto;
  border: 1px solid rgba(0, 0, 0, 0.3);
  border-radius: 4px;
}

/* CSS form */
.booking-form {
  width: calc(100% - 2 * 24px);
  height: 150px;
  background-color: white;
  border: 1px solid var(--topic-color-200);
  box-shadow: 2px 2px 4px var(--topic-color-300);
  border-radius: 4px;
  position: absolute;
  bottom: 12px;
  display: flex;
  justify-content: space-between;
  flex-wrap: nowrap;
  padding: 12px 32px 12px 32px;
  overflow: hidden;
}
.form-selected-timeslot {
  display: flex;
  flex-direction: column;
  row-gap: 6px;
}

.form-summary {
  display: flex;
  justify-content: space-between;
  flex-direction: column;
  width: 30%;
}

.summary-money {
  display: flex;
  flex-wrap: nowrap;
  justify-content: space-between;
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
/* CSS ảnh preview court photos */
.court-photos {
  height: 35%;
  width: 100%;
  background-color: whitesmoke;
}

.court-photos img {
  object-fit: cover;
  width: 100%;
  height: 100%;
}

.google-maps {
  background-color: wheat;
  flex: 1;
}

/* CSS table */

.booking-table {
  border-collapse: collapse;
  border-spacing: unset;
  box-sizing: border-box;
  border: unset;
  font-size: 14px;
  overflow: scroll;
  background-color: white;
  border-spacing: unset;
  width: 100%;
}
.booking-table th,
.booking-table td,
.booking-table tr {
  border: 1px solid rgba(0, 0, 0, 0.3);
  /* border-bottom: 1px solid #ddd; */
}

.court-th {
  background-size: 56px;
  background-repeat: no-repeat;
  background-position-x: center;
  background-position-y: calc(6px + 18px);
}

.booking-table th {
  background-color: var(--gray-background);
  height: 86px;
  font-size: 18px;
  padding-top: 6px;
  position: sticky;
  top: -1px;
}

.booking-table tr {
  height: 30px;
}

.booking-table tr td {
  font-size: 15px;
}

/* Slot khả dụng */
.slot-available {
  background-color: #e8f5e9; /* Màu xanh nhạt */
  cursor: pointer;
}

/* Slot được chọn */
.slot-selected {
  background-color: lightsalmon; /* Màu xanh đậm hơn */
  cursor: pointer;
}

/* Slot không khả dụng */
.slot-unavailable {
  background-color: #ffcdd2; /* Màu đỏ nhạt */
  cursor: not-allowed;
  pointer-events: none; /* Vô hiệu hóa click */
}
</style>