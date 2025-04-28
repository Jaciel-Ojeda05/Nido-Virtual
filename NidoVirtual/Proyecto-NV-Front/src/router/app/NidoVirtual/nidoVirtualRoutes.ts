import { RouteRecordRaw } from 'vue-router';
import HomeLoyout from '@/layouts/app/NidoVirtual/HomeLoyout.vue';
import LoginView from '@/views/app/NidoVirtual/login/LoginView.vue';
import RestNuevaContrasenaView from '@/views/app/NidoVirtual/restNuevaContrasena/RestNuevaContrasenaView.vue';
import TableroView from '@/views/app/NidoVirtual/tablero/TableroView.vue';
import GeneroView from '@/views/app/NidoVirtual/Generos/GenerosView.vue';
import PlataformasView from '@/views/app/NidoVirtual/Plataformas/PlataformasView.vue';
import VideojuegosView from '@/views/app/NidoVirtual/Videojuegos/VideojuegosView.vue';
import ReportesView from '@/views/app/NidoVirtual/Reportes/ReportesView.vue';
import UsuariosView from '@/views/app/NidoVirtual/Usuarios/UsuariosView.vue';

const controlAccesoRoutes: RouteRecordRaw[] = [
  { path: '/NidoVirtual/login', name: 'Login', component: LoginView, meta: { title: 'NidoVirtual - Login' } },
  {
    path: '/NidoVirtual',
    component: HomeLoyout,
    children: [
      {
        path: 'restablecer-contrasenia',
        name: 'RestNuevaContrasenaCA',
        component: RestNuevaContrasenaView,
        meta: {
          title: 'Restablecer Contraseña', requiresAuth: true, system: 'NidoVirtual',
          breadcrumb: [
            { label: 'NidoVirtual' },
            { label: 'Restablecer Contraseña' }
          ]
        }
      },
      {
        path: 'Inicio',
        name: 'Inicio',
        component: TableroView,
        meta: {
          title: 'Inicio', requiresAuth: true, system: 'NidoVirtual',
          breadcrumb: [
            { label: 'NidoVirtual' },
            { label: 'Inicio' }
          ]
        }
      },
      {
        path: 'Generos',
        name: 'Generos',
        component: GeneroView,
        meta: {
          title: 'Generos', requiresAuth: true, system: 'NidoVirtual',
          breadcrumb: [
            { label: 'NidoVirtual' },
            { label: 'Genero' }
          ]
        }
      },
      {
        path: 'Plataformas',
        name: 'Plataformas',
        component: PlataformasView,
        meta: {
          title: 'Plataformas', requiresAuth: true, system: 'NidoVirtual',
          breadcrumb: [
            { label: 'NidoVirtual' },
            { label: 'Plataformas' }
          ]
        }
      },
      {
        path: 'Videojuegos',
        name: 'Videojuegos',
        component: VideojuegosView,
        meta: {
          title: 'Videojuegos', requiresAuth: true, system: 'NidoVirtual',
          breadcrumb: [
            { label: 'NidoVirtual' },
            { label: 'Videojuegos' }
          ]
        }
      },
      {
        path: 'Reportes',
        name: 'Reportes',
        component: ReportesView,
        meta: {
          title: 'Reportes', requiresAuth: true, system: 'NidoVirtual',
          breadcrumb: [
            { label: 'NidoVirtual' },
            { label: 'Reportes' }
          ]
        }
      },
      {
        path: 'Usuarios',
        name: 'Usuarios',
        component: UsuariosView,
        meta: {
          title: 'Usuarios', requiresAuth: true, system: 'NidoVirtual',
          breadcrumb: [
            { label: 'NidoVirtual' },
            { label: 'Usuarios' }
          ]
        }
      }
    ]
  }
];

export default controlAccesoRoutes;