<template>
  <div class="container">
    <div class="content">
      <div class="left-container">
        <div class="left-top">
          <div class="roboto-bold font-size-24">Chỉnh sửa giá</div>
          <!-- <button class="regular-btn bgc-topic3-500">Ngưng hoạt động</button> -->
        </div>
        <div class="left-mid">
          <div>
            <div class="lm-title">
              <div>Sửa giá tiền của cụm sân</div>
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
          <button
            class="regular-btn add-time-slot-btn"
            style="margin: 16px"
            @click="toggleAddPriceForm(true)"
          >
            Thêm giờ (lịch đặt)
          </button>
          <div class="left-mid-bot">
            <button
              @click="handleCreateCourtTimeSlots"
              class="regular-btn apply-time-slot-btn"
            >
              Áp dụng cho
            </button>
            <div>
              <SelectDateRange @date-range-selected="handleDateRange" />
            </div>
          </div>
        </div>
      </div>
      <div class="right-container">
        <div class="r-title">
          <div class="input-container">
            <div
              class="roboto-bold font-size-24"
              style="color: var(--topic-color-600)"
            >
              Chỉnh sửa thông tin
            </div>
            <label for="" class="roboto-bold font-size-24">Tên sân</label>
            <input v-model="cluster.name" type="text" class="title-input" />
          </div>
        </div>
        <div class="court-photos">
          <img :src="imageCourtUrl.url" alt="" />
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
          <button class="regular-btn cancel-btn" @click="cancelChanges">
            Hủy thay đổi
          </button>
          <button class="regular-btn save-btn" @click="handleSave">Lưu</button>
        </div>
      </div>
    </div>
    <div
      class="overlay"
      @click="toggleAddPriceForm(false)"
      v-if="isAddPriceVisible"
    >
      <div class="add-price-form" @click.stop>
        <div class="font-size-24 roboto-bold">Thêm giờ (lịch đặt)</div>
        <InputSelect
          style="width: 86px"
          :placeHoder="'Chọn giờ'"
          v-model="newPrice.time"
          :suggestions="
            getDifferenceBetweenArrays(
              listTime,
              listCourtPrices.map((price) => price.time)
            )
          "
        />
        <div class="input-container" style="width: 100%; padding: 0 20%">
          <label for="">Giá tiền (VNĐ)</label>
          <input
            class="add-price-input"
            type="text"
            id="moneyInput"
            placeholder="Nhập giá tiền"
            v-model="formattedPrice"
            @input="updatePrice"
          />
        </div>
        <div v-if="addPriceError" style="color: red">
          Vui lòng điền đủ các trường
        </div>
        <button class="regular-btn bgc-topic-500" @click="handleAddCourtPrice">
          Thêm +
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";

import {
  getCourtPricesByCourtClusterId,
  getCourtClusterById,
  getCourtsOfCourtCluster,
  getAddressByid,
  getListTime,
  createDefaultPrice,
  autoCreateCourtTimeSlot,
  getImageCourtUrl,
  putAddress,
  putCluster,
} from "../../scripts/apiService.js";

import InputSelect from "@/components/inputs/InputSelect.vue";
import SelectDateRange from "@/components/inputs/SelectDateRange.vue";
export default {
  components: {
    SelectDateRange,
    InputSelect,
  },
  computed: {
    formattedPrice: {
      get() {
        return this.formatCurrency(this.newPrice.price);
      },
      set(value) {
        this.newPrice.price = this.parseCurrency(value);
      },
    },
  },
  data() {
    return {
      imageCourtUrl: {},
      addPriceError: false,
      newPrice: {
        time: "",
        price: 50000,
        courtClusterId: "",
      },
      isAddPriceVisible: false,
      listTime: [],
      dateRange: {
        startDate: new Date().toISOString().split("T")[0],
        endDate: new Date().toISOString().split("T")[0],
      },
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
    await this.loadData();
  },
  methods: {
    getDifferenceBetweenArrays(array1, array2) {
      return array1.filter((time) => !array2.includes(time));
    },

    handleAddCourtPrice() {
      try {
        if (this.newPrice.price && this.newPrice.time) {
          this.newPrice.courtClusterId = this.cluster.id;

          // Thêm giá trị mới vào listCourtPrices
          this.listCourtPrices.push({ ...this.newPrice });

          // Reset newPrice sau khi thêm
          this.addPriceError = false;
          this.newPrice = {
            time: "",
            price: 50000,
            courtClusterId: "",
          };

          // Sắp xếp lại danh sách theo thời gian
          this.listCourtPrices.sort((a, b) => {
            const timeA = new Date(`1970/01/01 ${a.time}`);
            const timeB = new Date(`1970/01/01 ${b.time}`);
            return timeA - timeB;
          });
          // Đóng form thêm giá
          this.toggleAddPriceForm(false);
        } else {
          this.addPriceError = true;
        }
      } catch (error) {
        console.log(`handleAddCourtPrice ${error}`);
      }
    },

    async handleSave() {
      try {
        let defaultPriceRes = await createDefaultPrice(this.listCourtPrices);
        let addressRes = await putAddress(this.cluster.addressId, this.address);
        let clusterRes = await putCluster(this.cluster.id, this.cluster);
        if (addressRes.success && clusterRes.success) {
          Swal.fire("Thành công", "", "success");
        }
        this.loadData();
      } catch (error) {
        console.log(`handleSave cluster manage${error}`);
      }
    },
    async handleCreateCourtTimeSlots() {
      try {
        let validDate = this.validDateRange(
          this.dateRange.startDate,
          this.dateRange.endDate
        );
        console.log(validDate);

        if (validDate === true) {
          const result = await Swal.fire({
            title: "Bạn chắc chắn muốn áp dụng lịch?",
            icon: "question",
            showCancelButton: true,
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Hủy",
          });

          if (result.isConfirmed) {
            try {
              let defaultPriceRes = await createDefaultPrice(
                this.listCourtPrices
              );
              if (defaultPriceRes.success) {
                let autoCreateRes = await autoCreateCourtTimeSlot(
                  this.cluster.id,
                  this.generateDateArray(
                    this.dateRange.startDate,
                    this.dateRange.endDate
                  )
                );
                if (autoCreateRes.success) {
                  console.log(autoCreateRes.devMsg);
                  if (autoCreateRes.devMsg != null) {
                    Swal.fire(
                      "Đã có lịch tồn tại trong khoảng thời gian này!",
                      autoCreateRes.userMsg,
                      "error"
                    );
                  } else {
                    Swal.fire("Thành công!", "", "success");
                  }
                } else {
                  Swal.fire("Thất bại!", autoCreateRes.userMsg, "error");
                }
                console.log(autoCreateRes);
              }
            } catch (error) {
              console.log(`handleCreateCourtTimeSlots: ${error}`);
            }
          }
        } else {
          Swal.fire("Vui lòng chọn ngày hợp lệ", "", "error");
        }
      } catch (error) {
        console.log(`handleCreateCourtTimeSlots ${error}`);
      }
    },
    generateDateArray(startDate, endDate) {
      const start = new Date(startDate);
      const end = new Date(endDate);
      const dates = [];

      // Kiểm tra nếu ngày bắt đầu và ngày kết thúc trùng nhau
      if (start.getTime() === end.getTime()) {
        dates.push(start.toISOString().split("T")[0]);
      } else {
        // Lặp qua các ngày từ start đến end
        let currentDate = start;
        while (currentDate <= end) {
          dates.push(currentDate.toISOString().split("T")[0]);
          currentDate.setDate(currentDate.getDate() + 1);
        }
      }

      return dates;
    },
    validDateRange(startDate, endDate) {
      return !!startDate && !!endDate && startDate <= endDate;
    },
    removeCourtPrice(index) {
      this.listCourtPrices.splice(index, 1);
    },
    handleDateRange(range) {
      this.dateRange = range;
    },
    formatCurrency(amount) {
      return new Intl.NumberFormat("vi-VN", {
        style: "currency",
        currency: "VND",
      }).format(amount);
    },
    parseCurrency(value) {
      // Remove all non-digit characters
      const numericValue = value.replace(/[^\d]/g, "");
      return parseFloat(numericValue) || 0;
    },
    cancelChanges() {
      this.$router.push({ name: "manage-court-cluster" });
    },
    updatePrice(event) {
      let inputValue = event.target.value;

      // Remove all non-digit characters
      inputValue = inputValue.replace(/[^\d]/g, "");

      // Convert to number and check limit
      let numericValue = parseFloat(inputValue) || 0;
      if (numericValue > 10000000) {
        numericValue = 10000000;
      }
      if (numericValue < 1000) {
        numericValue = 1000;
      }
      // Update the model with the formatted value
      this.formattedPrice = numericValue.toString();
    },
    toggleAddPriceForm(io) {
      this.isAddPriceVisible = io;
    },
    async loadData() {
      try {
        // Lấy id từ route
        const courtClusterId = this.$route.params.id;
        const [clusterRes, courtPricesRes, courtsRes, listTimeRes] =
          await Promise.all([
            getCourtClusterById(courtClusterId),
            getCourtPricesByCourtClusterId(courtClusterId),
            getCourtsOfCourtCluster(courtClusterId),
            getListTime(),
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
        if (listTimeRes.success) {
          this.listTime = listTimeRes.data.map((time) => time + ":00");
        }
        let imgCourtRes = await getImageCourtUrl(this.cluster.id);
        if (imgCourtRes.success) {
          this.imageCourtUrl = imgCourtRes.data[0] ? imgCourtRes.data[0] : {};
        }
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
  column-gap: 12px;
  justify-content: start;
  height: 90px;
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
  width: 20%;
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
.court-photos img {
  object-fit: cover;
  width: 100%;
  height: 100%;
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

.apply-time-slot-btn {
  background-color: orange;
}

/* CSS form add price */
.add-price-form {
  position: fixed;
  top: 50%;
  left: 50%;
  background-color: white;
  transform: translate(-50%, -50%);
  width: 500px;
  padding: 32px;
  height: fit-content;
  display: flex;
  border-radius: 4px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  row-gap: 32px;
  align-items: center;
  box-shadow: 2px 2px 12px rgba(0, 0, 0, 0.5);
}

.add-price-form .add-price-input {
  width: 100%;
  padding: 4px 16px;
}
</style>