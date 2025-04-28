<template>
    <HelperContenido titulo="Gestión de Géneros">
        <div class="row mb-3">
            <div class="col-6 d-flex align-items-center">
                <button class="btn btn-primary" @click="agregarGenero()">
                    <i class="bi bi-plus-lg me-1"></i> Agregar Género
                </button>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <div class="input-group">
                    <input type="text" class="form-control" placeholder="Buscar género" v-model="searchTerm">
                    <button class="btn btn-outline-secondary" type="button">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div v-if="filteredGeneros.length === 0 && !loading">
                    <div class="alert alert-warning" role="alert">
                        No se encontraron géneros.
                    </div>
                </div>
                <table v-if="filteredGeneros.length > 0" class="table table-striped table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 5%;">#</th>
                            <th style="width: 85%;">Descripción</th>
                            <th style="width: 5%;"></th>
                            <th style="width: 5%;"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(genero, index) in filteredGeneros" :key="genero.idGenero">
                            <td>{{ index + 1 }}</td>
                            <td>{{ genero.descGenero }}</td>
                            <td>
                                <button class="btn btn-primary btn-sm" @click="modificarGenero(genero)"
                                    title="Modificar">
                                    <i class="fa-solid fa-pencil"></i>
                                </button>
                            </td>
                            <td>
                                <button class="btn btn-danger btn-sm" @click="eliminarGenero(genero.idGenero)"
                                    title="Eliminar">
                                    <i class="fa-solid fa-trash"></i>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div v-if="loading" class="alert alert-info" role="alert">
                    Cargando géneros...
                </div>
                <Pagination v-if="filteredGeneros.length > 0" class="mt-3" :totalItems="totalItems"
                    :itemsPerPageOptions="[5, 10, 15]" :currentPage="currentPage" :itemsPerPage="itemsPerPage"
                    @update:currentPage="handlePageChange" @update:itemsPerPage="handleItemsPerPageChange" />
            </div>
        </div>
        <AvisoDesicioModal v-if="showModalAvisoDesicion" :title="modalTitle" :message="modalMessage"
            @confirm="confirmarEliminarGenero" @close-modal="closeModalAvisoDesicion" />
        <GeneroModal v-if="showModalGenero" :isEdit="isEditing" :genero="selectedGenero" @save="guardarGenero"
            @close-modal="closeModalGenero" />
    </HelperContenido>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue';
import HelperContenido from '@/components/common/helperContenido/HelperContenido.vue';
import AvisoDesicioModal from '@/components/common/modals/Decisión/AvisoDesicion.modal.vue';
import GeneroModal from '@/components/common/modals/NidoVirtual/Generos/Generos.modal.vue';
import Pagination from '@/components/common/pagination/pagination.component.vue';
import { GenerosModel } from '@/core/models/Nido-Virtual/Generos/Generos.model';
import { ResponseModel } from '@/core/models/Response/Response.model';
import { getAllGenero, postGenero, putGenero, deleteGenero } from '@/core/services/Nido-Vitual/Generos/Generos.service';
import { toast } from 'vue3-toastify';

const generos = ref<GenerosModel[]>([]);
const loading = ref(false);
const searchTerm = ref('');
const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalItems = ref(0);
const filteredGeneros = computed(() => {
    const term = searchTerm.value.toLowerCase();
    const start = (currentPage.value - 1) * itemsPerPage.value;
    const end = start + itemsPerPage.value;
    const results = generos.value.filter(g => g.descGenero.toLowerCase().includes(term));
    totalItems.value = results.length;
    return results.slice(start, end);
});

const showModalAvisoDesicion = ref(false);
const modalTitle = ref('');
const modalMessage = ref('');
const generoAEliminarId = ref(0);

const showModalGenero = ref(false);
const isEditing = ref(false);
const selectedGenero = ref<GenerosModel>(new GenerosModel());

onMounted(() => {
    cargarGeneros();
});

function cargarGeneros() {
    loading.value = true;
    getAllGenero(
        (response: ResponseModel<GenerosModel[]>) => {
            loading.value = false;
            if (response.data) {
                generos.value = response.data;
            } else {
                toast.error('No se pudieron cargar los géneros.');
            }
        },
        (error) => {
            loading.value = false;
            toast.error(`Error al cargar los géneros: ${error.message}`);
            console.error('Error al cargar géneros:', error);
        }
    );
}

function agregarGenero() {
    isEditing.value = false;
    selectedGenero.value = new GenerosModel();
    showModalGenero.value = true;
}

function modificarGenero(genero: GenerosModel) {
    isEditing.value = true;
    selectedGenero.value = { ...genero };
    showModalGenero.value = true;
}

function guardarGenero(generoData: { idGenero?: number; descGenero: string }) {
    if (isEditing.value && generoData.idGenero) {
        actualizarGenero({ idGenero: generoData.idGenero, descGenero: generoData.descGenero });
    } else {
        crearGenero({ descGenero: generoData.descGenero });
    }
}

function crearGenero(generoData: { descGenero: string }) {
    postGenero(
        generoData.descGenero,
        (response: ResponseModel<string>) => {
            if (response.codigo === 500) {
                toast.error(response.mensaje || 'Error al crear el género.');
            } else {
                closeModalGenero();
                toast.success(response.mensaje || 'Género creado exitosamente.');
                cargarGeneros();
            }
        },
        (error) => {
            toast.error(`Error al crear el género: ${error.message}`);
            console.error('Error al crear género:', error);
        }
    );
}

function actualizarGenero(genero: GenerosModel) {
    putGenero(genero,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                toast.success(response.mensaje || 'Género actualizado exitosamente.');
                closeModalGenero();
                cargarGeneros();
            } else {
                toast.error(response.mensaje || 'Error al actualizar el género.');
            }
        },
        (error) => {
            toast.error(`Error al actualizar el género: ${error.message}`);
            console.error('Error al actualizar género:', error);
        }
    );
}

function eliminarGenero(id: number) {
    generoAEliminarId.value = id;
    modalTitle.value = 'Confirmar Eliminación';
    modalMessage.value = '¿Estás seguro de que deseas eliminar este género?';
    showModalAvisoDesicion.value = true;
}

function confirmarEliminarGenero() {
    deleteGenero(
        generoAEliminarId.value,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                toast.success(response.mensaje || 'Género eliminado exitosamente.');
                closeModalAvisoDesicion();
                cargarGeneros();
            } else {
                toast.error(response.mensaje || 'Error al eliminar el género.');
            }
        },
        (error) => {
            toast.error(`Error al eliminar el género: ${error.message}`);
            console.error('Error al eliminar género:', error);
        }
    );
}

function closeModalAvisoDesicion() {
    showModalAvisoDesicion.value = false;
    generoAEliminarId.value = 0;
}

function closeModalGenero() {
    showModalGenero.value = false;
    selectedGenero.value = new GenerosModel();
    isEditing.value = false;
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
