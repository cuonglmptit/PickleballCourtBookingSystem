<template>
  <div class="content-container">
    <router-view>
    </router-view>
  </div>
</template>

<script>
export default {};
</script>

<style>
.content-container {
  background-color: rgba(0, 0, 0, 0.01);
  width: 100%;
  min-height: 100vh;
  background-color: wheat;
  display: flex;
  padding-top: 96px;
  background-color: var(--gray-background);
}
</style>