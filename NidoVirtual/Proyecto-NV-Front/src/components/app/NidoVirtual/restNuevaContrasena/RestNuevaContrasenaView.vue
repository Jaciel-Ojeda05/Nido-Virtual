<template>
  <HelperContenido titulo="Cambiar contraseña">
    <div class="container d-flex justify-content-center align-items-center pt-0">
      <div class="card p-4 shadow-lg" style="width: 100%; max-width: 700px;">
        <div class="text-center mb-4">
          <h5 class="mb-3">Criterios para la contraseña</h5>
          <ul class="list-group text-start ms-3">
            <li>Debe tener entre <b>8 y 16 caracteres</b></li>
            <li>Debe incluir al menos <b>una letra mayúscula</b></li>
            <li>Debe incluir al menos <b>una letra minúscula</b></li>
            <li>Debe incluir al menos <b>un número</b></li>
            <li>Debe incluir al menos <b>un carácter especial</b> como !, @, #, etc.</li>
          </ul>
        </div>
        <form @submit.stop.prevent="submit_form">
          <div class="col-md-12 mb-2">
            <label for="current-password" class="form-label">Nueva contraseña</label>
            <div class="input-group">
              <span class="input-group-text" style="background-color: white;">
                <i class="bi bi-lock"></i>
              </span>
              <input :type="pwd_type" class="form-control" v-model="is_contrasena_nueva" autocomplete="current-password"
                style="background-color: white; border-left: 0px;">
              <button class="btn btn-outline-secondary" type="button" @click="set_pwd_type">
                <i class="bi bi-eye-fill"></i>
              </button>
            </div>
          </div>
          <div class="col-md-12 mb-3">
            <label for="current-password" class="form-label">Confirmar nuevacontraseña</label>
            <div class="input-group">
              <span class="input-group-text" style="background-color: white;">
                <i class="bi bi-lock"></i>
              </span>
              <input :type="pwd_type2" class="form-control" v-model="is_confirma_contrasena"
                autocomplete="current-password" style="background-color: white; border-left: 0px;">
              <button class="btn btn-outline-secondary" type="button" @click="set_pwd_type2">
                <i class="bi bi-eye-fill"></i>
              </button>
            </div>
          </div>
          <div class="field-wrapper">
            <div class="col-12">
              <button :disabled="!(is_contrasena_nueva != '' && is_confirma_contrasena != '')" type="submit"
                class="btn btn-primary w-100"
                style="padding: 11px 14px; font-size: 16px; letter-spacing: 2px; margin: 25px 0 10px; border: none">
                Cambiar contraseña
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </HelperContenido>
</template>

<script setup>
import HelperContenido from '@/components/common/helperContenido/HelperContenido.vue';
import { AccesoRequest } from '@/core/models/Acceso/AccesoRequest.model';
import { ref } from 'vue';
import { useUserStore } from '@/stores/Acceso.store';
import { toast } from 'vue3-toastify';
import { postCambioClave } from '@/core/services/Nido-Vitual/Acceso/Auth.service';

const pwd_type = ref('password');
const pwd_type2 = ref('password');
const is_contrasena_nueva = ref('');
const is_confirma_contrasena = ref('');

const set_pwd_type = () => {
  if (pwd_type.value === 'password') {
    pwd_type.value = 'text';
  } else {
    pwd_type.value = 'password';
  }
};

const set_pwd_type2 = () => {
  if (pwd_type2.value === 'password') {
    pwd_type2.value = 'text';
  } else {
    pwd_type2.value = 'password';
  }
};

const submit_form = () => {
  if (is_contrasena_nueva.value != is_confirma_contrasena.value) {
    toast.error('Contraseña diferente');
    return;
  } else {
    let regex = /^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$/;
    if (regex.test(is_confirma_contrasena.value)) {
      let obj = new AccesoRequest();
      obj.clave = is_confirma_contrasena.value;
      obj.idUsuario = useUserStore().getUser().idUsuario;
      postCambioClave(
        obj,
        (data) => {
          toast.success(data.mensaje);
          is_contrasena_nueva.value = '';
          is_confirma_contrasena.value = '';
        },
        (error) => {
          toast.error('Error en la actualización de la contraseña:' + error.message);
        }
      );
    } else {
      toast.error('La contraseña no cumple con los criterios establecidos');
      return;
    }
  }
};
</script>

<style scoped>
.layout-px-spacing {
  min-height: calc(100vh - 170px) !important;
}

.auth-boxed .form-form .form-form-wrap form .field-wrapper svg.feather-eye {
  top: 45px;
  right: 33px;
}

.form-control.is-valid:focus,
.was-validated .form-control:valid:focus {
  border-color: #7b8c90;
  box-shadow: 0 0 0 .25rem rgba(255, 130, 0, .25);
}

.panel .panel-body {
  padding: 5px;
}
</style>