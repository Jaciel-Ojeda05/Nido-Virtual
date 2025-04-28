<template>
  <div class="modal fade show d-block" tabindex="-1">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">{{ title }}</h5>
          <button type="button" class="btn-close" @click="$emit('close-modal')" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <p>{{ message }}</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-danger btn-primary2" @click="$emit('close-modal')">Cancelar</button>
          <button type="button" class="btn  btn-primary" @click="$emit('confirm')">Aceptar</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const props = defineProps<{
  title: string;
  message: string;
}>();

const emit = defineEmits(['confirm', 'close-modal']);
</script>

<style scoped></style>