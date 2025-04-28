<template>
  <div class="d-flex justify-content-between align-items-center">
    <div>
      <label class="pe-2">Mostrar</label>
      <select v-model="localItemsPerPage" @change="resetPage">
        <option v-for="option in itemsPerPageOptions" :key="option" :value="option">
          {{ option }}
        </option>
      </select>
      <label class="ps-2">registros por página, Total registros: {{ totalItems }}</label>
    </div>
    <nav aria-label="Page navigation">
      <ul class="pagination mb-0">
        <li class="page-item" :class="{ disabled: localCurrentPage === 1 }">
          <a class="page-link" href="#" @click.prevent="goToPage(localCurrentPage - 1)">
            &lt;
          </a>
        </li>
        <li class="page-item px-1" v-for="page in visiblePages" :key="page"
          :class="{ active: page === localCurrentPage }">
          <a class="page-link" href="#" @click.prevent="goToPage(page)">
            {{ page }}
          </a>
        </li>
        <li class="page-item" :class="{ disabled: localCurrentPage === totalPages }">
          <a class="page-link" href="#" @click.prevent="goToPage(localCurrentPage + 1)">
            &gt;
          </a>
        </li>
      </ul>
    </nav>
  </div>
</template>

<script setup>
import { ref, computed, watch } from 'vue';

const props = defineProps({
  totalItems: {
    type: Number,
    required: true,
  },
  itemsPerPageOptions: {
    type: Array,
    default: () => [5, 10, 15],
  },
  currentPage: {
    type: Number,
    default: 1,
  },
  itemsPerPage: {
    type: Number,
    default: 5,
  },
});

const emit = defineEmits(['update:pagination']);

const localItemsPerPage = ref(props.itemsPerPage);
const localCurrentPage = ref(props.currentPage);

watch(() => props.currentPage, (newPage) => {
  localCurrentPage.value = newPage;
});

const totalPages = computed(() => Math.ceil(props.totalItems / localItemsPerPage.value));

const visiblePages = computed(() => {
  const pages = [];
  const startPage = Math.max(1, localCurrentPage.value - 1);
  const endPage = Math.min(startPage + 2, totalPages.value);
  for (let i = startPage; i <= endPage; i++) {
    pages.push(i);
  }
  return pages;
});

watch([localItemsPerPage, localCurrentPage], ([newItemsPerPage, newCurrentPage]) => {
  emit('update:pagination', {
    currentPage: newCurrentPage,
    itemsPerPage: newItemsPerPage,
  });
});

const goToPage = (page) => {
  if (page >= 1 && page <= totalPages.value) {
    localCurrentPage.value = page;
  }
};

const resetPage = () => {
  emit('update:pagination', {
    currentPage: newCurrentPage,
    itemsPerPage: 1,
  });
};
</script>

<style scoped>
.pagination li a {
  cursor: pointer;
  border-radius: 50%;
  width: 36px;
  height: 36px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.pagination li.active a {
  background-color: #13423D;
  color: white;
  border-color: #13423D;
}

.pagination li.disabled a {
  pointer-events: none;
  opacity: 0.6;
}
</style>
