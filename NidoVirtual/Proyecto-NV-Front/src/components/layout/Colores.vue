<template>
  <div class="dark-mode d-flex align-items-center">
    <a v-if="themeStore.state.dark_mode == 'light'" href="javascript:;" class="d-flex align-items-center"
      @click="toggleMode('dark')">
      <IconFeatherSun />
      <span class="ms-2">Claro</span>
    </a>
    <a v-if="themeStore.state.dark_mode == 'dark'" href="javascript:;" class="d-flex align-items-center"
      @click="toggleMode('system')">
      <IconFeatherMoon />
      <span class="ms-2">Oscuro</span>
    </a>
    <a v-if="themeStore.state.dark_mode == 'system'" href="javascript:;" class="d-flex align-items-center"
      @click="toggleMode('light')">
      <IconFeatherAirplay />
      <span class="ms-2">Sistema</span>
    </a>
  </div>
</template>
<script setup lang="ts">
import { onMounted } from "vue";
import useThemeStore from "@/stores/theme-store";
import IconFeatherSun from '@/assets/svg/IconFeatherSun.vue'
import IconFeatherMoon from "@/assets/svg/IconFeatherMoon.vue";
import IconFeatherAirplay from "@/assets/svg/IconFeatherAirplay.vue";

const themeStore = useThemeStore();
const toggleMode = (mode?: string) => {
  window.$appSetting.toggleMode(mode);
};
onMounted(() => {

  toggleMode();
});
</script>
