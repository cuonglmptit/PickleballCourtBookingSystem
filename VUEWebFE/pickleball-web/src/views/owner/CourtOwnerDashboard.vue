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
      </div>

      <div class="total">
        Tổng số tiền: {{ formatCurrency(totalAmount) }}
      </div>
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
      clusters: ["Golden Pickle Courts", "Silver Court Arena", "Diamond Fields"],
      chart: null,
      rawData: [
        { cluster: "Golden Pickle Courts", date: "2024-12-20", amount: 500000 },
        { cluster: "Silver Court Arena", date: "2024-12-21", amount: 300000 },
        { cluster: "Diamond Fields", date: "2024-12-22", amount: 200000 },
        { cluster: "Golden Pickle Courts", date: "2024-12-23", amount: 700000 },
      ],
      filteredData: [],
      totalAmount: 0,
    };
  },
  mounted() {
    this.filteredData = this.rawData;
    this.updateChart();
  },
  methods: {
    filterData() {
      this.filteredData = this.rawData.filter((item) => {
        const isWithinDateRange =
          (!this.startDate || item.date >= this.startDate) &&
          (!this.endDate || item.date <= this.endDate);
        const isMatchingCluster =
          this.selectedCluster === "all" ||
          item.cluster === this.selectedCluster;

        return isWithinDateRange && isMatchingCluster;
      });

      this.totalAmount = this.filteredData.reduce(
        (sum, item) => sum + item.amount,
        0
      );

      this.updateChart();
    },
    updateChart() {
      const labels = [...new Set(this.filteredData.map((item) => item.date))];
      const datasets = this.clusters.map((cluster) => {
        const data = labels.map((label) => {
          const record = this.filteredData.find(
            (item) => item.date === label && item.cluster === cluster
          );
          return record ? record.amount : 0;
        });
        return {
          label: cluster,
          data,
          backgroundColor: this.getRandomColor(),
        };
      });

      if (this.chart) {
        this.chart.destroy();
      }

      const ctx = document.getElementById("moneyChart").getContext("2d");
      this.chart = new Chart(ctx, {
        type: "bar",
        data: {
          labels,
          datasets,
        },
        options: {
          responsive: true,
          plugins: {
            legend: {
              position: "top",
            },
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
