<template>
    <div class="modal fade show d-block" tabindex="-1">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{ isEdit ? 'Modificar Plataforma' : 'Nueva Plataforma' }}</h5>
                    <button type="button" class="btn-close" @click="$emit('close-modal')" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="handleSubmit">
                        <div class="row pb-3 pt-3">
                            <div class="col-12 pt-2">
                                <label for="descripcion" class="form-label">Descripción</label>
                                <input type="text" class="form-control" v-model="plataforma.descPlataforma" required>
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
import { PlataformasRequestModel } from '@/core/models/Nido-Virtual/Plataformas/PlataformasRequest.model';

const props = defineProps<{
    plataforma: PlataformasRequestModel;
    isEdit: boolean;
}>();

const emit = defineEmits(['save', 'close-modal']);

const plataforma = ref<PlataformasRequestModel>(new PlataformasRequestModel());

watch(() => props.plataforma, (newPlataforma) => {
    plataforma.value = { ...newPlataforma };
}, { immediate: true });

const handleSubmit = () => {
    emit('save', { ...plataforma.value });
};
</script>

<style scoped></style>