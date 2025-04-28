<template>
    <HelperContenido titulo="Gestión de Usuarios">
        <div class="row mb-3">
            <div class="col-6 d-flex align-items-center">
                <button class="btn btn-primary" @click="agregarUsuario()">
                    <i class="bi bi-plus-lg me-1"></i> Agregar Usuario
                </button>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <div class="input-group">
                    <input type="text" class="form-control" placeholder="Buscar usuario" v-model="searchTerm">
                    <button class="btn btn-outline-secondary" type="button">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div v-if="filteredUsuarios.length === 0 && !loading">
                    <div class="alert alert-warning" role="alert">
                        No se encontraron usuarios.
                    </div>
                </div>
                <table v-if="filteredUsuarios.length > 0" class="table table-striped table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 5%;">#</th>
                            <th style="width: 20%;">Nombre</th>
                            <th style="width: 20%;">Apellido Paterno</th>
                            <th style="width: 20%;">Apellido Materno</th>
                            <th style="width: 25%;">Correo</th>
                            <th style="width: 5%;">Admin</th>
                            <th style="width: 5%;"></th>
                            <th style="width: 5%;"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(usuario, index) in filteredUsuarios" :key="usuario.idCliente">
                            <td>{{ index + 1 }}</td>
                            <td>{{ usuario.nombreUsuario }}</td>
                            <td>{{ usuario.apePaterno }}</td>
                            <td>{{ usuario.apeMaterno }}</td>
                            <td>{{ usuario.correo }}</td>
                            <td>
                                <i v-if="usuario.isAdmin" class="fa-solid fa-check-circle text-success"></i>
                                <i v-else class="fa-solid fa-times-circle text-danger"></i>
                            </td>
                            <td>
                                <button class="btn btn-primary btn-sm" @click="modificarUsuario(usuario)"
                                    title="Modificar">
                                    <i class="fa-solid fa-pencil"></i>
                                </button>
                            </td>
                            <td>
                                <button class="btn btn-danger btn-sm" @click="eliminarUsuario(usuario.idCliente)"
                                    title="Eliminar">
                                    <i class="fa-solid fa-trash"></i>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div v-if="loading" class="alert alert-info" role="alert">
                    Cargando usuarios...
                </div>
                <Pagination v-if="filteredUsuarios.length > 0" class="mt-3" :totalItems="totalItems"
                    :itemsPerPageOptions="[5, 10, 15]" :currentPage="currentPage" :itemsPerPage="itemsPerPage"
                    @update:currentPage="handlePageChange" @update:itemsPerPage="handleItemsPerPageChange" />
            </div>
        </div>
        <AvisoDesicioModal v-if="showModalAvisoDesicion" :title="modalTitle" :message="modalMessage"
            @confirm="confirmarEliminarUsuario" @close-modal="closeModalAvisoDesicion" />
        <UsuarioModal v-if="showModalUsuario" :isEdit="isEditing" :usuario="selectedUsuario" @save="guardarUsuario"
            @close-modal="closeModalUsuario" />
    </HelperContenido>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue';
import HelperContenido from '@/components/common/helperContenido/HelperContenido.vue';
import AvisoDesicioModal from '@/components/common/modals/Decisión/AvisoDesicion.modal.vue';
import UsuarioModal from '@/components/common/modals/NidoVirtual/Usuarios/Usuarios.modal.vue';
import Pagination from '@/components/common/pagination/pagination.component.vue';
import { UsuariosResponseModel } from '@/core/models/Nido-Virtual/Usuarios/UsuariosResponse.model';
import { UsuariosRequestModel } from '@/core/models/Nido-Virtual/Usuarios/UsuariosRequest.model';
import { ResponseModel } from '@/core/models/Response/Response.model';
import { getAllUsuarios, postUsuario, putUsuario, deleteUsuario } from '@/core/services/Nido-Vitual/Usuarios/Usuarios.servide';
import { toast } from 'vue3-toastify';

const usuarios = ref<UsuariosResponseModel[]>([]);
const loading = ref(false);
const searchTerm = ref('');
const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalItems = ref(0);
const filteredUsuarios = computed(() => {
    const term = searchTerm.value.toLowerCase();
    const start = (currentPage.value - 1) * itemsPerPage.value;
    const end = start + itemsPerPage.value;
    const results = usuarios.value.filter(u =>
        u.nombreUsuario.toLowerCase().includes(term) ||
        u.apePaterno.toLowerCase().includes(term) ||
        u.apeMaterno.toLowerCase().includes(term) ||
        u.correo.toLowerCase().includes(term)
    );
    totalItems.value = results.length;
    return results.slice(start, end);
});

const showModalAvisoDesicion = ref(false);
const modalTitle = ref('');
const modalMessage = ref('');
const usuarioAEliminarId = ref(0);

const showModalUsuario = ref(false);
const isEditing = ref(false);
const selectedUsuario = ref<UsuariosRequestModel>(new UsuariosRequestModel());

onMounted(() => {
    cargarUsuarios();
});

function cargarUsuarios() {
    loading.value = true;
    getAllUsuarios(
        (response: ResponseModel<UsuariosResponseModel[]>) => {
            loading.value = false;
            if (response.data) {
                usuarios.value = response.data;
            } else {
                toast.error('No se pudieron cargar los usuarios.');
            }
        },
        (error) => {
            loading.value = false;
            toast.error(`Error al cargar los usuarios: ${error.message}`);
            console.error('Error al cargar usuarios:', error);
        }
    );
}

function agregarUsuario() {
    isEditing.value = false;
    selectedUsuario.value = new UsuariosRequestModel();
    showModalUsuario.value = true;
}

function modificarUsuario(usuario: UsuariosResponseModel) {
    isEditing.value = true;
    selectedUsuario.value = {
        nombreUsuario: usuario.nombreUsuario,
        apePaterno: usuario.apePaterno,
        apeMaterno: usuario.apeMaterno,
        correo: usuario.correo,
        userPassword: '',
        isAdmin: usuario.isAdmin,
    };
    showModalUsuario.value = true;
}

function guardarUsuario(usuario: UsuariosRequestModel) {
    if (isEditing.value) {
        actualizarUsuario(usuario);
    } else {
        crearUsuario(usuario);
    }
}

function crearUsuario(usuario: UsuariosRequestModel) {
    postUsuario(usuario,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarUsuarios();
                toast.success('Usuario creado exitosamente.');
                closeModalUsuario();
            } else {
                toast.error(response.mensaje || 'Error al crear el usuario.');
            }
        },
        (error) => {
            toast.error(`Error al crear el usuario: ${error.message}`);
            console.error('Error al crear usuario:', error);
        }
    );
}

function actualizarUsuario(usuario: UsuariosRequestModel) {
    // Necesitas el ID del usuario para la actualización
    const usuarioParaActualizar = usuarios.value.find(u =>
        u.nombreUsuario === selectedUsuario.value.nombreUsuario &&
        u.apePaterno === selectedUsuario.value.apePaterno &&
        u.apeMaterno === selectedUsuario.value.apeMaterno &&
        u.correo === selectedUsuario.value.correo
    );

    if (usuarioParaActualizar) {
        const usuarioConId = { ...usuario, idCliente: usuarioParaActualizar.idCliente };
        putUsuario(usuarioConId,
            (response: ResponseModel<string>) => {
                if (response.codigo === 200) {
                    cargarUsuarios();
                    toast.success('Usuario actualizado exitosamente.');
                    closeModalUsuario();
                } else {
                    toast.error(response.mensaje || 'Error al actualizar el usuario.');
                }
            },
            (error) => {
                toast.error(`Error al actualizar el usuario: ${error.message}`);
                console.error('Error al actualizar usuario:', error);
            }
        );
    } else {
        toast.error('No se encontró el usuario para actualizar.');
    }
}

function eliminarUsuario(id: number) {
    usuarioAEliminarId.value = id;
    modalTitle.value = 'Confirmar Eliminación';
    modalMessage.value = '¿Estás seguro de que deseas eliminar este usuario?';
    showModalAvisoDesicion.value = true;
}

function confirmarEliminarUsuario() {
    deleteUsuario(usuarioAEliminarId.value,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarUsuarios();
                toast.success('Usuario eliminado exitosamente.');
                closeModalAvisoDesicion();
            } else {
                toast.error(response.mensaje || 'Error al eliminar el usuario.');
            }
        },
        (error) => {
            toast.error(`Error al eliminar el usuario: ${error.message}`);
            console.error('Error al eliminar usuario:', error);
        }
    );
}

function closeModalAvisoDesicion() {
    showModalAvisoDesicion.value = false;
}

function closeModalUsuario() {
    showModalUsuario.value = false;
}

function handlePageChange(newPage: number) {
    currentPage.value = newPage;
}

function handleItemsPerPageChange(newItemsPerPage: number) {
    itemsPerPage.value = newItemsPerPage;
    currentPage.value = 1;
}

watch(searchTerm, () => {
    currentPage.value = 1;
});
</script>