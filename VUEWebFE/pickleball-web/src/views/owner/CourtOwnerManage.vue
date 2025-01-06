<template>
  <div class="container">
    <div class="navigation">
      <router-link class="nav-option" :to="{ name: 'owner-manage-booking' }"
        >Quản lý booking</router-link
      >
      <router-link class="nav-option" :to="{ name: 'manage-court-cluster' }"
        >Quản lý sân</router-link
      >
    </div>
    <div class="content">
      <CourtRsItem
        v-for="(cluster, index) in courtClusters"
        :key="index"
        :manageMode="true"
        :courtClusterData="cluster"
      />
      <button class="add-btn p-icon-add-btn" @click="showForm"></button>
    </div>
  </div>

  <div class="overlay" @click="hideForm" v-if="isFormVisible">
    <div class="form-container" @click.stop>
      <div class="left-part">
        <div class="left-top">
          <div class="font-size-32">Thêm cụm sân</div>
        </div>
        <div class="signin-form" action="">
          <div class="p-input-container">
            <label for="name">Tên sân</label>
            <input
              type="text"
              id="name"
              v-model="newCluster.name"
              placeholder="Nhập tên sân"
            />
          </div>
          <div class="p-input-container">
            <label for="openTime">Thời gian mở cửa</label>
            <input type="time" id="openTime" v-model="newCluster.openTime" />
          </div>
          <div class="p-input-container">
            <label for="closeTime">Thời gian đóng cửa</label>
            <input type="time" id="closeTime" v-model="newCluster.closeTime" />
          </div>
          <div class="p-input-container">
            <label for="numberOfCourts">Số lượng sân</label>
            <select v-model="newCluster.numberOfCourts" style="width: 44px">
              <option v-for="i in 8" :key="i" :value="i">{{ i }}</option>
            </select>
          </div>
          <div class="p-input-container">
            <label for="description">Mô tả</label>
            <textarea
              v-model="newCluster.description"
              id="description"
              cols="30"
              rows="3"
            ></textarea>
          </div>
        </div>
        <div class="left-bottom">
          <button class="p-button this-scoped-btn" @click="submitForm">
            Tạo cụm sân
          </button>
        </div>
      </div>
      <div class="right-part">
        <div class="p-input-container signin-form">
          <label for="city">Địa chỉ</label>
          <input
            type="text"
            id="city"
            v-model="newCluster.city"
            placeholder="Khu vực"
          />
          <input
            type="text"
            id="district"
            v-model="newCluster.district"
            placeholder="Quận/Huyện..."
          />
          <input
            type="text"
            id="ward"
            v-model="newCluster.ward"
            placeholder="Tên phường..."
          />
          <input
            type="text"
            id="street"
            v-model="newCluster.street"
            placeholder="Tên đường..."
          />
        </div>
        <div class="upload-container">
          <div class="font-size-32">Thêm ảnh</div>
          <div v-if="uploadedImage" class="preview-image">
            <img :src="uploadedImage" alt="Uploaded image" />
            <button @click="removeImage">X</button>
          </div>
          <input
            type="file"
            accept="image/*"
            @change="handleFileUpload"
            :disabled="uploadedImage !== null"
          />
          <p v-if="uploadedImage !== null">
            Bạn chỉ có thể tải lên một hình ảnh.
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import CourtRsItem from "../search/CourtRsItem.vue";
import {
  getCourtClusterByCourtOwner,
  createCourtCluster,
} from "../../scripts/apiService.js";
export default {
  components: {
    CourtRsItem,
  },
  data() {
    return {
      isFormVisible: false, // Kiểm soát hiển thị của form
      courtClusters: [],
      uploadedImage: null, // Lưu trữ URL của hình ảnh được tải lên
      newCluster: {
        name: "",
        openTime: "",
        closeTime: "",
        numberOfCourts: 1,
        description: "",
        city: "",
        district: "",
        ward: "",
        street: "",
        image: null,
      },
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    hideForm() {
      this.isFormVisible = false; // Ẩn form
    },
    showForm() {
      this.isFormVisible = true; // Hiển thị form
    },
    handleFileUpload(event) {
      const file = event.target.files[0];
      if (file) {
        this.newCluster.image = file;
        const reader = new FileReader();
        reader.onload = (e) => {
          this.uploadedImage = e.target.result; // Thay thế URL của ảnh mới
        };
        reader.readAsDataURL(file); // Đọc tệp dưới dạng URL
      }
    },
    removeImage() {
      this.uploadedImage = null; // Xóa ảnh hiện tại
    },
    async loadData() {
      try {
        let clusterReponse = await getCourtClusterByCourtOwner();
        if (clusterReponse.success) {
          this.courtClusters = clusterReponse.data;
        }
      } catch (error) {
        console.log(`loadData CourtOwnerManage.vue: ${error}`);
      }
    },
    async submitForm() {
      try {
        const formData = new FormData();
        formData.append("name", this.newCluster.name);
        formData.append("description", this.newCluster.description);
        formData.append("openingTime", this.newCluster.openTime);
        formData.append("closingTime", this.newCluster.closeTime);
        formData.append("city", this.newCluster.city);
        formData.append("district", this.newCluster.district);
        formData.append("ward", this.newCluster.ward);
        formData.append("street", this.newCluster.street);
        formData.append("numberOfCourts", this.newCluster.numberOfCourts);
        // If there's an image uploaded, append it as well
        if (this.newCluster.image) {
          const file = this.newCluster.image; // Assuming it's a File object
          formData.append("image", file);
        }
        const res = await createCourtCluster(formData);
        if (res.success) {
          Swal.fire("Thành công!", "", "success");
          this.loadData();
          this.hideForm();
        } else {
          Swal.fire("Lỗi!", "", "error");
        }
      } catch (error) {
        console.error("Error submitting form:", error);
      }
    },
    resetForm() {
      this.newCluster = {
        name: "",
        openTime: "",
        closeTime: "",
        numberOfCourts: 1,
        description: "",
        city: "",
        district: "",
        ward: "",
        street: "",
        image: null,
      };
      this.uploadedImage = null;
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
  grid-template-columns: 1fr 1fr;
  gap: 12px;
  width: 100%;
  border-radius: 4px;
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

/* add button */
.add-btn {
  width: 52px;
  height: 52px;
  background-size: 52px;
  background-repeat: no-repeat;
  background-position: center;
  border-radius: 1000px;
  border: none;
  position: absolute;
  bottom: 32px;
  right: 32px;
  cursor: pointer;
  box-shadow: 1px 1px 4px rgba(0, 0, 0, 0.5);
}

/* CSS form */
.form-container {
  position: fixed;
  z-index: 100;
  top: 50%;
  left: 50%;
  background-color: white;
  transform: translate(-50%, -50%);
  width: 1200px;
  height: 85%;
  /* background-color: burlywood; */
  display: flex;
  border-radius: 4px;
  overflow: hidden;
  box-shadow: 2px 2px 12px rgba(0, 0, 0, 0.5);
}

.left-part {
  background-color: white;
  width: 60%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  row-gap: 32px;
  flex-shrink: 0;
}

.signin-form {
  padding: 24px 72px 24px 72px;
  width: 100%;
  flex-shrink: 0;
  display: flex;
  flex-direction: column;
  justify-content: center;
  row-gap: 24px;
}

.p-input-container {
  position: relative;
  display: flex;
  flex-direction: column;
  row-gap: 6px;
}

.p-input-container input {
  height: 36px;
  border: 1px solid rgba(0, 0, 0, 0.05);
  border-radius: 4px;
  outline: none;
  padding: 0px 18px 0px 18px;
  background-color: var(--gray-background);
  text-align: start;
}

input:focus {
  border: 1px solid var(--topic-color-500);
  background-color: white;
}

.left-top {
  display: flex;
  justify-content: start;
  padding: 0 72px 0 72px;
}

.left-bottom {
  display: flex;
  justify-content: center;
}

.big-title {
  font-size: 32px;
  font-family: roboto-bold;
}

.this-scoped-btn {
  width: 212px;
  background-color: green;
}

/* CSS phần bên phải form */
.right-part {
  width: 40%;
  height: 100%;
  background-color: white;
  background-repeat: no-repeat;
  background-size: cover;
  background-position-x: calc(100% + 64px);
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: center;
  padding: 24px;
}

/* CĐ upload image */
.upload-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 12px;
}

.preview-image {
  position: relative;
  display: inline-block;
  margin: 5px;
}

.preview-image img {
  max-width: 100px;
  max-height: 100px;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.preview-image button {
  position: absolute;
  top: 0;
  right: 0;
  background-color: red;
  color: white;
  border: none;
  border-radius: 50%;
  width: 20px;
  height: 20px;
  cursor: pointer;
}
</style>