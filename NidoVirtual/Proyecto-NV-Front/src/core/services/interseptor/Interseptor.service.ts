import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import { useLoading } from "@/core/composables/useLoading";
import router from '@/router/index.router';
import { useUserStore } from '@/stores/Acceso.store';

let activeRequests = 0;

const { setLoading } = useLoading();

async function Interceptor(url: string, options: AxiosRequestConfig): Promise<AxiosResponse> {
    const authStore = useUserStore();
    const user = authStore.getUser();
    const headers = {
        ...options.headers,
        'Authorization': `Bearer ${user?.token}`,
        'Content-Type': options.headers?.['Content-Type'] || 'application/json', 
    };
    activeRequests++;
    setLoading(true);
    try {
        const response = await axios({
            url,
            ...options,
            headers
        });
        return response;
    } catch (error: any) {
        if (error.response?.status === 401) {
            authStore.removeUser();
            router.push('/Checador/login');
            throw new Error('Token inválido o expirado. Redirigiendo al login.');
        } else {
            console.log(error.response?.data?.message);
            throw new Error(`${error.response?.data?.message || 'Error desconocido'}`);
        }
    } finally {
        activeRequests--;
        if (activeRequests === 0) {
            setLoading(false);
        }
    }
}

export default Interceptor;