<template>
  <Teleport to="#breadcrumb" v-if="isMounted && hasBreadcrumb">
    <ul class="navbar-nav flex-row">
      <li>
        <div class="page-header">
          <nav class="breadcrumb-one" aria-label="breadcrumb">
            <ol class="breadcrumb">
              <li class="breadcrumb-item">
                <a href="/">
                  <IconHome />
                </a>
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
      </li>
    </ul>
  </Teleport>
</template>

<script setup lang="ts">
import IconHome from '@/assets/svg/IconHome.vue'
import { nextTick } from 'process'
import { onMounted, ref, computed } from 'vue'
import { useRoute } from 'vue-router'
const route = useRoute()

import { useHead } from '@unhead/vue'

const title = computed<string>(() => {
  const meta = route.meta
  return meta.pageTitle as string
})

useHead({ title: title })

interface IRoute {
  label: string
  to?: string
}

const breadcrumbs = computed(() => {
  const matchedRoutes = route.matched
  return matchedRoutes.map((routeItem) => routeItem.meta.breadcrumb)
})

const isMounted = ref(false)
const hasBreadcrumb = ref(true)

onMounted(() => {
  nextTick(() => {
    isMounted.value = true
  })
})
</script>
