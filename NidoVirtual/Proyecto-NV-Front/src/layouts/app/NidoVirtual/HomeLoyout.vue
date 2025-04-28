<template>
  <div :class="[themeStore.state.layout_style, themeStore.state.menu_style]">
    <div>
      <Header></Header>
      <div class="main-container" id="container" :class="[
        !themeStore.state.is_show_sidebar ? 'sidebar-closed sbar-open' : '',
        themeStore.state.menu_style === 'collapsible-vertical' ? 'collapsible-vertical-mobile' : ''
      ]">
        <div class="overlay" :class="{ show: !themeStore.state.is_show_sidebar }"
          @click="themeStore.toggleSideBar(!themeStore.state.is_show_sidebar)"></div>
        <div class="search-overlay" :class="{ show: themeStore.state.is_show_search }"
          @click="themeStore.toggleSearch(!themeStore.state.is_show_search)"></div>
        <Sidebar v-if="themeStore.state.menu_style !== 'sin-menu'"></Sidebar>
        <div id="content" class="main-content">
          <router-view />
          <Footer></Footer>
        </div>
        <app-settings />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onBeforeMount } from 'vue';
import { useThemeStore } from '@/stores/theme-store'
import appSettings from '@/components/layout/AppSettings.te.vue'
const themeStore = useThemeStore()

import Header from '@/components/layout/Header.vue'
import Sidebar from '@/components/layout/Sidebar.vue'
import Footer from '@/components/layout/footer.vue'

onBeforeMount(() => {
  themeStore.toggleMenuStyle('collapsible-vertical');
});  
</script>
