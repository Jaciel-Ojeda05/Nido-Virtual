<template>
  <ul class="navbar-item flex-row ms-md-0 ms-auto">
    <li class="nav-item align-self-center search-animated" :class="{ 'show-search': themeStore.state.is_show_search }">
      <IconFeatherSearch />
      <form class="form-inline search-full form-inline search"
        :class="{ 'input-focused': themeStore.state.is_show_search }">
        <div class="search-bar">
          <input type="text" class="form-control search-form-control ms-lg-auto" placeholder="Buscar..." />
        </div>
      </form>
    </li>
  </ul>
</template>

<script setup lang="ts">
import { useThemeStore } from '@/stores/theme-store'
import IconFeatherSearch from "@/assets/svg/IconFeatherSearch.vue"
const themeStore = useThemeStore()
</script>
