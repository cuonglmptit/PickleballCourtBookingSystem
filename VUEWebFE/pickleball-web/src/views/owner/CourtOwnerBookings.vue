<template>
  <div class="container">
    <div class="navigation">
      <router-link class="nav-option" :to="{ name: 'owner-manage-booking' }"
        >Quản lý booking
      </router-link>
      <router-link class="nav-option" :to="{ name: 'manage-court-cluster' }"
        >Quản lý sân
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
            Trạng thái:
            <select v-model="statusFilter" class="status-select">
              <option value="">Tất cả</option>
              <option value="Pending">Đang chờ</option>
              <option value="CourtOwnerConfirmed">Đã xác nhận</option>
              <option value="ConfirmedButUnpaid">
                Đã xác nhận nhưng chưa thanh toán
              </option>
              <option value="Paid">Đã thanh toán</option>
              <option value="Unpaid">Chưa thanh toán</option>
              <option value="Completed">Đã hoàn thành</option>
              <option value="Canceled">Đã hủy</option>
            </select>
          </div>
          <div>
            Cụm Sân:
            <select v-model="clusterFilter" class="status-select">
              <option value="">Tất cả</option>
              <option
                v-for="(cluster, index) in courtClusters"
                :value="cluster.id"
                :key="index"
              >
                {{ cluster.name }}
              </option>
            </select>
          </div>
        </div>
        <table class="booking-table">
          <thead>
            <tr>
              <th>STT</th>
              <th>Mã booking</th>
              <th>Thời gian đặt</th>
              <th>Cụm sân</th>
              <th>Sân số</th>
              <th>Trạng thái</th>
              <th>Thanh toán</th>
              <th>Tổng tiền</th>
              <th>Thao tác</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(booking, index) in filteredBookings" :key="booking.id">
              <td>{{ (currentPage - 1) * itemsPerPage + index + 1 }}</td>
              <td class="roboto-bold">{{ booking.code }}</td>
              <td>{{ formatDate(booking.timeBooking) }}</td>
              <td>{{ getCourtClusterName(booking.courtClusterId) }}</td>
              <td>{{ getCourtNumber(booking.courtId) }}</td>
              <td class="roboto-bold" :class="statusClass(booking.status)">
                {{ formatStatus(booking.status) }}
              </td>
              <td
                class="roboto-bold"
                :class="statusClass(booking.paymentStatus)"
              >
                {{ formatStatus(booking.paymentStatus) }}
              </td>
              <td
                class="roboto-bold"
                :class="statusClass(booking.paymentStatus)"
              >
                {{ formatCurrency(booking.amount) }}
              </td>
              <td class="center-colgap-4px">
                <button class="detail-btn" @click="viewDetails(booking)">
                  Chi tiết
                </button>
                <div
                  class="center-colgap-4px"
                  v-if="booking.status === 'Pending'"
                >
                  <button class="accept-btn" @click="acceptBooking(booking)">
                    Xác nhận
                  </button>
                  <button class="reject-btn" @click="rejectBooking(booking)">
                    Từ chối
                  </button>
                </div>
                <button
                  v-if="
                    booking.status === 'CourtOwnerConfirmed' &&
                    booking.paymentStatus === 'Unpaid'
                  "
                  class="confirm-paid-mini-btn"
                  @click="confirmPaidBooking(booking)"
                >
                  Xác nhận thanh toán
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
          <span style="padding: 0 6px"
            >Trang {{ currentPage }} / {{ totalPages }}</span
          >
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
    <form class="detail-form" @click.stop @submit.prevent>
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
          <div class="big-title" :class="statusClass(selectedBooking.status)">
            {{ formatStatus(selectedBooking.status) }}
          </div>
        </div>
        <div>
          <div class="big-title">Thanh toán</div>
          <div
            class="big-title"
            :class="statusClass(selectedBooking.paymentStatus)"
          >
            {{ formatStatus(selectedBooking.paymentStatus) }}
          </div>
        </div>
        <div class="buttons-container">
          <div
            class="center-colgap-4px"
            v-if="selectedBooking.status === 'Pending'"
          >
            <button
              class="reject-btn roboto-bold font-size-24"
              style="width: 200px; height: 36px"
              @click="rejectBooking(selectedBooking)"
            >
              Từ chối
            </button>
            <button
              class="accept-btn roboto-bold font-size-24"
              style="width: 200px; height: 36px"
              @click="acceptBooking(selectedBooking)"
            >
              Xác nhận
            </button>
          </div>
          <button
            v-if="
              selectedBooking.status === 'CourtOwnerConfirmed' &&
              selectedBooking.paymentStatus === 'Unpaid'
            "
            class="confirm-paid-btn regular-btn roboto-bold font-size-24"
            style="width: 100%; height: 36px"
            @click="confirmPaidBooking(selectedBooking)"
          >
            Xác nhận đã thanh toán
          </button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import {
  getBookingStatus,
  getCourtClusterByCourtOwner,
  getCourtsOfCourtCluster,
  getCourtTimeSlotsByBookingId,
  getCustomerInfo,
  courtOwnerConfirmBooking,
  courtOwnerConfirmPaid,
  cancelBooking,
} from "../../scripts/apiService.js";
export default {
  data() {
    return {
      isFormVisible: false,
      searchQuery: "",
      statusFilter: "",
      clusterFilter: "",
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
    this.loadData();
  },
  computed: {
    filteredBookings() {
      const filtered = this.bookings.filter((booking) => {
        if (booking.code) {
          const matchesSearch = booking.code
            .toLowerCase()
            .includes(this.searchQuery.toLowerCase());
          const matchesStatus =
            !this.statusFilter ||
            booking.status === this.statusFilter ||
            booking.paymentStatus === this.statusFilter ||
            (this.statusFilter === "ConfirmedButUnpaid" &&
              booking.status === "CourtOwnerConfirmed" &&
              booking.paymentStatus === "Unpaid");
          const matchesCluster =
            !this.clusterFilter ||
            booking.courtClusterId === this.clusterFilter;
          return matchesSearch && matchesStatus && matchesCluster;
        }
      });
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return filtered.slice(start, end);
    },
    totalPages() {
      const totalFiltered = this.bookings.filter((booking) => {
        if (booking.code) {
          const matchesSearch = booking.code
            .toLowerCase()
            .includes(this.searchQuery.toLowerCase());
          const matchesStatus =
            !this.statusFilter ||
            booking.status === this.statusFilter ||
            booking.paymentStatus === this.statusFilter;
          return matchesSearch && matchesStatus;
        }
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
        case "Canceled":
          statusVie = "Đã hủy";
          break;
        case "Completed":
          statusVie = "Đã hoàn thành";
          break;
        default:
          break;
      }
      return statusVie;
    },
    statusClass(status) {
      let statusClass = undefined;
      switch (status) {
        case "Paid":
          statusClass = "paid";
          break;
        case "Unpaid":
          statusClass = "unpaid";
          break;
        case "Pending":
          statusClass = "pending";
          break;
        case "CourtOwnerConfirmed":
          statusClass = "confirmed";
          break;
        case "Canceled":
          statusClass = "canceled";
          break;
        case "Completed":
          statusClass = "paid";
          break;
        default:
          break;
      }
      return statusClass;
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
    async acceptBooking(booking) {
      const result = await Swal.fire({
        title: "Bạn có chắc chắn muốn xác nhận lịch đặt này?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Hủy",
      });

      if (result.isConfirmed) {
        try {
          var response = await courtOwnerConfirmBooking(booking.id);
          if (response.success) {
            Swal.fire("Thành công!", response.userMsg, "success");
          } else {
            Swal.fire("Thất bại!", response.userMsg, "error");
          }
          await this.loadData();
          this.viewDetails(
            this.bookings.find((item) => item.id === booking.id)
          );
        } catch (error) {
          console.log(`acceptBooking CourtOwnerBookings.vue: ${error}`);
        }
      }
    },
    async rejectBooking(booking) {
      const result = await Swal.fire({
        title: "Bạn có chắc chắn muốn hủy lịch đặt này?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Hủy",
      });

      if (result.isConfirmed) {
        try {
          var response = await cancelBooking(booking.id);
          if (response.success) {
            Swal.fire("Thành công!", response.userMsg, "success");
          } else {
            Swal.fire("Thất bại!", response.userMsg, "error");
          }
          await this.loadData();
          this.viewDetails(
            this.bookings.find((item) => item.id === booking.id)
          );
        } catch (error) {
          console.log(`rejectBooking CourtOwnerBookings.vue: ${error}`);
        }
      }
    },
    async confirmPaidBooking(booking) {
      const result = await Swal.fire({
        title:
          "Bạn có chắc chắn muốn xác nhận khách đã thanh toán lịch đặt này?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Hủy",
      });

      if (result.isConfirmed) {
        try {
          var response = await courtOwnerConfirmPaid(booking.id);
          if (response.success) {
            Swal.fire("Thành công!", response.userMsg, "success");
          } else {
            Swal.fire("Thất bại!", response.userMsg, "error");
          }
          await this.loadData();
          this.viewDetails(
            this.bookings.find((item) => item.id === booking.id)
          );
        } catch (error) {
          console.log(`confirmPaidBooking CourtOwnerBookings.vue: ${error}`);
        }
      }
    },
    async loadData() {
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
        console.error(`Error in loadData CourtOwnerBookings.vue: ${error}`);
      }
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
}

.reject-btn {
  background-color: #e74c3c;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 4px 8px;
  cursor: pointer;
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
  width: 200px;
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

.pending {
  color: orange;
}

.confirmed {
  color: var(--topic-color4-500);
}

.paid {
  color: var(--topic-color-500); /* Màu xanh dương */
}

.unpaid {
  color: var(--topic-color2-500);
}

.canceled {
  color: var(--topic-color3-500); /* Màu đỏ */
}

.buttons-container {
  display: flex;
  flex-direction: column;
  row-gap: 12px;
  box-sizing: border-box;
}

.confirm-paid-btn {
  box-sizing: border-box;
  background-color: var(--topic-color2-600);
  font-size: 24px;
}

.confirm-paid-mini-btn {
  background-color: var(--topic-color2-600);
  color: white;
  border: none;
  border-radius: 4px;
  padding: 4px 8px;
  cursor: pointer;
}

.center-colgap-4px {
  display: flex;
  justify-content: center;
  column-gap: 4px;
}
</style>