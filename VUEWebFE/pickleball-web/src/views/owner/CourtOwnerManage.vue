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
        <form class="signin-form" action="">
          <div class="p-input-container">
            <label for="name">Tên sân</label>
            <input type="text" id="name" placeholder="" />
          </div>
          <div class="p-input-container">
            <label for="">Thời gian mở cửa</label>
            <input type="openTime" id="openTime" />
          </div>
          <div class="p-input-container">
            <label for="">Thời gian đóng cửa</label>
            <input type="openTime" id="openTime" />
          </div>
          <div class="p-input-container">
            <label for="">Địa chỉ</label>
            <input type="text" id="city" placeholder="Khu vực" />
            <div class="p-input-container">
              <input type="text" id="district" placeholder="Quận/Huyện..." />
            </div>
            <div class="p-input-container">
              <input type="text" id="ward" placeholder="Tên phường..." />
            </div>
            <div class="p-input-container">
              <input type="text" id="street" placeholder="Tên đường..." />
            </div>
          </div>
          <div>
            <label for="">Số lượng sân: </label>
            <input type="number" />
          </div>
          <div class="p-input-container">
            <label for="">Mô tả</label>
            <textarea name="" id="" cols="30" rows="3"></textarea>
          </div>
        </form>
        <div class="left-bottom">
          <button class="p-button this-scoped-btn">Tạo cụm sân</button>
        </div>
      </div>
      <div class="right-part">
        <div class="font-size-32">Thêm ảnh</div>
        <div class="upload-container">
          <div
            v-for="(image, index) in uploadedImages"
            :key="index"
            class="preview-image"
          >
            <img :src="image" alt="Uploaded image" />
            <button @click="removeImage(index)">X</button>
          </div>
          <input
            type="file"
            accept="image/*"
            @change="handleFileUpload"
            :disabled="uploadedImages.length >= 3"
          />
          <p v-if="uploadedImages.length >= 3">
            Bạn chỉ có thể tải lên tối đa 3 hình ảnh.
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CourtRsItem from "../search/CourtRsItem.vue";
import { getCourtClusterByCourtOwner } from "../../scripts/apiService.js";
export default {
  components: {
    CourtRsItem,
  },
  data() {
    return {
      isFormVisible: false, // Kiểm soát hiển thị của form
      courtClusters: [],
      uploadedImages: [], // Lưu trữ URL của các hình ảnh được tải lên
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
      const files = event.target.files;
      if (files.length + this.uploadedImages.length > 3) {
        alert("Bạn chỉ có thể tải lên tối đa 3 hình ảnh.");
        return;
      }
      Array.from(files).forEach((file) => {
        const reader = new FileReader();
        reader.onload = (e) => {
          this.uploadedImages.push(e.target.result); // Thêm URL vào mảng
        };
        reader.readAsDataURL(file); // Đọc tệp dưới dạng URL
      });
    },
    removeImage(index) {
      this.uploadedImages.splice(index, 1); // Xóa ảnh khỏi mảng
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
  background-color: var(--gray-background);
  background-repeat: no-repeat;
  background-size: cover;
  background-position-x: calc(100% + 64px);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
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