<template>
  <div>
    <div class="header-container fixed-top">
      <div class="">
        <header class="header navbar navbar-expand-sm">
          <div v-if="themeStore.state.menu_style !== 'sin-menu'">
            <a href="javascript:void(0);" class="sidebarCollapse" data-placement="bottom"
              @click="themeStore.toggleSideBar(!themeStore.state.is_show_sidebar)">
            </a>
          </div>
          <div class="d-none horizontal-menu">
            <a href="javascript:void(0);" class="sidebarCollapse" data-placement="bottom"
              @click="themeStore.toggleSideBar(!themeStore.state.is_show_sidebar)">
              <IconHamburger />
            </a>
          </div>
          <div class="px-2">
            <ModuloArea v-if="themeStore.state.is_visible_modulo" />
          </div>
          <div class="navbar-item flex-row ms-md-auto">
            <div class="col-6"></div>
            <div class="col-6 d-flex justify-content-end">
              <template v-if="props.autenticar">
                <Salir />
              </template>
            </div>
          </div>
        </header>
      </div>
    </div>
    <div class="sub-header-container fixed-top" v-if="themeStore.state.is_visible_subheader">
      <header class="header navbar navbar-expand-sm">
        <div id="breadcrumb" class="vue-portal-target"></div>
      </header>
    </div>
    <div class="topbar-nav header navbar" role="banner">
      <MenuTopBar />
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, onBeforeUnmount } from 'vue'
import useThemeStore from '@/stores/theme-store'
import MenuTopBar from './MenuTopbar.vue'
import ModuloArea from './ModuloArea.vue'
import Salir from './Salir.vue'

import IconHamburger from '@/assets/svg/IconHamburger.vue'

const themeStore = useThemeStore()
const isMobile = ref(false)

const checkWindowSize = () => {
  isMobile.value = window.innerWidth <= 991
}

onBeforeUnmount(() => {
  window.removeEventListener('resize', checkWindowSize)
})

onMounted(() => {
})

interface Props {
  autenticar?: boolean
}

const props = withDefaults(defineProps<Props>(), { autenticar: true })
</script>
