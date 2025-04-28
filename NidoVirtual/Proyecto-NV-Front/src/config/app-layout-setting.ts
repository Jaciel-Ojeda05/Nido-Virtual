import useThemeStore from '@/stores/theme-store'
import { $themeConfig } from './theme-config'

export default {
  init() {
    const store = useThemeStore()

    const version: number = parseInt(localStorage.getItem('version') + "")
    if (isNaN(version) || version < $themeConfig.version) {
      store.toggleMenuStyle($themeConfig.theme)
      store.toggleMenuStyle($themeConfig.navigation)
      store.toggleLayoutStyle($themeConfig.layout)
      store.toggleVersion($themeConfig.version)
    }

    let val = localStorage.getItem('dark_mode')
    if (!val) {
      val = $themeConfig.theme
    }
    store.toggleDarkMode(val)

    val = localStorage.getItem('menu_style')
    if (!val) {
      val = $themeConfig.navigation
    }
    store.toggleMenuStyle(val)

    val = localStorage.getItem('layout_style')
    if (!val) {
      val = $themeConfig.layout
    }
    store.toggleLayoutStyle(val)
  },

  toggleMode(mode: string | null) {
    const store = useThemeStore()
    if (!mode) {
      const val = localStorage.getItem('dark_mode')
      mode = val
      if (!val) {
        mode = 'light'
      }
    }
    store.toggleDarkMode(mode || 'light')
    return mode
  }
}