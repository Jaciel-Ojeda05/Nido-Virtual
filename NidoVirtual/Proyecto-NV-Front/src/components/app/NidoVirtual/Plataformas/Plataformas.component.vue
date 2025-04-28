<template>
    <HelperContenido titulo="Gestión de Plataformas">
        <div class="row mb-3">
            <div class="col-6 d-flex align-items-center">
                <button class="btn btn-primary" @click="agregarPlataforma()">
                    <i class="bi bi-plus-lg me-1"></i> Agregar Plataforma
                </button>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <div class="input-group">
                    <input type="text" class="form-control" placeholder="Buscar plataforma" v-model="searchTerm">
                    <button class="btn btn-outline-secondary" type="button">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div v-if="filteredPlataformas.length === 0 && !loading">
                    <div class="alert alert-warning" role="alert">
                        No se encontraron plataformas.
                    </div>
                </div>
                <table v-if="filteredPlataformas.length > 0" class="table table-striped table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 10%;">#</th>
                            <th style="width: 80%;">Descripción</th>
                            <th style="width: 5%;"></th>
                            <th style="width: 5%;"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(plataforma, index) in filteredPlataformas" :key="plataforma.idPlataforma">
                            <td>{{ index + 1 }}</td>
                            <td>{{ plataforma.descPlataforma }}</td>
                            <td>
                                <button class="btn btn-primary btn-sm" @click="modificarPlataforma(plataforma)"
                                    title="Modificar">
                                    <i class="fa-solid fa-pencil"></i>
                                </button>
                            </td>
                            <td>
                                <button class="btn btn-danger btn-sm"
                                    @click="eliminarPlataforma(plataforma.idPlataforma)" title="Eliminar">
                                    <i class="fa-solid fa-trash"></i>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div v-if="loading" class="alert alert-info" role="alert">
                    Cargando plataformas...
                </div>
                <Pagination v-if="filteredPlataformas.length > 0" class="mt-3" :totalItems="totalItems"
                    :itemsPerPageOptions="[5, 10, 15]" :currentPage="currentPage" :itemsPerPage="itemsPerPage"
                    @update:currentPage="handlePageChange" @update:itemsPerPage="handleItemsPerPageChange" />
            </div>
        </div>
        <AvisoDesicioModal v-if="showModalAvisoDesicion" :title="modalTitle" :message="modalMessage"
            @confirm="confirmarEliminarPlataforma" @close-modal="closeModalAvisoDesicion" />
        <PlataformaModal v-if="showModalPlataforma" :isEdit="isEditing" :plataforma="selectedPlataforma"
            @save="guardarPlataforma" @close-modal="closeModalPlataforma" />
    </HelperContenido>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue';
import HelperContenido from '@/components/common/helperContenido/HelperContenido.vue';
import AvisoDesicioModal from '@/components/common/modals/Decisión/AvisoDesicion.modal.vue';
import PlataformaModal from '@/components/common/modals/NidoVirtual/Plataformas/Plataformas.modal.vue';
import Pagination from '@/components/common/pagination/pagination.component.vue';
import { PlataformasResponseModel } from '@/core/models/Nido-Virtual/Plataformas/PlataformasResponse.model';
import { PlataformasRequestModel } from '@/core/models/Nido-Virtual/Plataformas/PlataformasRequest.model';
import { ResponseModel } from '@/core/models/Response/Response.model';
import { getAllPlataformas, postPlataforma, putPlataforma, deletePlataforma } from '@/core/services/Nido-Vitual/Plataformas/Plataformas.service';
import { toast } from 'vue3-toastify';

const plataformas = ref<PlataformasResponseModel[]>([]);
const loading = ref(false);
const searchTerm = ref('');
const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalItems = ref(0);
const filteredPlataformas = computed(() => {
    const term = searchTerm.value.toLowerCase();
    const start = (currentPage.value - 1) * itemsPerPage.value;
    const end = start + itemsPerPage.value;
    const results = plataformas.value.filter(p => p.descPlataforma.toLowerCase().includes(term));
    totalItems.value = results.length;
    return results.slice(start, end);
});

const showModalAvisoDesicion = ref(false);
const modalTitle = ref('');
const modalMessage = ref('');
const plataformaAEliminarId = ref(0);

const showModalPlataforma = ref(false);
const isEditing = ref(false);
const selectedPlataforma = ref<PlataformasRequestModel>(new PlataformasRequestModel());

onMounted(() => {
    cargarPlataformas();
});

function cargarPlataformas() {
    loading.value = true;
    getAllPlataformas(
        (response: ResponseModel<PlataformasResponseModel[]>) => {
            loading.value = false;
            if (response.data) {
                plataformas.value = response.data;
            } else {
                toast.error('No se pudieron cargar las plataformas.');
            }
        },
        (error) => {
            loading.value = false;
            toast.error(`Error al cargar las plataformas: ${error.message}`);
            console.error('Error al cargar plataformas:', error);
        }
    );
}

function agregarPlataforma() {
    isEditing.value = false;
    selectedPlataforma.value = new PlataformasRequestModel();
    showModalPlataforma.value = true;
}

function modificarPlataforma(plataforma: PlataformasResponseModel) {
    isEditing.value = true;
    selectedPlataforma.value = { idPlataforma: plataforma.idPlataforma, descPlataforma: plataforma.descPlataforma };
    showModalPlataforma.value = true;
}

function guardarPlataforma(plataformaData: PlataformasRequestModel) {
    if (isEditing.value && plataformaData.idPlataforma) {
        actualizarPlataforma(plataformaData);
    } else {
        crearPlataforma(plataformaData.descPlataforma);
    }
}

function crearPlataforma(descPlataforma: string) {
    postPlataforma(descPlataforma,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarPlataformas();
                toast.success('Plataforma creada exitosamente.');
                closeModalPlataforma();
            } else {
                toast.error(response.mensaje || 'Error al crear la plataforma.');
            }
        },
        (error) => {
            toast.error(`Error al crear la plataforma: ${error.message}`);
            console.error('Error al crear plataforma:', error);
        }
    );
}

function actualizarPlataforma(plataforma: PlataformasRequestModel) {
    putPlataforma(plataforma,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarPlataformas();
                toast.success('Plataforma actualizada exitosamente.');
                closeModalPlataforma();
            } else {
                toast.error(response.mensaje || 'Error al actualizar la plataforma.');
            }
        },
        (error) => {
            toast.error(`Error al actualizar la plataforma: ${error.message}`);
            console.error('Error al actualizar plataforma:', error);
        }
    );
}

function eliminarPlataforma(idPlataforma: number) {
    plataformaAEliminarId.value = idPlataforma;
    modalTitle.value = 'Confirmar Eliminación';
    modalMessage.value = '¿Estás seguro de que deseas eliminar esta plataforma?';
    showModalAvisoDesicion.value = true;
}

function confirmarEliminarPlataforma() {
    deletePlataforma(plataformaAEliminarId.value,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarPlataformas();
                toast.success('Plataforma eliminada exitosamente.');
                closeModalAvisoDesicion();
            } else {
                toast.error(response.mensaje || 'Error al eliminar la plataforma.');
            }
        },
        (error) => {
            toast.error(`Error al eliminar la plataforma: ${error.message}`);
            console.error('Error al eliminar plataforma:', error);
        }
    );
}

function closeModalAvisoDesicion() {
    showModalAvisoDesicion.value = false;
}

function closeModalPlataforma() {
    showModalPlataforma.value = false;
    selectedPlataforma.value = new PlataformasRequestModel();
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