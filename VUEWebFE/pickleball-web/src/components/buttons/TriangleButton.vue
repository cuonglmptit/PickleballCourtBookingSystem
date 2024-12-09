<template>
  <button class="triangle-button">
    <slot name="name">
        Button
    </slot>
  </button>
</template>

<script>
export default {};
</script>

<style scoped>
.triangle-button {
  border-radius: 4px;
  border: none;
  height: 36px;
  width: 100%;
  min-width: 72px;
  background-color: var(--topic-color2-500);
  color: white;
  font-family: roboto-medium;
  font-size: 16px;
  cursor: pointer;
  box-sizing: border-box;
}
</style>