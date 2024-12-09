<template>
  <main class="main">
    <div class="main-content-container">
      <router-view></router-view>
    </div>
  </main>
</template>
<script>
export default {};
</script>
<style>
/* CSS CHO MAIN PART */

.main-content-container {
  padding-top: calc(112px + 24px);
  padding-left: 56px;
  padding-right: 56px;
  margin: 0px 200px;
  width: 100%;
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 36px;
}

.main-content-container .item-container {
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>