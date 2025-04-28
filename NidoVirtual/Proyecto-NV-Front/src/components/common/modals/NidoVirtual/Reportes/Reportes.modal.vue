<template>
    <div class="modal fade show d-block" tabindex="-1">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{ isEdit ? 'Modificar Reporte' : 'Nuevo Reporte' }}</h5>
                    <button type="button" class="btn-close" @click="$emit('close-modal')" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="handleSubmit">
                        <div class="row pb-3 pt-3">
                            <div class="col-12 pt-2">
                                <label for="idUsuario" class="form-label">Usuario</label>
                                <select v-model="reporte.idUsuario" class="form-select" id="idUsuario" required>
                                    <option :value="0" disabled>Seleccionar Usuario</option>
                                    <option v-for="usuario in usuarios" :key="usuario.idCliente"
                                        :value="usuario.idCliente">
                                        {{ usuario.nombreUsuario }} {{ usuario.apePaterno }} {{ usuario.apeMaterno }}
                                    </option>
                                </select>
                            </div>
                        </div>
                        <div class="row pb-3">
                            <div class="col-12 pt-2">
                                <label for="idVideojuegos" class="form-label">Videojuego</label>
                                <select v-model="reporte.idVideojuegos" class="form-select" id="idVideojuegos" required>
                                    <option :value="0" disabled>Seleccionar Videojuego</option>
                                    <option v-for="videojuego in videojuegos" :key="videojuego.idVideojuegos"
                                        :value="videojuego.idVideojuegos">
                                        {{ videojuego.titulo }}
                                    </option>
                                </select>
                            </div>
                        </div>
                        <div class="row pb-3">
                            <div class="col-12 pt-2">
                                <label for="cantidad" class="form-label">Cantidad</label>
                                <input type="number" class="form-control" v-model="reporte.cantidad" id="cantidad"
                                    required>
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
import { ref, onMounted, watch } from 'vue';
import { PostReportesModel } from '@/core/models/Nido-Virtual/Reportes/PostRepostesRequest.model';
import { PutReportesRquestModel } from '@/core/models/Nido-Virtual/Reportes/PutReportesRequest.model';
import { UsuariosResponseModel } from '@/core/models/Nido-Virtual/Usuarios/UsuariosResponse.model';
import { VideojuegosResponseModel } from '@/core/models/Nido-Virtual/Videojuegos/VideojuegosResponse.model';
import { getAllUsuarios } from '@/core/services/Nido-Vitual/Usuarios/Usuarios.servide';
import { getAllVideojuegos } from '@/core/services/Nido-Vitual/Videojuegos/Videojuegos.service';
import { ResponseModel } from '@/core/models/Response/Response.model';
import { ReportesResponseModel } from '@/core/models/Nido-Virtual/Reportes/ReportesResponse.model';

const props = defineProps<{
    isEdit: boolean;
    reporte?: ReportesResponseModel | null;
}>();

const emit = defineEmits(['save', 'close-modal']);

const reporte = ref<PostReportesModel | PutReportesRquestModel>(new PostReportesModel());
const usuarios = ref<UsuariosResponseModel[]>([]);
const videojuegos = ref<VideojuegosResponseModel[]>([]);
const loadingUsuarios = ref(false);
const loadingVideojuegos = ref(false);

onMounted(() => {
    cargarUsuarios();
    cargarVideojuegos();
    watch(() => props.reporte, (newReporte) => {
        if (props.isEdit && newReporte) {
            reporte.value = {
                idVenta: newReporte.idVenta,
                idUsuario: newReporte.idCliente,
                idVideojuegos: newReporte.idVideojuegos,
                cantidad: newReporte.cantidad,
            } as PutReportesRquestModel;
        } else {
            reporte.value = new PostReportesModel();
        }
    }, { immediate: true });
});

function cargarUsuarios() {
    loadingUsuarios.value = true;
    getAllUsuarios(
        (response: ResponseModel<UsuariosResponseModel[]>) => {
            loadingUsuarios.value = false;
            if (response.data) {
                usuarios.value = response.data;
            } else {
                console.error('Error al cargar usuarios:', response.mensaje);
            }
        },
        (error) => {
            loadingUsuarios.value = false;
            console.error('Error al cargar usuarios:', error);
        }
    );
}

function cargarVideojuegos() {
    loadingVideojuegos.value = true;
    getAllVideojuegos(
        (response: ResponseModel<VideojuegosResponseModel[]>) => {
            loadingVideojuegos.value = false;
            if (response.data) {
                videojuegos.value = response.data;
            } else {
                console.error('Error al cargar videojuegos:', response.mensaje);
            }
        },
        (error) => {
            loadingVideojuegos.value = false;
            console.error('Error al cargar videojuegos:', error);
        }
    );
}

const handleSubmit = () => {
    emit('save', reporte.value);
};
</script>

<style scoped></style>