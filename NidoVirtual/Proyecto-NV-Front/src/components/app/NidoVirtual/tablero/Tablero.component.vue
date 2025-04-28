<template>
  <HelperContenido titulo="Lista de Videojuegos">
    <div class="container">
      <div class="row row-cols-1 row-cols-md-3 g-4">
        <div v-for="videojuego in videojuegos" :key="videojuego.idVideojuegos" class="col">
          <div class="card">
            <img :src="obtenerImagenVideojuego(videojuego.imgVideojuego)" class="card-img-top"
              alt="Imagen de {{ videojuego.titulo }}" style="height: 200px; object-fit: cover; cursor: pointer;"
              :onerror="setDefaultImage" @click="mostrarDetalle(videojuego)" />
            <div class="card-body">
              <h5 class="card-title">{{ videojuego.titulo }}</h5>
              <p class="card-text">Precio: ${{ videojuego.precio.toFixed(2) }}</p>
              <button class="btn btn-primary" @click="mostrarDetalle(videojuego)">Ver Detalles</button>
            </div>
          </div>
        </div>
        <div v-if="videojuegos.length === 0 && !loading" class="col">
          <p>No se encontraron videojuegos.</p>
        </div>
        <div v-if="loading" class="col">
          <p>Cargando videojuegos...</p>
        </div>
        <div v-if="error" class="col">
          <p class="text-danger">Error al cargar los videojuegos: {{ error }}</p>
        </div>
      </div>
      <DetalleVideojuego v-if="detalleVisible" :videojuego="videojuegoSeleccionado"
        @cerrar-detalle="detalleVisible = false" />
    </div>
  </HelperContenido>
</template>

<script setup lang="ts">
import HelperContenido from '@/components/common/helperContenido/HelperContenido.vue';
import DetalleVideojuego from '@/components/common/modals/NidoVirtual/Tablero/Tablero.modal.vue';
import { ref, onMounted } from 'vue';
import { getAllVideojuegos } from '@/core/services/Nido-Vitual/Videojuegos/Videojuegos.service';
import { VideojuegosResponseModel } from '@/core/models/Nido-Virtual/Videojuegos/VideojuegosResponse.model';
import { ResponseModel } from '@/core/models/Response/Response.model';
import defaultGameImage from '@/assets/images/imgDefault.png';

const videojuegos = ref<VideojuegosResponseModel[]>([]);
const loading = ref(false);
const error = ref<string | null>(null);
const detalleVisible = ref(false);
const videojuegoSeleccionado = ref<VideojuegosResponseModel | null>(null);

onMounted(() => {
  cargarVideojuegos();
});

function cargarVideojuegos() {
  loading.value = true;
  error.value = null;
  getAllVideojuegos(
    (response: ResponseModel<VideojuegosResponseModel[]>) => {
      loading.value = false;
      if (response.data) {
        videojuegos.value = response.data;
      } else {
        error.value = 'No se recibieron datos de videojuegos.';
      }
    },
    (err: Error) => {
      loading.value = false;
      error.value = err.message || 'Ocurrió un error al cargar los videojuegos.';
      console.error('Error al cargar videojuegos:', err);
    }
  );
}

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

function mostrarDetalle(videojuego: VideojuegosResponseModel) {
  videojuegoSeleccionado.value = videojuego;
  detalleVisible.value = true;
}
</script>

<style scoped>
.container {
  padding-top: 20px;
}

.card {
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  overflow: hidden;
}

.card-img-top {
  width: 100%;
  display: block;
  cursor: pointer;
  /* Indica que la imagen también es clickable */
}

.card-body {
  padding: 15px;
}

.card-title {
  font-size: 1.2em;
  margin-bottom: 5px;
}

.card-text {
  font-size: 1em;
  color: #555;
}

.btn-primary {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 8px 15px;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-primary:hover {
  background-color: #0056b3;
}
</style>