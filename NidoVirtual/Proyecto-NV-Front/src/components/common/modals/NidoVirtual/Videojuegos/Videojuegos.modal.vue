<template>
    <div class="modal fade show d-block" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{ isEdit ? 'Modificar Videojuego' : 'Nuevo Videojuego' }}</h5>
                    <button type="button" class="btn-close" @click="$emit('close-modal')" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="handleSubmit" enctype="multipart/form-data">
                        <div class="row pb-3 pt-3">
                            <div class="col-md-6">
                                <label for="titulo" class="form-label">Título</label>
                                <input type="text" class="form-control" v-model="videojuego.titulo" required>
                            </div>
                            <div class="col-md-6">
                                <label for="fechaLanzamiento" class="form-label">Fecha de Lanzamiento</label>
                                <input type="date" class="form-control" v-model="videojuego.fechaLanzamiento" required>
                            </div>
                            <div class="col-12 pt-2">
                                <label for="descVideojuego" class="form-label">Descripción</label>
                                <textarea class="form-control" v-model="videojuego.descVideojuego" rows="3"
                                    required></textarea>
                            </div>
                            <div class="col-md-4 pt-2">
                                <label for="precio" class="form-label">Precio</label>
                                <input type="number" class="form-control" v-model="videojuego.precio" required>
                            </div>
                            <div class="col-md-4 pt-2">
                                <label for="stock" class="form-label">Stock</label>
                                <input type="number" class="form-control" v-model="videojuego.stock" required>
                            </div>
                            <div class="col-md-4 pt-2">
                                <label for="idGenero" class="form-label">Género</label>
                                <select class="form-select" v-model="videojuego.idGenero" required>
                                    <option :value="0" disabled>Seleccionar Género</option>
                                    <option v-for="genero in generos" :key="genero.idGenero" :value="genero.idGenero">
                                        {{ genero.descGenero }}
                                    </option>
                                </select>
                            </div>
                            <div class="col-md-6 pt-2">
                                <label for="idPlataforma" class="form-label">Plataforma</label>
                                <select class="form-select" v-model="videojuego.idPlataforma" required>
                                    <option :value="0" disabled>Seleccionar Plataforma</option>
                                    <option v-for="plataforma in plataformas" :key="plataforma.idPlataforma"
                                        :value="plataforma.idPlataforma">
                                        {{ plataforma.descPlataforma }}
                                    </option>
                                </select>
                            </div>
                            <div class="col-md-6 pt-2">
                                <label for="imgVideojuego" class="form-label">Imagen</label>
                                <input type="file" class="form-control" @change="handleImageUpload" accept="image/*"
                                    :required="!isEdit || !videojuego.imgVideojuego">
                                <img v-if="videojuego.imgVideojuego && isEdit" :src="videojuego.imgVideojuego"
                                    alt="Imagen del videojuego" class="img-thumbnail mt-2" style="max-width: 100px;">
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
import { ref, watch, onMounted } from 'vue';
import { AddVideojuegosResponse } from '@/core/models/Nido-Virtual/Videojuegos/AddVideojuegosResponse.model';
import { GenerosModel } from '@/core/models/Nido-Virtual/Generos/Generos.model';
import { PlataformasResponseModel } from '@/core/models/Nido-Virtual/Plataformas/PlataformasResponse.model';
import { getAllGenero } from '@/core/services/Nido-Vitual/Generos/Generos.service';
import { getAllPlataformas } from '@/core/services/Nido-Vitual/Plataformas/Plataformas.service';
import { ResponseModel } from '@/core/models/Response/Response.model';

const props = defineProps<{
    videojuego: AddVideojuegosResponse;
    isEdit: boolean;
}>();

const emit = defineEmits(['save', 'close-modal']);

const videojuego = ref<AddVideojuegosResponse>(new AddVideojuegosResponse());
const generos = ref<GenerosModel[]>([]);
const plataformas = ref<PlataformasResponseModel[]>([]);
const imageFile = ref<File | null>(null);

onMounted(() => {
    cargarGeneros();
    cargarPlataformas();
});

function cargarGeneros() {
    getAllGenero(
        (response: ResponseModel<GenerosModel[]>) => {
            if (response.data) {
                generos.value = response.data;
            } else {
                console.error('Error al cargar géneros en el modal.');
            }
        },
        (error) => {
            console.error('Error al cargar géneros:', error);
        }
    );
}

function cargarPlataformas() {
    getAllPlataformas(
        (response: ResponseModel<PlataformasResponseModel[]>) => {
            if (response.data) {
                plataformas.value = response.data;
            } else {
                console.error('Error al cargar plataformas en el modal.');
            }
        },
        (error) => {
            console.error('Error al cargar plataformas:', error);
        }
    );
}

watch(() => props.videojuego, (newVideojuego) => {
    videojuego.value = { ...newVideojuego };
}, { immediate: true });

const handleImageUpload = (event: Event) => {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
        imageFile.value = target.files[0];
    } else {
        imageFile.value = null;
    }
};

const handleSubmit = () => {
    emit('save', { ...videojuego.value, imageFile: imageFile.value });
};
</script>

<style scoped></style>