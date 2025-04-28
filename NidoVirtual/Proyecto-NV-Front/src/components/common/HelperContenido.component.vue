<template>
  <div class="layout-px-spacing">
    <TeleportHeader />
    <div class="row layout-top-spacing">
      <div class="col-xl-12 col-lg-12 col-md-12 col-12 layout-spacing">
        <div class="statbox panel box box-shadow">
          <div class="panel-heading" v-if="props.titulo">
            <div class="row">
              <div class="col-xl-12 col-md-12 col-sm-12 col-12">
                <h4>{{ props.titulo }}</h4>
                <h6 v-if="subtitulo" class="card-subtitle mt-2 mb-2 text-muted">{{ props.subtitulo }}</h6>
              </div>
            </div>
          </div>
          <div class="panel-body">
            <slot></slot>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { useHead } from '@unhead/vue'
import TeleportHeader from '@/components/common/TeleportHeader.vue'

const props = defineProps({
  titulo: { type: String, required: false },
  subtitulo: { type: String, required: false }
})
useHead({ title: props.titulo })
</script>
