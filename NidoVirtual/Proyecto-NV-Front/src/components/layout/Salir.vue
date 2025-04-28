<template>
  <div class="dark-mode d-flex align-items-center me-3">
    <div class="auth-item">
      <div>
        <div class="d-flex flex-row align-items-baseline">
          <div class="nav-item dropdown">
            <a
              id="user-dropdown"
              @click.prevent="toggleDropdown"
              class="nav-link dropdown-toggle"
              href="#"
              role="button"
              aria-expanded="false"
              :class="{ 'text-white': isWhiteText }"
            >
              <i class="bi bi-person-circle"></i>
              <b class="px-2">{{ username }}</b>
            </a>
            <ul class="dropdown-menu" ref="dropdownMenu" aria-labelledby="user-dropdown">
              <li><a class="dropdown-item" href="#" @click="logout()">Salir</a></li>
              <li><a class="dropdown-item" href="#" @click="cambiarContra()">Cambiar contraseña</a></li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useRouter } from 'vue-router';
import { useUserStore } from '@/stores/Acceso.store';
import useThemeStore from '@/stores/theme-store';

const userStore = useUserStore();
const router = useRouter();
const username = ref('');
const dropdownMenu = ref<HTMLElement | null>(null);
const themeStore = useThemeStore();

const isWhiteText = computed(() => {
  return themeStore.state.menu_style === 'sin-menu' || themeStore.state.menu_style === 'horizontal';
});

function toggleDropdown(event: Event) {
  event.stopPropagation();
  if (dropdownMenu.value) {
    dropdownMenu.value.classList.toggle('show');
  }
}

document.addEventListener('click', (e) => {
  const target = e.target as HTMLElement;
  if (dropdownMenu.value && !dropdownMenu.value.contains(target) && !target.closest('#user-dropdown')) {
    dropdownMenu.value.classList.remove('show');
  }
});

onMounted(() => {
  const user = userStore.getUser();
  username.value = user?.correo!;
});

function logout() {
  userStore.removeUser();
  router.push('/NidoVirtual/login');
}

function cambiarContra(){
  router.push('/NidoVirtual/restablecer-contrasenia');
}
</script>

<style scoped>
.header-container .navbar .navbar-item .dark-mode a:hover {
  color: #13423D;
}

.header-container .navbar .navbar-item .dark-mode a {
  color: #13423D;
  white-space: nowrap;
}

.dropdown-menu.show {
  display: block; 
}
</style>