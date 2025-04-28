<template>
  <div class="sidebar-wrapper sidebar-theme">
    <nav ref="menu" id="sidebar">
      <div class="logo">
        <img src="@/assets/images/LogoNidoVirtual.avif" alt="Logo Nido Virtual">
      </div>
      <div class="shadow-bottom"></div>
      <perfect-scrollbar class="list-unstyled menu-categories" tag="ul" :options="{
        wheelSpeed: 0.5,
        swipeEasing: !0,
        minScrollbarLength: 40,
        maxScrollbarLength: 300,
        suppressScrollX: true
      }">
        <li class="menu">
          <a class="dropdown-toggle" data-bs-toggle="" data-bs-target="" aria-controls="inicio" aria-expanded="false">
            <div>
              <i class="fas fa-home"></i>
              <span>
                <router-link :to="{ name: 'Inicio' }" @click="toggleMobileMenu">Inicio</router-link>
              </span>
            </div>
          </a>
        </li>
        <li class="menu">
          <a class="dropdown-toggle" data-bs-toggle="" data-bs-target="" aria-controls="generos" aria-expanded="false">
            <div>
              <i class="fas fa-tags"></i>
              <span>
                <router-link :to="{ name: 'Generos' }" @click="toggleMobileMenu">Géneros</router-link>
              </span>
            </div>
          </a>
        </li>
        <li class="menu">
          <a class="dropdown-toggle" data-bs-toggle="" data-bs-target="" aria-controls="plataformas"
            aria-expanded="false">
            <div>
              <i class="fa-regular fa-chess-rook"></i>
              <span>
                <router-link :to="{ name: 'Plataformas' }" @click="toggleMobileMenu">Plataformas</router-link>
              </span>
            </div>
          </a>
        </li>
        <li class="menu">
          <a class="dropdown-toggle" data-bs-toggle="" data-bs-target="" aria-controls="videojuegos"
            aria-expanded="false">
            <div>
              <i class="fas fa-gamepad"></i>
              <span>
                <router-link :to="{ name: 'Videojuegos' }" @click="toggleMobileMenu">Videojuegos</router-link>
              </span>
            </div>
          </a>
        </li>
        <li class="menu">
          <a class="dropdown-toggle" data-bs-toggle="" data-bs-target="" aria-controls="reportes" aria-expanded="false">
            <div>
              <i class="fas fa-chart-bar"></i>
              <span>
                <router-link :to="{ name: 'Reportes' }" @click="toggleMobileMenu">Reportes</router-link>
              </span>
            </div>
          </a>
        </li>
        <li class="menu">
          <a class="dropdown-toggle" data-bs-toggle="" data-bs-target="" aria-controls="usuarios" aria-expanded="false">
            <div>
              <i class="fas fa-users"></i>
              <span>
                <router-link :to="{ name: 'Usuarios' }" @click="toggleMobileMenu">Usuarios</router-link>
              </span>
            </div>
          </a>
        </li>
      </perfect-scrollbar>
    </nav>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useThemeStore } from '@/stores/theme-store';

const themeStore = useThemeStore();
const menu = ref<HTMLElement | null>(null);

onMounted(() => {
  const selector = document.querySelector(`#sidebar a[href="${window.location.pathname}"]`);
  if (selector) {
    const ul = selector.closest('ul.collapse');
    if (ul) {
      let ele: any = ul.closest('li.menu')?.querySelectorAll('.dropdown-toggle');
      if (ele) {
        ele = ele[0];
        setTimeout(() => {
          ele.click();
        });
      }
    } else {
      (selector as HTMLInputElement | null)?.click();
    }
  }
});

const toggleMobileMenu = () => {
  if (window.innerWidth < 991) {
    themeStore.toggleSideBar(!themeStore.isShowSidebar());
  }
};
</script>

<style scoped>
.logo {
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
  transition: width 0.3s ease, height 0.3s ease, margin 0.3s ease;
}

.logo img {
  display: block;
  transition: all 0.3s ease; 
}
</style>