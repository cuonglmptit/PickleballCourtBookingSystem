<template>
  <div class="container">
    <div class="content">
      <div class="left-container">
        <div class="left-top">
          <div class="roboto-bold font-size-24">Chỉnh sửa cụm sân</div>
          <button class="regular-btn bgc-topic3-500">Ngưng hoạt động</button>
        </div>
        <div class="left-mid">
          <div>
            <div class="lm-title">
              <div>Sửa giá tiền</div>
              <div>Số lượng sân: {{ courts.length }}</div>
            </div>
          </div>
          <div class="left-content">
            <div
              v-for="courtPrice in listCourtPrices"
              :key="courtPrice.id"
              class="time-slot"
            >
              {{ courtPrice.time + " - Giá tiền: " + courtPrice.price }}
              <button
                class="delete-slot-btn p-icon-x"
                @click="removeCourtPrice(index)"
              ></button>
            </div>
          </div>
          <div class="left-mid-bot">
            <button class="regular-btn add-time-slot-btn">Thêm giờ</button>
          </div>
        </div>
      </div>
      <div class="right-container">
        <div class="r-title">
          <div class="input-container">
            <label for="" class="roboto-bold font-size-24">Tên sân</label>
            <input v-model="cluster.name" type="text" class="title-input" />
          </div>
        </div>
        <div class="court-photos">
          <input type="file" />
        </div>
        <div class="address-container">
          <div class="roboto-bold font-size-24">Địa chỉ</div>
          <div class="input-container-inline">
            <label for="">Khu vực:</label>
            <input v-model="address.city" type="text" class="address-input" />
          </div>
          <div class="input-container-inline">
            <label for="">Quận/huyện:</label>
            <input
              v-model="address.district"
              type="text"
              class="address-input"
            />
          </div>
          <div class="input-container-inline">
            <label for="">Phường:</label>
            <input v-model="address.ward" type="text" class="address-input" />
          </div>
          <div class="input-container-inline">
            <label for="">Đường:</label>
            <input v-model="address.street" type="text" class="address-input" />
          </div>
        </div>
        <div class="description input-container">
          <label for="" class="roboto-bold font-size-24">Mô tả:</label>
          <textarea
            v-model="cluster.description"
            name=""
            id=""
            cols="30"
            rows="10"
          ></textarea>
        </div>
        <div class="action-container">
          <button class="regular-btn cancel-btn">Hủy thay đổi</button>
          <button class="regular-btn save-btn">Lưu</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {
  getCourtPricesByCourtClusterId,
  getCourtClusterById,
  getCourtsOfCourtCluster,
  getAddressByid,
} from "../../scripts/apiService.js";
export default {
  data() {
    return {
      cluster: {
        id: "",
        name: "",
        description: "",
        openingTime: "",
        closingTime: "",
        addressId: "",
        courtOwnerId: "",
        status: 1,
      },
      listCourtPrices: [],
      courts: {},
      address: {
        id: "",
        city: "",
        district: "",
        ward: "",
        street: "",
      },
    };
  },
  async created() {
    this.loadData();
  },
  methods: {
    removeCourtPrice(index) {
      this.listCourtPrices.splice(index, 1);
    },
    async loadData() {
      try {
        // Lấy id từ route
        const courtClusterId = this.$route.params.id;
        const [clusterRes, courtPricesRes, courtsRes] = await Promise.all([
          getCourtClusterById(courtClusterId),
          getCourtPricesByCourtClusterId(courtClusterId),
          getCourtsOfCourtCluster(courtClusterId),
        ]);
        if (clusterRes.success) {
          this.cluster = clusterRes.data;
        }
        let addressRes = await getAddressByid(this.cluster.addressId);
        this.address = addressRes.data;
        if (courtPricesRes.success) {
          this.listCourtPrices = courtPricesRes.data;
        }
        if (courtsRes.success) {
          this.courts = courtsRes.data;
        }
        console.log(this.cluster);
      } catch (error) {
        console.log(`loadData OwnerCourtClusterDetail: ${error}`);
      }
    },
  },
};
</script>

<style scoped>
.container {
  height: calc(100vh - 96px - 12px);
  padding: 12px 112px;
}

.content {
  /* background-color: bisque; */
  width: 100%;
  height: calc(100vh - 96px - 12px - 12px);
  background-color: white;
  border: 1px solid rgba(0, 0, 0, 0.3);
  box-shadow: 1px 1px 6px rgba(0, 0, 0, 0.5);
  box-sizing: border-box;
  display: flex;
  border-radius: 4px;
  width: 100%;
  padding: 12px;
  overflow: auto;
  justify-content: space-between;
}
/* CSS bên trái */
.left-container {
  display: flex;
  flex-direction: column;
  width: 60%;
  row-gap: 16px;
  padding: 16px;
  overflow: hidden;
}

.left-container .left-top {
  display: flex;
  flex-wrap: nowrap;
  justify-content: space-between;
}

.left-container .left-mid {
  border-radius: 4px;
  overflow: hidden;
  background-color: ghostwhite;
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  box-shadow: 0 0 4px rgba(0, 0, 0, 0.5);
}

.left-content {
  padding: 16px;
  column-count: 2; /* 2 cột */
  column-gap: 20px;
  row-gap: 12px;
  box-sizing: border-box;
  flex: 1;
}

.time-slot {
  color: white;
  margin-bottom: 12px;
  padding: 0 24px;
  height: 32px;
  font-size: 18px;
  border-radius: 2px;
  background-color: var(--topic-color4-400);
  box-shadow: 2px 2px 2px rgba(0, 0, 0, 0.7);
  font-family: roboto-bold;
  display: flex;
  justify-content: space-between;
  align-items: center;
  white-space: nowrap;
  overflow: hidden;
  text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
  position: relative;
}

.add-time-slot-btn {
  background-color: var(--topic-color-500);
  box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
}

.time-slot .delete-slot-btn {
  width: 22px;
  height: 22px;
  background-size: 22px;
  background-repeat: no-repeat;
  border: none;
  background-position: center;
  flex-shrink: 0;
  position: absolute;
  right: 12px;
  cursor: pointer;
  box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
  border-radius: 100px;
  border: 1px solid white;
}

.left-mid .lm-title {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: var(--topic-color-700);
  padding: 6px 12px;
}

.left-mid .lm-title div {
  font-size: 24px;
  font-family: roboto-bold;
  color: white;
}

.left-mid-bot {
  display: flex;
  justify-content: end;
  padding: 16px;
}

/* css bên phải */
.right-container {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 40%;
  height: 100%;
  padding: 16px;
}

.input-container {
  display: flex;
  flex-direction: column;
  row-gap: 4px;
}

.input-container-inline {
  display: flex;
  justify-content: space-between;
  column-gap: 4px;
  align-items: center;
}

.input-container-inline input {
  flex: 1;
}

.input-container-inline label {
  white-space: nowrap;
}

.title-input {
  height: 36px;
  width: 100%;
  border-radius: 4px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  padding: 0px 12px;
  font-size: 22px;
}

.court-photos {
  height: 200px;
  width: 100%;
  background-color: whitesmoke;
}

.address-container {
  display: flex;
  flex-direction: column;
  row-gap: 4px;
}

.address-input {
  height: 24px;
  border-radius: 4px;
  border: 1px solid rgba(0, 0, 0, 0.3);
  padding: 0px 12px;
  width: 100%;
}

.description {
  height: 112px;
}

.description textarea {
  border-radius: 4px;
}

.action-container {
  display: flex;
  column-gap: 9px;
  justify-content: end;
  align-items: center;
}

.save-btn {
  width: 72px;
  background-color: var(--topic-color4-500);
}

.cancel-btn {
  color: black;
  border: 1px solid rgba(0, 0, 0, 0.2);
}
</style>