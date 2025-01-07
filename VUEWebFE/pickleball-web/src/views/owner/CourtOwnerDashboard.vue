<template>
  <div class="container">
    <div class="content">
      <div class="filter">
        <label> Chọn khoảng thời gian </label>
        <select name="" id="" v-model="selectedCluster">
          <option value="all">Tất cả cụm sân</option>
          <option
            v-for="(clusterData, index) in data"
            :key="index"
            :value="clusterData.courtCluster.id"
          >
            {{ clusterData.courtCluster.name }}
          </option>
        </select>
        <div>
          <input type="date" v-model="dateRange.startDate" />
          <span style="padding: 0 6px">đến</span>
          <input type="date" v-model="dateRange.endDate" />
        </div>
        <button @click="filterData">Lọc</button>
      </div>

      <div class="chart-container">
        <div class="spin-chart">
          <div class="total">
            Tổng số tiền: {{ formatCurrency(totalMoney) }}
          </div>
          <canvas id="totalRevenueChart"></canvas>
        </div>
        <div class="bar-chart">
          <canvas id="revenueByClusterChart"></canvas>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Chart, registerables } from "chart.js";
import { getStatisticInRange, getStatisticAll } from "@/scripts/apiService.js";
import Swal from "sweetalert2";

Chart.register(...registerables);

export default {
  components: {},
  data() {
    return {
      selectedCluster: "all",
      dateRange: {
        startDate: new Date().toISOString().split("T")[0],
        endDate: new Date().toISOString().split("T")[0],
      },
      data: null,
      filteredData: null,
      totalRevenueChartInstance: null,
      revenueByClusterChartInstance: null,
    };
  },
  computed: {
    totalMoney() {
      if (!this.data) return 0;

      if (this.selectedCluster === "all") {
        // Tính tổng từ tất cả các totalRevenue
        return this.data.reduce(
          (acc, current) => acc + current.totalRevenue,
          0
        );
      } else {
        // Tìm cụm sân phù hợp và lấy totalRevenue của nó
        const cluster = this.data.find(
          (current) => current.courtCluster.id === this.selectedCluster
        );
        return cluster ? cluster.totalRevenue : 0;
      }
    },
  },
  created() {},
  async mounted() {
    await this.loadData();
    this.createRevenueByClusterChart();
    this.createTotalRevenueChart();
  },
  methods: {
    async loadData() {
      try {
        const dataRes = await getStatisticInRange(
          this.dateRange.startDate,
          this.dateRange.endDate
        );
        if (dataRes.success) {
          this.data = dataRes.data;
          this.filteredData = this.data;
        }
      } catch (error) {
        console.log(`loadData Dashboard Owner: ${error}`);
      }
    },
    validDateRange(startDate, endDate) {
      return !!startDate && !!endDate && startDate <= endDate;
    },
    createTotalRevenueChart() {
      if (this.totalRevenueChartInstance) {
        this.totalRevenueChartInstance.destroy();
      }
      if (!this.filteredData) return;
      const labels = this.filteredData.map((item) => item.courtCluster.name);
      const data = this.filteredData.map((item) => item.totalRevenue);
      const ctx = document.getElementById("totalRevenueChart").getContext("2d");
      this.totalRevenueChartInstance = new Chart(ctx, {
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
      if (this.revenueByClusterChartInstance) {
        this.revenueByClusterChartInstance.destroy();
      }
      if (!this.filteredData) return;
      const labels = this.filteredData.flatMap((cluster) =>
        cluster.bookings.map((booking) => booking.timeBooking.split("T")[0])
      );
      const datasets = this.filteredData.map((cluster) => ({
        label: cluster.courtCluster.name,
        data: cluster.bookings.map((booking) => booking.amount || 0), // Thêm giá trị mặc định nếu không có 'amount'
        backgroundColor: this.getRandomColor(),
      }));
      const ctx = document
        .getElementById("revenueByClusterChart")
        .getContext("2d");
      this.revenueByClusterChartInstance = new Chart(ctx, {
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
    handleDateRange(range) {
      this.dateRange = range;
    },
    async filterData() {
      let validDate = this.validDateRange(
        this.dateRange.startDate,
        this.dateRange.endDate
      );
      console.log(validDate);

      await this.loadData();
      if (validDate === true) {
        if (this.selectedCluster !== "all") {
          // Tìm cụm sân phù hợp và lấy totalRevenue của nó
          const cluster = this.data.find(
            (current) => current.courtCluster.id === this.selectedCluster
          );
          this.filteredData = [cluster];
        }
        this.createRevenueByClusterChart();
        this.createTotalRevenueChart();
      } else {
        Swal.fire("Vui lòng chọn khoảng thời gian hợp lệ!", "", "error");
      }
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
  justify-content: start;
  align-items: flex-start;
  column-gap: 32px;
  margin-bottom: 16px;
  height: 60px;
}

.chart-container {
  display: flex;
  justify-content: center;
  column-gap: 64px;
  height: 100%;
  width: 100%;
  align-items: center;
}

.chart-container .spin-chart {
  width: 30%;
}
.chart-container .bar-chart {
  width: 70%;
}
.total {
  margin-top: 16px;
  font-size: 18px;
  font-weight: bold;
}
</style>
