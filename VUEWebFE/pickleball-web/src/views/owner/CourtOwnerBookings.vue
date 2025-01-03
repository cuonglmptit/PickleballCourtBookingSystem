<template>
  <div class="container">
    <div class="navigation">
      <router-link class="nav-option" :to="{ name: 'manage-court-cluster' }"
        >Quản lý sân
      </router-link>
      <router-link class="nav-option" :to="{ name: 'owner-manage-booking' }"
        >Quản lý booking
      </router-link>
    </div>
    <div class="content">
      <div class="booking-management">
        <div class="filters">
          <input
            v-model="searchQuery"
            type="text"
            placeholder="Tìm kiếm booking..."
            class="search-input"
          />
          <div>
            Lọc theo:
            <select v-model="statusFilter" class="status-select">
              <option value="">Tất cả</option>
              <option value="Pending">Đang chờ</option>
              <option value="CourtOwnerConfirmed">Đã xác nhận</option>
              <option value="Paid">Đã thanh toán</option>
              <option value="Unpaid">Chưa thanh toán</option>
            </select>
          </div>
        </div>
        <table class="booking-table">
          <thead>
            <tr>
              <th>STT</th>
              <th>Mã booking</th>
              <th>Thời gian đặt</th>
              <th>Trạng thái</th>
              <th>Thanh toán</th>
              <th>Cụm sân</th>
              <th>Sân số</th>
              <th>Tổng tiền</th>
              <th>Thao tác</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(booking, index) in filteredBookings" :key="booking.id">
              <td>{{ (currentPage - 1) * itemsPerPage + index + 1 }}</td>
              <td>{{ booking.code }}</td>
              <td>{{ formatDate(booking.timeBooking) }}</td>
              <td>{{ formatStatus(booking.status) }}</td>
              <td>{{ formatStatus(booking.paymentStatus) }}</td>
              <td>{{ getCourtClusterName(booking.courtClusterId) }}</td>
              <td>{{ getCourtNumber(booking.courtId) }}</td>
              <td>{{ formatCurrency(booking.amount) }}</td>
              <td>
                <button class="detail-btn" @click="viewDetails(booking)">
                  Chi tiết
                </button>
                <button class="accept-btn" @click="acceptBooking(booking)">
                  Đồng ý
                </button>
                <button class="reject-btn" @click="rejectBooking(booking)">
                  Từ chối
                </button>
              </td>
            </tr>
          </tbody>
        </table>
        <div class="pagination">
          <button
            :disabled="currentPage === 1"
            @click="changePage(currentPage - 1)"
          >
            Trước
          </button>
          <span>Trang {{ currentPage }} / {{ totalPages }}</span>
          <button
            :disabled="currentPage === totalPages"
            @click="changePage(currentPage + 1)"
          >
            Tiếp
          </button>
        </div>
      </div>
    </div>
  </div>
  <div class="overlay" @click="hideForm" v-if="isFormVisible">
    <form class="detail-form" action="">
      <div class="left-form">
        <div class="big-title">Chi tiết lịch đặt</div>
        <div class="detail">
          <div>Mã booking: {{ selectedBooking.code }}</div>
          <div>
            Thời gian đặt: {{ formatDate(selectedBooking.timeBooking) }}
          </div>
          <div>
            Cụm sân: {{ getCourtClusterName(selectedBooking.courtClusterId) }}
          </div>
          <div>Địa chỉ: Đường Nguyễn Trãi, Hà Đông, Hà Nội</div>
          <ul>
            <li
              v-for="courtTimeSlot in seletedCourTimeSlotsByBookingId"
              :key="courtTimeSlot.id"
            >
              Sân {{ getCourtNumber(courtTimeSlot.courtId) }}:
              {{
                courtTimeSlot.time +
                " - " +
                formatDate(courtTimeSlot.date).split(" ")[1]
              }}
            </li>
          </ul>
          <div style="color: var(--topic-color4-500); font-size: 24px">
            Tổng: {{ formatCurrency(selectedBooking.amount) }}
          </div>
          <br />
          <div>Thông tin người đặt:</div>
          <div>Tên: {{ selectedBookingCustomerInfo.name }}</div>
          <div>Sđt: {{ selectedBookingCustomerInfo.phoneNumber }}</div>
          <div>Email: {{ selectedBookingCustomerInfo.email }}</div>
        </div>
      </div>
      <div class="right-form">
        <div>
          <div class="big-title">Trạng thái</div>
          <div class="big-title" style="color: orange">
            {{ formatStatus(selectedBooking.status) }}
          </div>
        </div>
        <div>
          <div class="big-title">Thanh toán</div>
          <div class="big-title" style="color: var(--topic-color2-400)">
            {{ formatStatus(selectedBooking.paymentStatus) }}
          </div>
        </div>
        <div class="buttons-container">
          <button
            class="reject-btn roboto-bold font-size-24"
            style="width: 200px; height: 36px; margin-right: 24px"
            @click="cancelBooking(selectedBooking)"
          >
            Từ chối
          </button>
          <button
            class="accept-btn roboto-bold font-size-24"
            style="width: 200px; height: 36px"
            @click="cancelBooking(selectedBooking)"
          >
            Xác nhận
          </button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import {
  getBookingStatus,
  getCourtClusterByCourtOwner,
  getCourtsOfCourtCluster,
  getCourtTimeSlotsByBookingId,
  getCustomerInfo,
} from "../../scripts/apiService.js";
export default {
  data() {
    return {
      isFormVisible: false,
      searchQuery: "",
      statusFilter: "",
      bookings: [],
      currentPage: 1,
      itemsPerPage: 15,
      courtClusters: [],
      courts: [],
      selectedBooking: null,
      seletedCourTimeSlotsByBookingId: [],
      selectedBookingCustomerInfo: {},
    };
  },
  async created() {
    try {
      // Load booking và courtCluster đồng thời
      const [bookingRes, courtClusterRes] = await Promise.all([
        getBookingStatus("All"),
        getCourtClusterByCourtOwner(),
      ]);

      // Xử lý kết quả của booking và courtCluster
      if (bookingRes.success) {
        this.bookings = bookingRes.data;
      }

      if (courtClusterRes.success) {
        this.courtClusters = courtClusterRes.data;
      }
      // Sau khi load xong courtCluster, tiếp tục lấy courts của từng cluster
      if (this.courtClusters.length > 0) {
        const courtPromises = this.courtClusters.map(
          (cluster) => getCourtsOfCourtCluster(cluster.id) // API nhận cluster.id
        );
        // Chạy tất cả request để lấy courts
        const courtsRes = await Promise.all(courtPromises);
        // Gộp tất cả các courts vào một mảng
        this.courts = courtsRes
          .filter((res) => res.success) // Lọc các response thành công
          .flatMap((res) => res.data); // Lấy dữ liệu từ các response
      }
    } catch (error) {
      console.log(`created CowrtOwwnerBookings: ${error}`);
    }
  },
  computed: {
    filteredBookings() {
      const filtered = this.bookings.filter((booking) => {
        const matchesSearch = booking.code
          .toLowerCase()
          .includes(this.searchQuery.toLowerCase());
        const matchesStatus =
          !this.statusFilter ||
          booking.status === this.statusFilter ||
          booking.paymentStatus === this.statusFilter;
        return matchesSearch && matchesStatus;
      });
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return filtered.slice(start, end);
    },
    totalPages() {
      const totalFiltered = this.bookings.filter((booking) => {
        const matchesSearch = booking.code
          .toLowerCase()
          .includes(this.searchQuery.toLowerCase());
        const matchesStatus =
          !this.statusFilter ||
          booking.status === this.statusFilter ||
          booking.paymentStatus === this.statusFilter;
        return matchesSearch && matchesStatus;
      }).length;
      return Math.ceil(totalFiltered / this.itemsPerPage);
    },
  },
  methods: {
    getCourtClusterName(courtClusterId) {
      const cluster = this.courtClusters.find(
        (cluster) => cluster.id === courtClusterId
      );
      return cluster ? cluster.name : "Không xác định";
    },
    getCourtNumber(courtId) {
      const court = this.courts.find((court) => court.id === courtId);
      return court ? court.courtNumber : "Không xác định";
    },
    changePage(page) {
      this.currentPage = page;
    },
    hideForm() {
      this.isFormVisible = false;
    },
    formatDate(dateString) {
      const options = {
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
        hour: "2-digit",
        minute: "2-digit",
      };
      return new Date(dateString).toLocaleString("vi-VN", options);
    },
    formatCurrency(amount) {
      return new Intl.NumberFormat("vi-VN", {
        style: "currency",
        currency: "VND",
      }).format(amount);
    },
    formatStatus(status) {
      let statusVie = undefined;
      switch (status) {
        case "Paid":
          statusVie = "Đã thanh toán";
          break;
        case "Unpaid":
          statusVie = "Chưa thanh toán";
          break;
        case "Pending":
          statusVie = "Đang chờ";
          break;
        case "CourtOwnerConfirmed":
          statusVie = "Đã xác nhận";
          break;
        default:
          break;
      }
      return statusVie;
    },
    async viewDetails(booking) {
      this.selectedBooking = booking; // Lưu thông tin booking được chọn
      const [courtTimeSlotsRes, customerInfoRes] = await Promise.all([
        getCourtTimeSlotsByBookingId(booking.id),
        getCustomerInfo(booking.customerId),
      ]);
      if (courtTimeSlotsRes.success) {
        this.seletedCourTimeSlotsByBookingId = courtTimeSlotsRes.data;
      }
      if (customerInfoRes.success) {
        this.selectedBookingCustomerInfo = customerInfoRes.data;
      }
      this.isFormVisible = true; // Hiển thị form chi tiết
    },
    acceptBooking(booking) {
      alert(`Đồng ý booking: ${booking.id}`);
      booking.status = "Đồng ý";
    },
    rejectBooking(booking) {
      alert(`Từ chối booking: ${booking.id}`);
      booking.status = "Từ chối";
    },
  },
};
</script>

<style scoped>
.container {
  display: flex;
  justify-content: space-between;
  height: calc(100vh - 96px - 12px);
  padding: 12px;
}

.content {
  /* background-color: bisque; */
  width: 100%;
  height: calc(100vh - 96px - 12px - 12px);
  margin-left: 200px;
  background-color: white;
  border: 1px solid rgba(0, 0, 0, 0.3);
  box-sizing: border-box;
  display: grid;
  border-radius: 4px;
  gap: 12px;
  width: 100%;
  padding: 12px;
  overflow: auto;
}

.navigation {
  position: fixed;
  top: 96px;
  left: 0;
  bottom: 0px;
  width: 200px;
  /* background-color: white; */
  display: flex;
  flex-direction: column;
  row-gap: 12px;
  padding: 12px;
}

.nav-option {
  font-size: 18px;
  font-family: roboto-medium;
  height: 32px;
  background-color: rgba(255, 255, 255, 1);
  padding: 6px;
  border-radius: 4px;
  display: flex;
  justify-content: start;
  align-items: center;
  border: 1px solid rgba(0, 0, 0, 0.2);
  box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
  text-decoration: unset;
  color: black;
  opacity: 0.8;
}

.router-link-active {
  opacity: 1;
  background-color: var(--topic-color-600);
  box-shadow: 2px 2px 4px var(--topic-color-500);
  color: white;
}

.big-title {
  font-size: 32px;
  font-family: roboto-bold;
}

.this-scoped-btn {
  width: 212px;
  background-color: green;
}

/* CSS TABLE */
.booking-management {
  padding: 16px;
  overflow-y: scroll;
  border-radius: 4px;
  position: relative;
}

.booking-table {
  width: 100%;
  border-collapse: collapse;
}

.booking-table th {
  position: sticky;
  top: 0;
}

.booking-table th,
.booking-table td {
  border: 1px solid rgba(0, 0, 0, 0.3);
  text-align: center;
  padding: 8px;
}

.booking-table th {
  background-color: #f4f4f4;
  font-weight: bold;
}

.detail-btn {
  background-color: #3498db;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 4px 8px;
  cursor: pointer;
}

.accept-btn {
  background-color: #2ecc71;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 4px 8px;
  cursor: pointer;
  margin-left: 4px;
}

.reject-btn {
  background-color: #e74c3c;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 4px 8px;
  cursor: pointer;
  margin-left: 4px;
}

.pagination {
  position: absolute;
  bottom: 0;
  right: 16px;
}

/* SEARCH */
.filters {
  display: flex;
  gap: 24px;
  margin-bottom: 16px;
}

.search-input {
  padding: 8px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  border-radius: 4px;
  width: 200px;
}

.status-select {
  padding: 8px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  border-radius: 4px;
  width: 150px;
}

/* CSS form chi tiet */
/* CSS form */
.detail-form {
  padding-top: 112px;
  position: fixed;
  z-index: 100;
  top: 50%;
  left: 50%;
  background-color: white;
  transform: translate(-50%, -50%);
  width: 1200px;
  height: 70%;
  display: flex;
  justify-content: space-around;
  border-radius: 4px;
  overflow: hidden;
  box-shadow: 2px 2px 12px rgba(0, 0, 0, 0.5);
}

.left-form {
  display: flex;
  flex-direction: column;
  row-gap: 36px;
}

.left-form .detail {
  display: flex;
  flex-direction: column;
  row-gap: 12px;
}
.left-form .detail div,
ul,
li {
  font-size: 16px;
}
.right-form {
  display: flex;
  flex-direction: column;
  row-gap: 36px;
}
</style>