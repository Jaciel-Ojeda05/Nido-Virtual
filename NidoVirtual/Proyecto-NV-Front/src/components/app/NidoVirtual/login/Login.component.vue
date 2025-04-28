<template>
  <div class="container d-flex justify-content-center align-items-center vh-100">
    <div class="card p-4 shadow-lg" style="width: 100%; max-width: 400px;">
      <div class="text-center mb-4">
        <img src="@/assets/images/LogoNidoVirtual.avif" class="img-fluid mb-2" style="height: 50%; width: 50%;"
          alt="Descripción de la imagen">
        <h4 class="brand-name" style="font-weight: 900;">Nido Virtual</h4>
        <p>Ingresar correo electrónico y contraseña para continuar</p>
      </div>
      <form @submit.prevent="loginSubmit">
        <div class="col-md-12 mb-3">
          <label for="id_usuario" class="form-label">Correo Electrónico</label>
          <div class="input-group">
            <input v-model="clave" type="text" class="form-control" required>
          </div>
        </div>
        <div class="col-md-12 mb-3">
          <label for="current-password" class="form-label">Contraseña</label>
          <div class="input-group">
            <input :type="pwd_type" class="form-control" v-model="password" autocomplete="current-password" required>
            <button class="btn btn-outline-secondary" type="button" @click="set_pwd_type">
              <i class="bi bi-eye-fill"></i>
            </button>
          </div>
        </div>
        <div class="field-wrapper">
          <div class="col-12">
            <button type="submit" class="btn btn-primary w-100"
              style="padding: 11px 14px; font-size: 16px; letter-spacing: 2px; margin: 25px 0 10px; border: none">
              Acceder
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { toast } from 'vue3-toastify';
import { ResponseModel } from '@/core/models/Response/Response.model';
import { useUserStore } from '@/stores/Acceso.store';
import { login } from '@/core/services/Nido-Vitual/Acceso/Auth.service';
import { AccesoRequest } from '@/core/models/Acceso/AccesoRequest.model';

const clave = ref<string | null>(null);
const password = ref('');
const router = useRouter();
const pwd_type = ref('password');
const authStore = useUserStore();

const set_pwd_type = () => {
  pwd_type.value = pwd_type.value === 'password' ? 'text' : 'password';
};

function loginSubmit() {
  if (!clave.value || !password.value) {
    toast.warning('Por favor, ingresa tu correo electrónico y contraseña.');
    return;
  }
  const loginPayload = new AccesoRequest(password.value, parseInt(clave.value));
  login(
    { correo: clave.value, userPassword: password.value },
    (data: ResponseModel<any>) => {
      if (data.data?.token) {
        authStore.setUser(data.data);
        router.push('/NidoVirtual/Inicio');
      } else {
        toast.error(data.mensaje || 'Credenciales inválidas.');
      }
    },
    (error) => {
      toast.error('Error al iniciar sesión: ' + error.message);
      console.error('Error de login:', error);
    }
  );
}
</script>

<style scoped>
/* Puedes mantener tus estilos existentes o modificarlos */
</style>