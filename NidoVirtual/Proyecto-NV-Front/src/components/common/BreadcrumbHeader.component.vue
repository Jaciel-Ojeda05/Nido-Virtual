<template>
  <div class="row mt-2 ms-2 me-2 mb-2">
    <div class="col-md-12 ">
      <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item">
            <IconHome />
          </li>
          <template v-for="(item, i) in breadcrumbs" :key="i">
            <template v-for="(path, i) in item" :key="i">
              <li class="breadcrumb-item" v-if="(path as IRoute)?.to">
                <router-link :to="{ name: (path as IRoute).to }">
                  {{ (path as IRoute).label }}
                </router-link>
              </li>
              <li class="breadcrumb-item" v-else>
                {{ (path as IRoute).label }}
              </li>
            </template>
          </template>
        </ol>
      </nav>
    </div>
  </div>
</template>

<script setup lang="ts">
import IconHome from '@/assets/svg/IconHome.vue'
import { computed } from 'vue'
import { useRoute } from 'vue-router'

interface IRoute {
  label: string
  to?: string
}

const route = useRoute()
const breadcrumbs = computed(() => {
  const matchedRoutes = route.matched
  return matchedRoutes.map((routeItem) => routeItem.meta.breadcrumb)
})
</script>
