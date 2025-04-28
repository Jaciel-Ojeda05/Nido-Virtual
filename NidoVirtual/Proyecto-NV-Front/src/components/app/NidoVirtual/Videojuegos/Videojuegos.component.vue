<template>
    <HelperContenido titulo="Gestión de Videojuegos">
        <div class="row mb-3">
            <div class="col-6 d-flex align-items-center">
                <button class="btn btn-primary" @click="agregarVideojuego()">
                    <i class="bi bi-plus-lg me-1"></i> Agregar Videojuego
                </button>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <div class="input-group">
                    <input type="text" class="form-control" placeholder="Buscar videojuego" v-model="searchTerm">
                    <button class="btn btn-outline-secondary" type="button">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div v-if="filteredVideojuegos.length === 0 && !loading">
                    <div class="alert alert-warning" role="alert">
                        No se encontraron videojuegos.
                    </div>
                </div>
                <table v-if="filteredVideojuegos.length > 0" class="table table-striped table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 5%;">#</th>
                            <th style="width: 20%;">Título</th>
                            <th style="width: 30%;">Descripción</th>
                            <th style="width: 10%;">Precio</th>
                            <th style="width: 10%;">Stock</th>
                            <th style="width: 10%;">Género</th>
                            <th style="width: 10%;">Plataforma</th>
                            <th style="width: 5%;"></th>
                            <th style="width: 5%;"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(videojuego, index) in filteredVideojuegos" :key="videojuego.idVideojuegos">
                            <td>{{ index + 1 }}</td>
                            <td>{{ videojuego.titulo }}</td>
                            <td>{{ videojuego.descVideojuego }}</td>
                            <td>${{ videojuego.precio.toFixed(2) }}</td>
                            <td>{{ videojuego.stock }}</td>
                            <td>{{ videojuego.descGenero }}</td>
                            <td>{{ videojuego.descPlataforma }}</td>
                            <td>
                                <button class="btn btn-primary btn-sm" @click="modificarVideojuego(videojuego)"
                                    title="Modificar">
                                    <i class="fa-solid fa-pencil"></i>
                                </button>
                            </td>
                            <td>
                                <button class="btn btn-danger btn-sm"
                                    @click="eliminarVideojuego(videojuego.idVideojuegos)" title="Eliminar">
                                    <i class="fa-solid fa-trash"></i>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div v-if="loading" class="alert alert-info" role="alert">
                    Cargando videojuegos...
                </div>
                <Pagination v-if="filteredVideojuegos.length > 0" class="mt-3" :totalItems="totalItems"
                    :itemsPerPageOptions="[5, 10, 15]" :currentPage="currentPage" :itemsPerPage="itemsPerPage"
                    @update:currentPage="handlePageChange" @update:itemsPerPage="handleItemsPerPageChange" />
            </div>
        </div>
        <AvisoDesicioModal v-if="showModalAvisoDesicion" :title="modalTitle" :message="modalMessage"
            @confirm="confirmarEliminarVideojuego" @close-modal="closeModalAvisoDesicion" />
        <VideojuegoModal v-if="showModalVideojuego" :isEdit="isEditing" :videojuego="selectedVideojuego"
            @save="guardarVideojuego" @close-modal="closeModalVideojuego" />
    </HelperContenido>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue';
import HelperContenido from '@/components/common/helperContenido/HelperContenido.vue';
import AvisoDesicioModal from '@/components/common/modals/Decisión/AvisoDesicion.modal.vue';
import VideojuegoModal from '@/components/common/modals/NidoVirtual/Videojuegos/Videojuegos.modal.vue';
import Pagination from '@/components/common/pagination/pagination.component.vue';
import { AddVideojuegosResponse } from '@/core/models/Nido-Virtual/Videojuegos/AddVideojuegosResponse.model';
import { VideojuegosRequestModel } from '@/core/models/Nido-Virtual/Videojuegos/VideojuegosRequest.model';
import { ResponseModel } from '@/core/models/Response/Response.model';
import { getAllVideojuegos, postVideojuego, putVideojuego, deleteVideojuego } from '@/core/services/Nido-Vitual/Videojuegos/Videojuegos.service';
import { toast } from 'vue3-toastify';
import { VideojuegosResponseModel } from '@/core/models/Nido-Virtual/Videojuegos/VideojuegosResponse.model';

const videojuegos = ref<VideojuegosResponseModel[]>([]);
const loading = ref(false);
const searchTerm = ref('');
const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalItems = ref(0);
const filteredVideojuegos = computed(() => {
    const term = searchTerm.value.toLowerCase();
    const start = (currentPage.value - 1) * itemsPerPage.value;
    const end = start + itemsPerPage.value;
    const results = videojuegos.value.filter(v =>
        v.titulo.toLowerCase().includes(term) ||
        v.descVideojuego.toLowerCase().includes(term) ||
        v.descGenero.toLowerCase().includes(term) ||
        v.descPlataforma.toLowerCase().includes(term) ||
        v.precio.toString().includes(term) ||
        v.stock.toString().includes(term)
    );
    totalItems.value = results.length;
    return results.slice(start, end);
});

const showModalAvisoDesicion = ref(false);
const modalTitle = ref('');
const modalMessage = ref('');
const videojuegoAEliminarId = ref(0);

const showModalVideojuego = ref(false);
const isEditing = ref(false);
const selectedVideojuego = ref(new AddVideojuegosResponse());

onMounted(() => {
    cargarVideojuegos();
});

function cargarVideojuegos() {
    loading.value = true;
    getAllVideojuegos(
        (response: ResponseModel<VideojuegosResponseModel[]>) => {
            loading.value = false;
            if (response.data) {
                videojuegos.value = response.data;
            } else {
                toast.error('No se pudieron cargar los videojuegos.');
            }
        },
        (error) => {
            loading.value = false;
            toast.error(`Error al cargar los videojuegos: ${error.message}`);
            console.error('Error al cargar videojuegos:', error);
        }
    );
}

function agregarVideojuego() {
    isEditing.value = false;
    selectedVideojuego.value = new AddVideojuegosResponse();
    showModalVideojuego.value = true;
}

function modificarVideojuego(videojuego: VideojuegosResponseModel) {
    isEditing.value = true;
    selectedVideojuego.value = {
        idVideojuegos: videojuego.idVideojuegos,
        titulo: videojuego.titulo,
        descVideojuego: videojuego.descVideojuego,
        fechaLanzamiento: videojuego.fechaLanzamiento,
        precio: videojuego.precio,
        stock: videojuego.stock,
        idGenero: 0,
        idPlataforma: 0,
        imgVideojuego: videojuego.imgVideojuego,
    };
    selectedVideojuego.value.idGenero = 0;
    selectedVideojuego.value.idPlataforma = 0;
    showModalVideojuego.value = true;
}

function guardarVideojuego(data: AddVideojuegosResponse & { imageFile: File | null }) {
    if (isEditing.value) {
        actualizarVideojuego(data);
    } else {
        crearVideojuego(data, data.imageFile);
    }
}

function crearVideojuego(videojuegoData: AddVideojuegosResponse, imageFile: File | null) {
    const nuevoVideojuego: VideojuegosRequestModel = {
        titulo: videojuegoData.titulo,
        descVideojuego: videojuegoData.descVideojuego,
        fechaLanzamiento: videojuegoData.fechaLanzamiento,
        precio: videojuegoData.precio,
        stock: videojuegoData.stock,
        idGenero: videojuegoData.idGenero,
        idPlataforma: videojuegoData.idPlataforma,
        imgVideojuego: '',
    };
    postVideojuego(nuevoVideojuego, imageFile,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarVideojuegos();
                toast.success('Videojuego creado exitosamente.');
                closeModalVideojuego();
            } else {
                toast.error(response.mensaje || 'Error al crear el videojuego.');
            }
        },
        (error) => {
            toast.error(`Error al crear el videojuego: ${error.message}`);
            console.error('Error al crear videojuego:', error);
        }
    );
}

function actualizarVideojuego(videojuego: AddVideojuegosResponse) {
    putVideojuego(videojuego,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarVideojuegos();
                toast.success('Videojuego actualizado exitosamente.');
                closeModalVideojuego();
            } else {
                toast.error(response.mensaje || 'Error al actualizar el videojuego.');
            }
        },
        (error) => {
            toast.error(`Error al actualizar el videojuego: ${error.message}`);
            console.error('Error al actualizar videojuego:', error);
        }
    );
}

function eliminarVideojuego(id: number) {
    videojuegoAEliminarId.value = id;
    modalTitle.value = 'Confirmar Eliminación';
    modalMessage.value = '¿Estás seguro de que deseas eliminar este videojuego?';
    showModalAvisoDesicion.value = true;
}

function confirmarEliminarVideojuego() {
    deleteVideojuego(videojuegoAEliminarId.value,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarVideojuegos();
                toast.success('Videojuego eliminado exitosamente.');
                closeModalAvisoDesicion();
            } else {
                toast.error(response.mensaje || 'Error al eliminar el videojuego.');
            }
        },
        (error) => {
            toast.error(`Error al eliminar el videojuego: ${error.message}`);
            console.error('Error al eliminar videojuego:', error);
        }
    );
}

function closeModalAvisoDesicion() {
    showModalAvisoDesicion.value = false;
}

function closeModalVideojuego() {
    showModalVideojuego.value = false;
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