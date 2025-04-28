<template>
  <footer :class="{ dark: themeStore.state.is_dark_mode }" class="footer-wrapper">
    <div class="footer-section f-section-1">
      <div class="container">
        <div class="col-12">
          <p :class="{ 'dark-text': themeStore.state.is_dark_mode }">
            ¿Eres un apasionado de los videojuegos? ¡Síguenos para estar al tanto de las últimas novedades, ofertas y
            nuestra comunidad gamer!
          </p>
        </div>
        <div class="redSocial col-12 d-flex">
          <ul class="social-icons">
            <li>
              <a href="https://www.facebook.com/" target="_blank" rel="noopener noreferrer">
                <i class="fab fa-facebook-f me-2"></i>
              </a>
            </li>
            <li>
              <a href="https://www.instagram.com/" target="_blank" rel="noopener noreferrer">
                <i class="fab fa-instagram"></i>
              </a>
            </li>
            <li>
              <a href="https://store.steampowered.com/" target="_blank" rel="noopener noreferrer">
                <i class="fab fa-steam"></i>
              </a>
            </li>
            <li>
              <a href="https://twitter.com/" target="_blank" rel="noopener noreferrer">
                <i class="fab fa-twitter"></i>
              </a>
            </li>
          </ul>
        </div>
      </div>
    </div>
    <div class="footer-section f-section-2">
      <p :class="{ 'dark-text': themeStore.state.is_dark_mode }" class="ms-2">
        © 2025 Tu Plataforma de Videojuegos. Todos los derechos reservados.
      </p>
    </div>
  </footer>
</template>

<script setup lang="ts">
import useThemeStore from '@/stores/theme-store';
const themeStore = useThemeStore();
</script>

<style scoped>
footer {
  background-color: #f1f1f1;
  color: #000;
  padding: 20px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 15px;
}

footer.dark {
  background-color: #060818;
  color: #fff;
}

.footer-wrapper .footer-section p {
  color: #13423D !important;
  text-align: center;
  margin-bottom: 10px;
}

.footer-wrapper .footer-section p.dark-text {
  color: #fff !important;
}

.social-icons {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  gap: 20px;
}

.social-icons li a {
  color: #555;
  font-size: 1.5em;
  transition: color 0.3s ease-in-out;
}

.social-icons li a:hover {
  color: #007bff;
}

footer.dark .social-icons li a {
  color: #eee;
}

footer.dark .social-icons li a:hover {
  color: #66b3ff;
}

.footer-section.f-section-2 p {
  font-size: 0.8em;
  color: #777 !important;
}

.footer-section.f-section-2 p.dark-text {
  color: #bbb !important;
}

.redSocial{
  padding-left: 100px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 15px;
}
</style>
