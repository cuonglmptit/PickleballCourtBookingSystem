<template>
  <div class="container">
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
              <option value="Đang chờ">Đang chờ</option>
              <option value="Đồng ý">Đã xác nhận</option>
              <option value="Đã thanh toán">Đã thanh toán</option>
              <option value="Chưa thanh toán">Chưa thanh toán</option>
            </select>
          </div>
        </div>
        <table class="booking-table">
          <thead>
            <tr>
              <th>STT</th>
              <th>Booking ID</th>
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
              <td>{{ formatDate(booking.bookingTime) }}</td>
              <td>{{ booking.status }}</td>
              <td>{{ booking.paymentStatus }}</td>
              <td>{{ booking.courtCluster }}</td>
              <td>{{ booking.court }}</td>
              <td>{{ formatCurrency(booking.totalAmount) }}</td>
              <td>
                <button class="detail-btn" @click="viewDetails(booking)">
                  Chi tiết
                </button>
                <button class="cancel-btn" @click="cancelBooking(booking)">
                  Hủy
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
      <div class="overlay">
        <form class="detail-form" action="">
          <div class="left-form">
            <div class="big-title">Chi tiết lịch đặt</div>
            <div class="detail">
              <div>Thời gian đặt: 12:00 21/12/2024</div>
              <div>Cụm sân hà đông</div>
              <div>Địa chỉ: Đường Nguyễn Trãi, Hà Đông, Hà Nội</div>
              <ul>
                <li>Sân 1: 14:00</li>
                <li>Sân 1: 15:00</li>
              </ul>
            </div>
          </div>
          <div class="right-form">
            <div>
              <div class="big-title">Trạng thái</div>
              <div class="big-title" style="color: orange">
                Đang chờ chủ sân xác nhận
              </div>
            </div>
            <div>
              <div class="big-title">Thanh toán</div>
              <div class="big-title" style="color: var(--topic-color2-400)">
                Chưa thanh toán
              </div>
            </div>
            <button
              class="cancel-btn roboto-bold font-size-24"
              style="width: 200px; height: 36px"
              @click="cancelBooking(booking)"
            >
              Hủy
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      searchQuery: "",
      bookings: [
        {
          id: "1",
          code: "12314",
          bookingTime: "2024-12-27T08:00:00",
          status: "Đang chờ",
          paymentStatus: "Chưa thanh toán",
          courtCluster: "Golden Pickle Courts",
          court: "2",
          totalAmount: 300000,
        },
        {
          id: "2",
          code: "41423",
          bookingTime: "2024-12-27T10:00:00",
          paymentStatus: "Đã thanh toán",
          status: "Đồng ý",
          courtCluster: "Silver Court Arena",
          court: "1",
          totalAmount: 500000,
        },
      ],
      currentPage: 1,
      itemsPerPage: 15,
    };
  },
  computed: {
    filteredBookings() {
      return this.bookings
        .filter((booking) =>
          booking.code.toLowerCase().includes(this.searchQuery.toLowerCase())
        )
        .slice(
          (this.currentPage - 1) * this.itemsPerPage,
          this.currentPage * this.itemsPerPage
        );
    },
    totalPages() {
      return Math.ceil(this.bookings.length / this.itemsPerPage);
    },
  },
  methods: {
    changePage(page) {
      this.currentPage = page;
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
    viewDetails(booking) {
      alert(`Xem chi tiết booking: ${booking.id}`);
    },
    cancelBooking(booking) {
      alert(`Hủy booking: ${booking.id}`);
      // Thêm logic để hủy booking
    },
  },
};
</script>

<style scoped>
.container {
  height: calc(100vh - 96px - 12px);
  padding: 12px;
}

.content {
  /* background-color: bisque; */
  width: 100%;
  height: calc(100vh - 96px - 12px - 12px);
  background-color: white;
  border: 1px solid rgba(0, 0, 0, 0.3);
  box-shadow: 1px 1px 6px rgba(0, 0, 0, 0.5);
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

.cancel-btn {
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
.left-form .detail div {
  font-size: 16px;
}
.right-form {
  display: flex;
  flex-direction: column;
  row-gap: 36px;
}
</style>
