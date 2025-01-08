<template>
  <div class="container">
    <div class="content">
      <div class="cluster-management">
        <div class="filters">
          <input
            v-model="searchQuery"
            type="text"
            placeholder="Tìm kiếm cụm sân..."
            class="search-input"
          />
          <div>
            Trạng thái:
            <select v-model="statusFilter" class="status-select">
              <option value="">Tất cả</option>
              <option value="Active">Hoạt động</option>
              <option value="Inactive">Ngừng hoạt động</option>
            </select>
          </div>
        </div>

        <table class="cluster-table">
          <thead>
            <tr>
              <th>STT</th>
              <th>Tên cụm sân</th>
              <th>Số lượng sân</th>
              <th>Tài khoản chủ sân</th>
              <th>Tên chủ sân</th>
              <th>SĐT chủ sân</th>
              <th>Email chủ sân</th>
              <th>Trạng thái</th>
              <th>Thao tác</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(cluster, index) in filteredClusters" :key="index">
              <td>{{ (currentPage - 1) * itemsPerPage + index + 1 }}</td>
              <td>{{ cluster.courtCluster.name }}</td>
              <td>{{ cluster.courts.length }}</td>
              <td>{{ cluster.owner.username }}</td>
              <td>{{ cluster.owner.name }}</td>
              <td>{{ cluster.owner.phoneNumber }}</td>
              <td>{{ cluster.owner.email }}</td>
              <td
                class="roboto-bold"
                :class="statusClass(cluster.courtCluster.status)"
              >
                {{ formatStatus(cluster.courtCluster.status) }}
              </td>
              <td class="center-colgap-4px">
                <button class="detail-btn" @click="viewDetails(cluster)">
                  Chi tiết
                </button>
                <button
                  v-if="cluster.courtCluster.status === 'Active'"
                  @click="deactiveCluster(cluster.courtCluster)"
                  class="reject-btn"
                >
                  Ngừng sân
                </button>
                <button
                  v-if="cluster.courtCluster.status === 'Inactive'"
                  class="accept-btn"
                  @click="activeCluster(cluster.courtCluster)"
                >
                  Mở sân
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
    <form class="detail-form" @click.stop @submit.prevent>
      <div class="left-form">
        <div class="big-title">Chi tiết cụm sân</div>
        <div class="detail">
          <div>Id cụm sân: {{ selectedCluster.courtCluster.id }}</div>
          <div>Tên cụm sân: {{ selectedCluster.courtCluster.name }}</div>
          <div>Số lượng sân: {{ selectedCluster.courts.length }}</div>
          <div v-if="selectedCluster && selectedCluster.address">
            Địa chỉ:
            {{
              `${selectedCluster.address.city}, ${selectedCluster.address.district}, ${selectedCluster.address.ward}, ${selectedCluster.address.street}`
            }}
          </div>
          <br />
          <div class="big-title">Thông tin chủ sân:</div>
          <div>Tên: {{ selectedCluster.owner.name }}</div>
          <div>Sđt: {{ selectedCluster.owner.phoneNumber }}</div>
          <div>Email: {{ selectedCluster.owner.email }}</div>
        </div>
      </div>
      <div class="right-form">
        <div>
          <div class="big-title">Trạng thái</div>
          <div
            class="big-title"
            :class="statusClass(selectedCluster.courtCluster.status)"
          >
            {{ formatStatus(selectedCluster.courtCluster.status) }}
          </div>
        </div>
        <div class="buttons-container">
          <button
            v-if="selectedCluster.courtCluster.status === 'Active'"
            class="reject-btn roboto-bold font-size-24"
            style="width: 200px; height: 36px"
            @click="deactiveCluster(selectedCluster.courtCluster)"
          >
            Ngừng sân
          </button>
          <button
            v-if="selectedCluster.courtCluster.status === 'Inactive'"
            class="accept-btn roboto-bold font-size-24"
            style="width: 200px; height: 36px"
            @click="activeCluster(selectedCluster.courtCluster)"
          >
            Mở sân
          </button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import {
  updateCourtClusterStatus,
  getClusterAdmin,
  getAddressByid,
} from "../../scripts/apiService.js";
export default {
  data() {
    return {
      isFormVisible: false,
      searchQuery: "",
      statusFilter: "",
      clusters: [],
      currentPage: 1,
      itemsPerPage: 15,
      courtClusters: [],
      courts: [],
      selectedCluster: null,
    };
  },
  async created() {
    this.loadData();
  },
  computed: {
    filteredClusters() {
      const filtered = this.clusters.filter((cluster) => {
        if (cluster.courtCluster.name) {
          const matchesSearch =
            this.removeVietnameseTones(cluster.courtCluster.name)
              .toLowerCase()
              .includes(this.searchQuery.toLowerCase()) ||
            cluster.owner.phoneNumber
              .toLowerCase()
              .includes(this.searchQuery.toLowerCase()) ||
            cluster.owner.email
              .toLowerCase()
              .includes(this.searchQuery.toLowerCase()) ||
            cluster.owner.username
              .toLowerCase()
              .includes(this.searchQuery.toLowerCase());
          const matchesStatus =
            !this.statusFilter ||
            cluster.courtCluster.status === this.statusFilter;
          return matchesSearch && matchesStatus;
        }
      });
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return filtered.slice(start, end);
    },
    totalPages() {
      const totalFiltered = this.filteredClusters.length;
      return Math.ceil(totalFiltered / this.itemsPerPage);
    },
  },

  methods: {
    async deactiveCluster(courtCluster) {
      const result = await Swal.fire({
        title: "Bạn có chắc chắn muốn ngưng hoạt động của cụm sân?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Hủy",
      });

      if (result.isConfirmed) {
        try {
          var response = await updateCourtClusterStatus(
            courtCluster.id,
            "Inactive"
          );
          if (response.success) {
            Swal.fire("Thành công!", response.userMsg, "success");
          } else {
            Swal.fire("Thất bại!", response.userMsg, "error");
          }
          await this.loadData();
          const updatedCluster = this.clusters.find(
            (item) => item.courtCluster.id === courtCluster.id
          );
          this.selectedCluster = updatedCluster;
          await this.viewDetails(updatedCluster);
        } catch (error) {
          console.log(`deactiveCluster Admin.vue: ${error}`);
        }
      }
    },
    async activeCluster(courtCluster) {
      const result = await Swal.fire({
        title: "Bạn có chắc chắn mở hoạt động cho cụm sân?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Hủy",
      });

      if (result.isConfirmed) {
        try {
          var response = await updateCourtClusterStatus(
            courtCluster.id,
            "Active"
          );
          if (response.success) {
            Swal.fire("Thành công!", response.userMsg, "success");
          } else {
            Swal.fire("Thất bại!", response.userMsg, "error");
          }
          await this.loadData();
          const updatedCluster = this.clusters.find(
            (item) => item.courtCluster.id === courtCluster.id
          );
          this.selectedCluster = updatedCluster;
          await this.viewDetails(updatedCluster);
        } catch (error) {
          console.log(`deactiveCluster Admin.vue: ${error}`);
        }
      }
    },
    removeVietnameseTones(str) {
      try {
        return str
          .normalize("NFD") // Tách các ký tự đặc biệt khỏi chữ cái
          .replace(/[\u0300-\u036f]/g, "") // Loại bỏ dấu
          .replace(/đ/g, "d") // Thay chữ "đ" thành "d"
          .replace(/Đ/g, "D"); // Thay chữ "Đ" thành "D"
      } catch (error) {
        console.log(`removeVietnameseTones InputSelect: ${error}`);
      }
    },
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
        case "Active":
          statusVie = "Hoạt động";
          break;
        case "Inactive":
          statusVie = "Ngừng hoạt động";
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
        case "Active":
          statusClass = "confirmed";
          break;
        case "Inactive":
          statusClass = "canceled";
          break;
        default:
          break;
      }
      return statusClass;
    },
    async viewDetails(cluster) {
      this.selectedCluster = cluster;
      const resp = await getAddressByid(
        this.selectedCluster.courtCluster.addressId
      );
      if (resp.success) {
        this.selectedCluster.address = resp.data;
      }
      this.isFormVisible = true; // Hiển thị form chi tiết
    },
    async loadData() {
      try {
        const clusterRes = await getClusterAdmin(this.statusFilter);
        if (clusterRes.success) {
          this.clusters = clusterRes.data;
        }
      } catch (error) {
        console.error(`Error in loadData Admin.vue: ${error}`);
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
.cluster-management {
  padding: 16px;
  overflow-y: scroll;
  border-radius: 4px;
  position: relative;
}

.cluster-table {
  width: 100%;
  border-collapse: collapse;
}

.cluster-table th {
  position: sticky;
  top: 0;
}

.cluster-table th,
.cluster-table td {
  border: 1px solid rgba(0, 0, 0, 0.3);
  text-align: center;
  padding: 8px;
}

.cluster-table th {
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