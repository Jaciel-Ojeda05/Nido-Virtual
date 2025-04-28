import { AccesoResponse } from '@/core/models/Acceso/AccesoResponse.model';
import { defineStore } from 'pinia';

export const useUserStore = defineStore('user', {
  state: (): { userInfo: AccesoResponse | null } => ({
    userInfo: JSON.parse(sessionStorage.getItem('userInfo') || 'null'),
  }),
  actions: {
    setUser(userInfo: AccesoResponse) {
      this.userInfo = userInfo; 
      sessionStorage.setItem('userInfo', JSON.stringify(userInfo)); 
    },
    getUser(): AccesoResponse | null {
      return this.userInfo || JSON.parse(sessionStorage.getItem('userInfo') || 'null'); 
    },
    removeUser() {
      this.userInfo = null; 
      sessionStorage.removeItem('userInfo'); 
    },
  },
});
