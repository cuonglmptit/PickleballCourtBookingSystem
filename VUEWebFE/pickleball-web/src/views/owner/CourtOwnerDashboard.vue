<template>
  <div class="container">
    <div class="content">
      <div class="filter">
        <label>
          Chọn cụm sân:
          <select v-model="selectedCluster">
            <option value="all">Tất cả</option>
            <option v-for="cluster in clusters" :key="cluster" :value="cluster">
              {{ cluster }}
            </option>
          </select>
        </label>

        <label>
          Chọn khoảng thời gian:
          <input type="date" v-model="startDate" />
          -
          <input type="date" v-model="endDate" />
        </label>

        <button @click="filterData">Lọc</button>
      </div>

      <div class="chart-container">
        <canvas id="moneyChart"></canvas>
        <canvas id="totalRevenueChart"></canvas>
      </div>

      <div class="total">Tổng số tiền: {{ formatCurrency(totalAmount) }}</div>
    </div>
  </div>
</template>

<script>
import { Chart, registerables } from "chart.js";

Chart.register(...registerables);

export default {
  data() {
    return {
      selectedCluster: "all",
      startDate: "",
      endDate: "",
      data: [
        {
          courtCluster: {
            id: "4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c",
            name: "Golden Pickle Courts",
            description:
              "Cụm sân hiện đại với ánh sáng vàng sang trọng, thiết kế phù hợp cho các giải đấu chuyên nghiệp.",
            openingTime: "07:00:00",
            closingTime: "23:00:00",
            addressId: "278f0020-0ceb-461e-8ddd-57380d02170b",
            courtOwnerId: "816762f5-736d-40e4-9a1d-4d7f2e9ed920",
            status: 1,
          },
          bookings: [
            {
              id: "be534c35-2815-4a3b-9839-2d43bf091b35",
              timeBooking: "2025-01-03T18:30:24",
              code: "BK0010",
              amount: 200000,
              status: "CourtOwnerConfirmed",
              paymentStatus: "Paid",
              courtId: "45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93",
              customerId: "8a5f8ae2-3d29-4cf4-8bb4-78efdccbf7f3",
              courtClusterId: "4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c",
              courtOwnerId: "816762f5-736d-40e4-9a1d-4d7f2e9ed920",
              customerPhoneNumber: null,
            },
            {
              id: "d186d849-d5c8-4c89-8556-e921c9e0f103",
              timeBooking: "2025-01-04T19:09:06",
              code: "BK0012",
              amount: 150000,
              status: "CourtOwnerConfirmed",
              paymentStatus: "Paid",
              courtId: "2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca",
              customerId: "a015c0c0-4c2d-4a23-a3e3-c0b60e2369ca",
              courtClusterId: "4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c",
              courtOwnerId: "816762f5-736d-40e4-9a1d-4d7f2e9ed920",
              customerPhoneNumber: null,
            },
            {
              id: "f8855e10-cf40-4c45-b3b8-89752aeeac2b",
              timeBooking: "2025-01-02T23:04:06",
              code: "BK0008",
              amount: 350000,
              status: "CourtOwnerConfirmed",
              paymentStatus: "Paid",
              courtId: "f239ab68-1a67-4b35-81b7-619399c0df78",
              customerId: "a015c0c0-4c2d-4a23-a3e3-c0b60e2369ca",
              courtClusterId: "4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c",
              courtOwnerId: "816762f5-736d-40e4-9a1d-4d7f2e9ed920",
              customerPhoneNumber: null,
            },
          ],
          totalRevenue: 700000,
          totalBookings: 3,
        },
        {
          courtCluster: {
            id: "4cf6a6d5-52b1-4854-8365-c72015682e9c",
            name: "Sân Bình Minh",
            description:
              "Nơi lý tưởng để khởi đầu một ngày mới với không gian thoáng đãng và ánh nắng ban mai.",
            openingTime: "05:00:00",
            closingTime: "19:00:00",
            addressId: "d8fe6eb7-d60b-42c5-9cf7-789abd25fbda",
            courtOwnerId: "816762f5-736d-40e4-9a1d-4d7f2e9ed920",
            status: 1,
          },
          bookings: [
            {
              id: "16d9ed3c-342e-4094-974f-d3778fdd4d95",
              timeBooking: "2025-01-06T10:02:38",
              code: "BK0019",
              amount: 300000,
              status: "CourtOwnerConfirmed",
              paymentStatus: "Paid",
              courtId: "c8cb2f9d-17c7-4b9e-b28b-07e4e62a43c4",
              customerId: "88ebaf75-0ef4-418c-aeee-c31441a6f5ec",
              courtClusterId: "4cf6a6d5-52b1-4854-8365-c72015682e9c",
              courtOwnerId: "816762f5-736d-40e4-9a1d-4d7f2e9ed920",
              customerPhoneNumber: null,
            },
          ],
          totalRevenue: 300000,
          totalBookings: 1,
        },
      ],
    };
  },
  mounted() {
    this.createTotalRevenueChart();
    this.createRevenueByClusterChart();
  },
  methods: {
    createTotalRevenueChart() {
      const labels = this.data.map((item) => item.courtCluster.name);
      const data = this.data.map((item) => item.totalRevenue);
      const ctx = document.getElementById("totalRevenueChart").getContext("2d");
      new Chart(ctx, {
        type: "pie",
        data: {
          labels,
          datasets: [
            {
              data,
              backgroundColor: this.generateColors(data.length),
            },
          ],
        },
        options: {
          responsive: true,
        },
      });
    },
    createRevenueByClusterChart() {
      const labels = this.data[0].bookings.map(
        (booking) => booking.timeBooking.split("T")[0]
      );
      const datasets = this.data.map((cluster) => ({
        label: cluster.courtCluster.name,
        data: cluster.bookings.map((booking) => booking.amount),
        backgroundColor: this.getRandomColor(),
      }));
      const ctx = document
        .getElementById("revenueByClusterChart")
        .getContext("2d");
      new Chart(ctx, {
        type: "bar",
        data: {
          labels,
          datasets,
        },
        options: {
          responsive: true,
          scales: {
            x: { stacked: true },
            y: { stacked: true },
          },
        },
      });
    },
    formatCurrency(amount) {
      return new Intl.NumberFormat("vi-VN", {
        style: "currency",
        currency: "VND",
      }).format(amount);
    },
    generateColors(count) {
      return Array.from({ length: count }, () => this.getRandomColor());
    },
    getRandomColor() {
      const r = Math.floor(Math.random() * 255);
      const g = Math.floor(Math.random() * 255);
      const b = Math.floor(Math.random() * 255);
      return `rgba(${r}, ${g}, ${b}, 0.6)`;
    },
  },
};
</script>

<style scoped>
.container {
  display: flex;
  justify-content: space-between;
  height: calc(100vh - 96px);
  width: 100%;
  padding: 12px;
}
.content {
  width: 100%;
  height: 100%;
  box-sizing: border-box;
  background-color: white;
  border-radius: 4px;
  box-shadow: 0 0 4px rgba(0, 0, 0, 0.5);
  padding: 16px;
}

.filter {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.chart-container {
  height: 600px;
  display: flex;
  justify-content: center;
}

.total {
  margin-top: 16px;
  font-size: 18px;
  font-weight: bold;
}
</style>
