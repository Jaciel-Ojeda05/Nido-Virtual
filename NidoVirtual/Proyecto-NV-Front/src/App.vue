<template>
    <FullScreenSpinner />
    <RouterView />
</template>

<script setup lang="ts">
import { onMounted, nextTick } from 'vue';
import { RouterView } from 'vue-router'
import { useBodyStore } from "@/stores/body-store";
import '@/assets/sass/app.scss';
import "@/assets/sass/custom-modal.scss";
import '@/assets/sass/info.scss';

import FullScreenSpinner from './components/common/fullScreenSpinner/fullScreenSpinner.component.vue';

const bodyStore = useBodyStore();

onMounted(() => {
    nextTick(() => {
        bodyStore.removeBodyClassName("page-loading");
    });
});
</script>
