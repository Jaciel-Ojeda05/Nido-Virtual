<template>
    <HelperContenido titulo="Gestión de Reportes">
        <div class="row mb-3">
            <div class="col-6 d-flex align-items-center">
                <button class="btn btn-primary" @click="agregarReporte()">
                    <i class="bi bi-plus-lg me-1"></i> Agregar Reporte
                </button>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <div class="input-group">
                    <input type="text" class="form-control" placeholder="Buscar reporte" v-model="searchTerm">
                    <button class="btn btn-outline-secondary" type="button">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div v-if="filteredReportes.length === 0 && !loading">
                    <div class="alert alert-warning" role="alert">
                        No se encontraron reportes.
                    </div>
                </div>
                <table v-if="filteredReportes.length > 0" class="table table-striped table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 5%;">#</th>
                            <th style="width: 20%;">Usuario</th>
                            <th style="width: 25%;">Videojuego</th>
                            <th style="width: 10%;">Cantidad</th>
                            <th style="width: 15%;">Total Ventas</th>
                            <th style="width: 15%;">Fecha Venta</th>
                            <th style="width: 5%;"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(reporte, index) in filteredReportes" :key="reporte.idVenta">
                            <td>{{ index + 1 }}</td>
                            <td>{{ reporte.nombreUsuario }}</td>
                            <td>{{ reporte.videojuego }}</td>
                            <td>{{ reporte.cantidad }}</td>
                            <td>${{ reporte.totalVentas.toFixed(2) }}</td>
                            <td>{{ formatDate(reporte.fechaVenta) }}</td>
                            <td>
                                <button class="btn btn-primary btn-sm" @click="modificarReporte(reporte)"
                                    title="Modificar">
                                    <i class="fa-solid fa-pencil"></i>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div v-if="loading" class="alert alert-info" role="alert">
                    Cargando reportes...
                </div>
                <Pagination v-if="filteredReportes.length > 0" class="mt-3" :totalItems="totalItems"
                    :itemsPerPageOptions="[5, 10, 15]" :currentPage="currentPage" :itemsPerPage="itemsPerPage"
                    @update:currentPage="handlePageChange" @update:itemsPerPage="handleItemsPerPageChange" />
            </div>
        </div>
        <ReporteModal v-if="showModalReporte" :isEdit="isEditing" :reporte="selectedReporte" @save="guardarReporte"
            @close-modal="closeModalReporte" />
    </HelperContenido>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue';
import HelperContenido from '@/components/common/helperContenido/HelperContenido.vue';
import ReporteModal from '@/components/common/modals/NidoVirtual/Reportes/Reportes.modal.vue';
import Pagination from '@/components/common/pagination/pagination.component.vue';
import { ReportesResponseModel } from '@/core/models/Nido-Virtual/Reportes/ReportesResponse.model';
import { PostReportesModel } from '@/core/models/Nido-Virtual/Reportes/PostRepostesRequest.model';
import { PutReportesRquestModel } from '@/core/models/Nido-Virtual/Reportes/PutReportesRequest.model';
import { ResponseModel } from '@/core/models/Response/Response.model';
import { getAllReportes, postReporte, putReporte, deleteReporte } from '@/core/services/Nido-Vitual/Reportes/Reportes.service';
import { toast } from 'vue3-toastify';

const reportes = ref<ReportesResponseModel[]>([]);
const loading = ref(false);
const searchTerm = ref('');
const currentPage = ref(1);
const itemsPerPage = ref(10);
const totalItems = ref(0);
const filteredReportes = computed(() => {
    const term = searchTerm.value.toLowerCase();
    const start = (currentPage.value - 1) * itemsPerPage.value;
    const end = start + itemsPerPage.value;
    const results = reportes.value.filter(r =>
        r.nombreUsuario.toLowerCase().includes(term) ||
        r.videojuego.toLowerCase().includes(term) ||
        r.fechaVenta.toLowerCase().includes(term) ||
        r.cantidad.toString().includes(term) ||
        r.totalVentas.toFixed(2).includes(term)
    );
    totalItems.value = results.length;
    return results.slice(start, end);
});

const showModalReporte = ref(false);
const isEditing = ref(false);
const selectedReporte = ref<ReportesResponseModel | null>(null);

onMounted(() => {
    cargarReportes();
});

function cargarReportes() {
    loading.value = true;
    getAllReportes(
        (response: ResponseModel<ReportesResponseModel[]>) => {
            loading.value = false;
            if (response.data) {
                reportes.value = response.data;
            } else {
                toast.error('No se pudieron cargar los reportes.');
            }
        },
        (error) => {
            loading.value = false;
            toast.error(`Error al cargar los reportes: ${error.message}`);
            console.error('Error al cargar reportes:', error);
        }
    );
}

function agregarReporte() {
    isEditing.value = false;
    selectedReporte.value = new ReportesResponseModel();
    showModalReporte.value = true;
}

function modificarReporte(reporte: ReportesResponseModel) {
    isEditing.value = true;
    selectedReporte.value = reporte;
    showModalReporte.value = true;
}

function guardarReporte(reporteData: PutReportesRquestModel) {
    if (isEditing.value && selectedReporte.value?.idVenta) {
        actualizarReporte({ ...reporteData, idVenta: selectedReporte.value.idVenta });
    } else {
        crearReporte(reporteData);
    }
}

function crearReporte(reporte: PostReportesModel) {
    postReporte(reporte,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarReportes();
                toast.success('Reporte creado exitosamente.');
                closeModalReporte();
            } else {
                toast.error(response.mensaje || 'Error al crear el reporte.');
            }
        },
        (error) => {
            toast.error(`Error al crear el reporte: ${error.message}`);
            console.error('Error al crear reporte:', error);
        }
    );
}

function actualizarReporte(reporte: PutReportesRquestModel) {
    putReporte(reporte,
        (response: ResponseModel<string>) => {
            if (response.codigo === 200) {
                cargarReportes();
                toast.success('Reporte actualizado exitosamente.');
                closeModalReporte();
            } else {
                toast.error(response.mensaje || 'Error al actualizar el reporte.');
            }
        },
        (error) => {
            toast.error(`Error al actualizar el reporte: ${error.message}`);
            console.error('Error al actualizar reporte:', error);
        }
    );
}

function closeModalReporte() {
    showModalReporte.value = false;
    selectedReporte.value = null;
}

function handlePageChange(newPage: number) {
    currentPage.value = newPage;
}

function handleItemsPerPageChange(newItemsPerPage: number) {
    itemsPerPage.value = newItemsPerPage;
    currentPage.value = 1;
}

function formatDate(dateString: string): string {
    try {
        const date = new Date(dateString);
        const day = date.getDate().toString().padStart(2, '0');
        const month = (date.getMonth() + 1).toString().padStart(2, '0');
        const year = date.getFullYear();
        const hours = date.getHours().toString().padStart(2, '0');
        const minutes = date.getMinutes().toString().padStart(2, '0');
        const seconds = date.getSeconds().toString().padStart(2, '0');
        return `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
    } catch (error) {
        console.error('Error al formatear la fecha y hora:', error);
        return '';
    }
}

watch(searchTerm, () => {
    currentPage.value = 1;
});
</script>