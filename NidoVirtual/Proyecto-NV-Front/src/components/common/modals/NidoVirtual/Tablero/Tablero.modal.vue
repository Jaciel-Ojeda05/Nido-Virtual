<template>
    <div class="modal-overlay" @click.self="$emit('cerrar-detalle')">
        <div class="modal-content">
            <HelperContenido :titulo="videojuego ? videojuego.titulo : 'Detalles del Videojuego'">
                <div v-if="videojuego" class="container mt-4">
                    <div class="row">
                        <div class="col-md-4">
                            <img :src="obtenerImagenVideojuego(videojuego.imgVideojuego)" class="img-fluid rounded"
                                alt="Imagen de {{ videojuego.titulo }}" :onerror="setDefaultImage" />
                        </div>
                        <div class="col-md-8">
                            <h3>{{ videojuego.titulo }}</h3>
                            <p class="lead">{{ videojuego.descVideojuego }}</p>
                            <ul class="list-unstyled">
                                <li><strong>Fecha de Lanzamiento:</strong> {{ videojuego.fechaLanzamiento }}</li>
                                <li><strong>Precio:</strong> ${{ videojuego.precio.toFixed(2) }}</li>
                                <li><strong>Stock:</strong> {{ videojuego.stock }} unidades</li>
                                <li><strong>Género:</strong> {{ videojuego.descGenero }}</li>
                                <li><strong>Plataforma:</strong> {{ videojuego.descPlataforma }}</li>
                            </ul>
                            <button class="btn btn-secondary" @click="$emit('cerrar-detalle')">Cerrar</button>
                        </div>
                    </div>
                </div>
                <div v-else class="container mt-4">
                    <p>No se ha seleccionado ningún videojuego para ver los detalles.</p>
                    <button class="btn btn-secondary" @click="$emit('cerrar-detalle')">Cerrar</button>
                </div>
            </HelperContenido>
        </div>
    </div>
</template>

<script setup lang="ts">
import HelperContenido from '@/components/common/helperContenido/HelperContenido.vue';
import { defineProps, defineEmits } from 'vue';
import { VideojuegosResponseModel } from '@/core/models/Nido-Virtual/Videojuegos/VideojuegosResponse.model';
import defaultGameImage from '@/assets/images/imgDefault.png';

const props = defineProps<{
    videojuego: VideojuegosResponseModel | null;
}>();

const emit = defineEmits(['cerrar-detalle']);

function obtenerImagenVideojuego(nombreArchivo: string | null | undefined): string {
    if (nombreArchivo) {
        return `${import.meta.env.VITE_API_DEV}/imagenes/${nombreArchivo}`;
    }
    return defaultGameImage;
}

function setDefaultImage(event: Event) {
    const img = event.target as HTMLImageElement;
    img.src = defaultGameImage;
}
</script>

<style scoped>
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
}

.modal-content {
    background-color: white;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    width: 80%;
    max-width: 500px;
    height: 60%;
    overflow-y: auto;
}

.container {
    padding: 20px;
}

.img-fluid {
    max-width: 100%;
    height: auto;
}

.mt-4 {
    margin-top: 1.5rem;
}

.list-unstyled li {
    margin-bottom: 0.5rem;
}
</style>