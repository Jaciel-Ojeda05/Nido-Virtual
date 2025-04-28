import axios from 'axios';
import { AccesoResponse } from '@/core/models/Acceso/AccesoResponse.model';
const apiUrl = import.meta.env.VITE_API_DEV;
const UrlLogin = import.meta.env.VITE_LOGIN_SISTEMAS;
const UrlCambio = import.meta.env.VITE_CAMBIOCLAVE_SISTEMAS;
import { useUserStore } from '@/stores/Acceso.store';
import { useLoading } from "@/core/composables/useLoading";
import { ResponseModel } from '@/core/models/Response/Response.model';
import { AccesoRequest } from '@/core/models/Acceso/AccesoRequest.model';
import Interseptor from '../../interseptor/Interseptor.service';

const { setLoading } = useLoading();

export function login(
    loginData: { correo: string | null; userPassword: string },
    callback: (response: ResponseModel<AccesoResponse>) => void,
    errorCallback: (error: Error) => void
): void {
    setLoading(true);
    axios.post(`${apiUrl}${UrlLogin}`, loginData, {
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => {
            const data = response.data as ResponseModel<AccesoResponse>;
            const authStore = useUserStore();
            authStore.setUser(data.data!);
            setLoading(false);
            callback(data);
        })
        .catch(error => {
            setLoading(false);
            errorCallback(error);
        });
}

export function postCambioClave(obj: AccesoRequest, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apiUrl}${UrlCambio}`, { method: 'POST', data: obj })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}
