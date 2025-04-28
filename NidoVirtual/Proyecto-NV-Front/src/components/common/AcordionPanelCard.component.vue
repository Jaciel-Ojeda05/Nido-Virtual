<template>
  <div class="card">
    <div class="card-header" role="tab">
      <div class="mb-0 collapsed" role="menu" data-bs-toggle="collapse" :data-bs-target="'#' + $props.id_card"
        aria-expanded="false" :aria-controls="$props.id_card">
        <span class="q-title">{{ props.titulo }}</span>
        <div class="icons">
          <IconChevronDown />
        </div>
      </div>
    </div>
    <div :id="props.id_card" class="collapse" :aria-labelledby="$props.id_card" data-bs-parent="#simple_faq">
      <div class="card-body">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import '/src/assets/sass/info.registro.scss'
import IconChevronDown from '@/assets/svg/IconChevronDown.vue'

const props = defineProps({
  id_card: { type: String, required: true },
  titulo: { type: String, required: true }
})
</script>
