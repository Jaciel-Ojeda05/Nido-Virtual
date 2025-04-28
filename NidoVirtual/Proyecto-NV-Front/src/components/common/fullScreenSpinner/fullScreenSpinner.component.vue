<template>
  <div v-if="isLoading" class="spinner-overlay">
    <img src="@/assets/images/NidoVirtualGif.gif" alt="Loading..." class="spinner-image" />
  </div>
</template>

<script setup lang="ts">
import { useLoading } from "@/core/composables/useLoading";
const { isLoading } = useLoading();
</script>

<style scoped></style>