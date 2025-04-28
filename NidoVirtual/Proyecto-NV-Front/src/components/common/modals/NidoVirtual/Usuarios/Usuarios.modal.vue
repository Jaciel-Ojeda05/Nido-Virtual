<template>
    <div class="modal fade show d-block" tabindex="-1">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{ isEdit ? 'Modificar Usuario' : 'Nuevo Usuario' }}</h5>
                    <button type="button" class="btn-close" @click="$emit('close-modal')" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="handleSubmit">
                        <div class="row pb-3 pt-3">
                            <div class="col-md-6">
                                <label for="nombreUsuario" class="form-label">Nombre</label>
                                <input type="text" class="form-control" v-model="usuario.nombreUsuario" required>
                            </div>
                            <div class="col-md-6">
                                <label for="apePaterno" class="form-label">Apellido Paterno</label>
                                <input type="text" class="form-control" v-model="usuario.apePaterno" required>
                            </div>
                            <div class="col-md-6 pt-2">
                                <label for="apeMaterno" class="form-label">Apellido Materno</label>
                                <input type="text" class="form-control" v-model="usuario.apeMaterno">
                            </div>
                            <div class="col-md-6 pt-2">
                                <label for="correo" class="form-label">Correo</label>
                                <input type="email" class="form-control" v-model="usuario.correo" required>
                            </div>
                            <div class="col-md-6 pt-2">
                                <label for="userPassword" class="form-label">Contraseña</label>
                                <input type="password" class="form-control" v-model="usuario.userPassword"
                                    :required="!isEdit">
                                <small v-if="isEdit" class="text-muted">Déjalo en blanco para no cambiar la
                                    contraseña.</small>
                            </div>
                            <div class="col-md-6 pt-2">
                                <div class="form-check">
                                    <input class="form-check-input" type="checkbox" v-model="usuario.isAdmin"
                                        id="isAdmin">
                                    <label class="form-check-label" for="isAdmin">Administrador</label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer px-0">
                            <button type="button" class="btn btn-danger" @click="$emit('close-modal')">Cancelar</button>
                            <button type="submit" class="btn btn-primary">Guardar</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import { UsuariosRequestModel } from '@/core/models/Nido-Virtual/Usuarios/UsuariosRequest.model';

const props = defineProps<{
    usuario: UsuariosRequestModel;
    isEdit: boolean;
}>();

const emit = defineEmits(['save', 'close-modal']);

const usuario = ref<UsuariosRequestModel>(new UsuariosRequestModel());

watch(() => props.usuario, (newUsuario) => {
    usuario.value = { ...newUsuario };
}, { immediate: true });

const handleSubmit = () => {
    emit('save', { ...usuario.value });
};
</script>

<style></style>