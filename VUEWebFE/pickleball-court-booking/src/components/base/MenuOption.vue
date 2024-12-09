<template>
  <component
    :is="isRouterLink ? 'router-link' : 'div'"
    :to="isRouterLink ? to : undefined"
    :class="['p-option']"
  >
    <div :class="iconClass"></div>
    {{ optionTitle }}
  </component>
</template>

<script>
export default {
  props: {
    optionTitle: {
      type: String,
      required: true,
    },
    isRouterLink: {
      type: Boolean,
      default: false, // Mặc định là không phải router-link
    },
    to: {
      type: [String, Object],
      default: null, // Chỉ sử dụng khi là router-link
    },
    iconClass: {
      type: String, // Dùng String để truyền tên class
      default: "", // Default class
    },
    isActive: {
      type: Boolean,
      default: false, // Nhận giá trị isActive từ parent
    },
  },
};
</script>

<style scoped>
.p-option {
  display: flex;
  justify-content: center;
  align-items: center;
  column-gap: 4px;
  width: 100%;
  height: fit-content;
  padding: 6px 0px;
  min-width: 150px;
  box-sizing: border-box;
  border: unset;
  color: black;
  border-radius: 4px;
  font-size: 16px;
  margin: 4px;
  font-family: roboto-medium;
  background-color: var(--topic-color-200);
  cursor: pointer;
  text-decoration: none;
}

.p-option:hover {
  transform: scale(1.03);
}

.p-option-selected {
  background-color: white;
}

.div-option-icon {
  width: 24px;
  height: 24px;
  background-repeat: no-repeat;
}

.router-link-active {
  background-color: white;
  border: 2px solid white;
}
</style>