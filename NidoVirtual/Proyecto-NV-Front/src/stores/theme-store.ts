
import { defineStore } from 'pinia'
import { ref } from 'vue'

import { useBodyStore } from '@/stores/body-store'


interface ThemeState {
  layout: string

  is_show_sidebar: boolean
  is_show_search: boolean
  is_dark_mode: boolean

  is_visible_icon_color: boolean
  is_visible_icon_message: boolean
  is_visible_icon_notification: boolean
  is_visible_subheader : boolean

  is_visible_modulo: boolean

  dark_mode: string
  menu_style: string
  layout_style: string
  version: string
  countryList: any[]
}


export const useThemeStore:any = defineStore('themeStore', () => {
  const bodyStore:any = useBodyStore()

  const state = ref<ThemeState>({
    layout: 'app',

    is_show_sidebar: true,
    is_show_search: false,
    is_dark_mode: false,

    is_visible_icon_color: false,
    is_visible_icon_message: true,
    is_visible_icon_notification:true,
    is_visible_subheader : true,

    is_visible_modulo : true,
    

    dark_mode: 'light',
      menu_style: 'vertical',
    layout_style: 'full',
    version : '20240914',

    countryList: [{ code: 'es', name: 'Spanish' }]
  })
  const setLayout = (value: string) => {
    state.value.layout = value
  }

  const getLayout = () => {
    return state.value.layout
  }
  const getLayoutStyle = () => {
    return state.value.layout_style
  }
  const getMenuStyle = () => {
    return state.value.menu_style
  }

  const isShowSidebar = () => {
    return state.value.is_show_sidebar
  }

  const toggleSideBar = (value: any) => {
    state.value.is_show_sidebar = value
  }

  const toggleSearch = (value: any) => {
    state.value.is_show_search = value
  }

  const toggleDarkMode = (value: any) => {
    value = value || 'light' 
    localStorage.setItem('dark_mode', value)
    state.value.dark_mode = value
    if (value == 'light') {
      state.value.is_dark_mode = false
    } else if (value == 'dark') {
      state.value.is_dark_mode = true
    } else if (value == 'system') {
      if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
        state.value.is_dark_mode = true
      } else {
        state.value.is_dark_mode = false
      }
    }

    if (state.value.is_dark_mode) {
      bodyStore.addBodyClassname('dark')
    } else {
      bodyStore.removeBodyClassName('dark')
    }
  }

  const toggleMenuStyle = (value: string) => {
    
    value = value || ''
    localStorage.setItem('menu_style', value)
    state.value.menu_style = value
    if (!value || value === 'vertical') {
      state.value.is_show_sidebar = true
    } else if (value === 'collapsible-vertical') {
      state.value.is_show_sidebar = false
    } else if (value === 'sin-menu') {
      state.value.is_show_sidebar = false
    }
  }

  const toggleLayoutStyle = (value: string) => {
   
    value = value || ''
    localStorage.setItem('layout_style', value)
    state.value.layout_style = value
  }

  const toggleVersion = (value: number) => {
    value = ( isNaN(value) ? 0 :  value)
    localStorage.setItem('version', value + "")
    state.value.version = value + ""
  }

 

  return {
    state,
    useThemeStore,
    toggleSideBar,
    setLayout,
    getLayout,
    toggleSearch,
    toggleDarkMode,
    toggleMenuStyle,
    toggleLayoutStyle,
    toggleVersion,

    getLayoutStyle,
    getMenuStyle,
    isShowSidebar
  }
})

export default useThemeStore
