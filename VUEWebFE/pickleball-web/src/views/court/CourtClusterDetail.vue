<template>
  <div class="container">
    <div class="left-part">
      <div class="left-top">
        <div class="choose-date">Chọn ngày và thời gian:</div>
        <DatePicker v-model:date="date" />
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
            <tr
              v-for="(time, index) in getSortedUniqueTimes(timeSlotsData)"
              :key="index"
            >
              <td class="text-align-center">{{ time }}</td>
              <td
                class="text-align-center"
                v-for="court in courts"
                :key="court.id"
                :class="{
                  'slot-unavailable': !findTimeSlot(
                    timeSlotsData,
                    court.id,
                    time
                  ).isAvailable,
                  'slot-available': findTimeSlot(timeSlotsData, court.id, time)
                    .isAvailable,
                }"
                :disabled="
                  !findTimeSlot(timeSlotsData, court.id, time).isAvailable
                "
                @click="
                  clickCourtTimeSlot(
                    $event,
                    court.id,
                    findTimeSlot(timeSlotsData, court.id, time)
                  )
                "
              >
                {{
                  formatCurrency(
                    findTimeSlot(timeSlotsData, court.id, time).price
                  )
                }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="booking-form">
        <div class="font-size-18 roboto-bold">Đã chọn:</div>
        <div class="form-selected-timeslot" v-if="selectedSlots.length">
          <!-- Hiển thị danh sách các ô đã chọn -->
          <div
            v-for="slot in selectedSlots"
            :key="slot.courtId + slot.time"
            class="font-size-18"
          >
            Sân {{ findCourt(courts, slot.courtId).courtNumber }}:
            {{ slot.time }}
            {{ date }}
          </div>
        </div>
        <div class="form-summary">
          <div class="summary-money">
            <div class="font-size-18 roboto-bold">Tổng:</div>
            <div class="font-size-18">{{ formatCurrency(totalAmount) }}</div>
          </div>
          <TriangleButton @click="handleBooking">
            <template v-slot:name> Đặt sân </template>
          </TriangleButton>
        </div>
      </div>
    </div>
    <div class="right-part">
      <div class="court-title">{{ courtCluster.name }}</div>
      <div class="court-photos">
        <img :src="courtClusterPhotoUrl" alt="abc" />
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
  getTimeSlotOfCourt,
  getImageCourtUrl,
  addBooking,
} from "../../scripts/apiService.js";
import DatePicker from "../../components/inputs/DatePicker.vue";
import TriangleButton from "../../components/buttons/TriangleButton.vue";
import Swal from "sweetalert2";
import store from "@/store";

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
      timeSlotsData: [],
      slotsState: [], // Trạng thái các slot, mảng hai chiều
      selectedSlots: [], // Lưu các ô đã chọn
      date: new Date().toISOString().split("T")[0],
      courtClusterPhotoUrl: null,
      selectedCourtId: null, // Lưu trữ ID của sân đang được chọn
    };
  },
  computed: {
    totalAmount() {
      return this.selectedSlots.reduce((sum, slot) => {
        return sum + (slot.price || 0); // Cộng giá trị price nếu có, nếu không thì cộng 0
      }, 0);
    },
  },
  async created() {
    try {
      await this.loadData();

      // Cập nhật Google Map
      this.updateGoogleMap();
    } catch (error) {
      console.log(`CourtClusterDetail created(): ` + error);
    }
  },
  watch: {
    // Khi $route.query thay đổi thì sẽ tìm kiếm lại
    date(newDate, oldDate) {
      this.loadData(this.courts, this.date);
    },
  },
  methods: {
    async loadCourtTimeSlot(courts, date) {
      const promises = courts.map((court) =>
        getTimeSlotOfCourt(court.id, date)
      );
      const results = await Promise.all(promises);
      // Trích xuất `.data` từ mỗi phần tử trong `results` và gộp tất cả thành một mảng
      const mergedArray = results.map((result) => result.data).flat();

      console.log(mergedArray);
      return mergedArray;
    },
    findTimeSlot(timeSlots, courtId, time) {
      return timeSlots.find(
        (slot) => slot.courtId === courtId && slot.time === time
      );
    },
    findCourt(courts, courtId) {
      return courts.find((court) => court.id === courtId);
    },
    getSortedUniqueTimes(timeSlots) {
      const uniqueTimes = [
        ...new Set(
          timeSlots
            .filter((slot) => slot && slot.time !== null) // Kiểm tra slot không phải null và time không phải null
            .map((slot) => slot.time)
        ),
      ];
      return uniqueTimes.sort();
    },
    async loadData() {
      //fetch data
      const courtClusterRes = await getCourtClusterById(this.$route.params.id);
      if (courtClusterRes.success) {
        this.courtCluster = courtClusterRes.data;
      }
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
      this.timeSlotsData = await this.loadCourtTimeSlot(this.courts, this.date);
      //Load ảnh
      console.log(this.courtCluster);
      const imgres = await getImageCourtUrl(this.courtCluster.id);
      if (imgres.success) {
        this.courtClusterPhotoUrl = imgres.data[0] ? imgres.data[0].url : "";
      }
      console.log(imgres);
    },
    clickCourtTimeSlot(event, courtId, timeSlot) {
      try {
        if (!this.handleAuthenticated()) {
          return;
        }
        const target = event.currentTarget;

        // Nếu sân khác được chọn, xóa các lựa chọn cũ
        if (this.selectedCourtId !== courtId) {
          this.clearAllSelectedSlots(); // Hàm này sẽ xóa các lựa chọn cũ
          this.selectedCourtId = courtId; // Cập nhật sân hiện tại
        }
        // Kiểm tra xem timeSlot đã có trong selectedSlots chưa
        const isSlotSelected = this.selectedSlots.some(
          (slot) =>
            slot.courtId === timeSlot.courtId && slot.time === timeSlot.time
        );

        if (!isSlotSelected) {
          // Nếu chưa chọn, thêm timeSlot vào selectedSlots
          this.selectedSlots.push(timeSlot);
        } else {
          // Nếu đã chọn, bỏ chọn (thực hiện xóa khỏi selectedSlots)
          const index = this.selectedSlots.findIndex(
            (slot) =>
              slot.courtId === timeSlot.courtId && slot.time === timeSlot.time
          );
          if (index !== -1) {
            this.selectedSlots.splice(index, 1);
          }
        }

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
    async handleBooking() {
      if (!this.handleAuthenticated()) {
        return;
      }
      if (this.selectedSlots.length === 0) {
        Swal.fire("Vui lòng chọn sân trước khi đặt.", "", "error");
        return;
      }
      const result = await Swal.fire({
        title: "Bạn có chắc muốn đặt lịch?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Hủy",
      });
      if (result.isConfirmed) {
        try {
          let slotsIds = this.selectedSlots.map((slot) => slot.id);
          let courtId = this.selectedSlots[0].courtId;
          const bookingRes = await addBooking(courtId, slotsIds);
          if (bookingRes.success) {
            Swal.fire("Thành công", "", "success");
            this.selectedSlots = [];
          } else {
            Swal.fire("Thất bại", "", "error");
          }
          console.log(bookingRes);
          this.loadData();
        } catch (error) {
          console.log(`handleBooking: ${error}`);
        }
      }
    },
    handleAuthenticated() {
      if (!store.getters["isAuthenticated"]) {
        Swal.fire(
          "Bạn cần đăng nhập để có thể thực hiện hành động này!",
          "",
          "warning"
        );
        this.$router.push("/login");
        return false;
      }
      return true;
    },
    clearAllSelectedSlots() {
      // Tìm tất cả các ô đã được chọn và bỏ chọn chúng
      const selectedSlots = this.$el.querySelectorAll(".slot-selected");
      selectedSlots.forEach((slot) => {
        slot.classList.remove("slot-selected");
      });
      this.selectedSlots = [];
    },
    formatCurrency(amount) {
      return new Intl.NumberFormat("vi-VN", {
        style: "currency",
        currency: "VND",
      }).format(amount);
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
  overflow-y: scroll;
}
.form-selected-timeslot div {
  background-color: orange;
  padding: 4px;
  margin: 0px 12px;
  border-radius: 2px;
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
  background-color: orange; /* Màu xanh đậm hơn */
  cursor: pointer;
}

/* Slot không khả dụng */
.slot-unavailable {
  background-color: #d3d3d3;
  color: #a9a9a9;
  pointer-events: none;
  cursor: not-allowed;
}
</style>