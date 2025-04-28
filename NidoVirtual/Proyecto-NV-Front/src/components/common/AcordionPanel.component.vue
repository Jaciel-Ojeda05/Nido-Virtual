<template>
  <div class="accordion" :id="props.id_acordion">
    <slot></slot>
  </div>
</template>

<script setup lang="ts">
const props = defineProps({
  id_acordion: { type: String, required: true }
})
</script>
