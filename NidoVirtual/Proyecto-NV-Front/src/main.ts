import { createApp } from 'vue';
import { createPinia } from 'pinia';
import { createHead } from '@unhead/vue';
import { PerfectScrollbarPlugin } from 'vue3-perfect-scrollbar';
import Swal from "sweetalert2";
import router from './router/index.router';
import App from './App.vue';
import 'vue3-toastify/dist/index.css';
import * as bootstrap from 'bootstrap';
import 'bootstrap-icons/font/bootstrap-icons.css';
import '@fortawesome/fontawesome-free/css/all.css';
import 'boxicons/css/boxicons.min.css';
import appSetting from './config/app-layout-setting';
import 'vue3-perfect-scrollbar/style.css';
import "vue3-form-wizard/dist/style.css";

import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

declare global {
  interface Window {
    bootstrap: any;
    $appSetting: any;
    Swal: any;
  }
}

function initializeApp() {
  try {
    const app = createApp(App);
    const pinia = createPinia();
    app.use(pinia);
    const head = createHead();
    window.bootstrap = bootstrap;
    window.$appSetting = appSetting;
    window.$appSetting.init();
    window.Swal = Swal;
    app.use(router);
    app.use(head);
    app.use(PerfectScrollbarPlugin);
    const vuetify = createVuetify({
      components,
      directives,
    });
    app.use(vuetify);
    app.mount('#app');
  } catch (error) {
    console.error('Error al inicializar la aplicación:', error);
  }
}

initializeApp();