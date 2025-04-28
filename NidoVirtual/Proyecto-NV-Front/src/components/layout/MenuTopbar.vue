<template>
  <nav class="topbar">
    <ul class="list-unstyled menu-categories" id="topAccordion">
      <li class="menu single-menu">
        <router-link :to="{ name: 'Inicio' }" class="dropdown-toggle autodroprown">
          <div class="">
            <i class="fas fa-home"></i> <span class="ms-2">Inicio</span>
          </div>
        </router-link>
      </li>
      <li class="menu single-menu">
        <router-link :to="{ name: 'Generos' }" class="dropdown-toggle autodroprown">
          <div class="">
            <i class="fas fa-tags"></i> <span class="ms-2">Géneros</span>
          </div>
        </router-link>
      </li>
      <li class="menu single-menu">
        <router-link :to="{ name: 'Plataformas' }" class="dropdown-toggle autodroprown">
          <div class="">
            <i class="fa-regular fa-chess-rook"></i> <span class="ms-2">Plataformas</span>
          </div>
        </router-link>
      </li>
      <li class="menu single-menu">
        <router-link :to="{ name: 'Videojuegos' }" class="dropdown-toggle autodroprown">
          <div class="">
            <i class="fas fa-chart-bar"></i> <span class="ms-2">Videojuegos</span>
          </div>
        </router-link>
      </li>
      <li class="menu single-menu">
        <router-link :to="{ name: 'Reportes' }" class="dropdown-toggle autodroprown">
          <div class="">
            <i class="fas fa-chart-bar"></i> <span class="ms-2">Reportes</span>
          </div>
        </router-link>
      </li>
      <li class="menu single-menu">
        <router-link :to="{ name: 'Usuarios' }" class="dropdown-toggle autodroprown">
          <div class="">
            <i class="fas fa-users"></i> <span class="ms-2">Usuarios</span>
          </div>
        </router-link>
      </li>
    </ul>
  </nav>
</template>

<script setup lang="ts">
</script>

<style scoped></style>