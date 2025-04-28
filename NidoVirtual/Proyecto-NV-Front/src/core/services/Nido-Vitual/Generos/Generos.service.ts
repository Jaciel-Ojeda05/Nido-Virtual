import Interseptor from '@/core/services/interseptor/Interseptor.service';
import { GenerosModel } from '@/core/models/Nido-Virtual/Generos/Generos.model';
import { ResponseModel } from "@/core/models/Response/Response.model";

const apiUrl = import.meta.env.VITE_API_DEV;
const apiGetGeneros = import.meta.env.VITE_GENEROS_GETALLGENEROS;
const apiPostGeneros = import.meta.env.VITE_GENEROS_POST;
const apiPutGeneros = import.meta.env.VITE_GENEROS_PUT;
const apiDeleteGeneros = import.meta.env.VITE_GENEROS_DELETE;

export function getAllGenero(callback: (response: ResponseModel<GenerosModel[]>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apiUrl}${apiGetGeneros}`, { method: 'GET' })
        .then(response => callback(response.data))
        .catch(error => errorCallback(error));
}

export function postGenero(descGenero: string, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apiUrl}${apiPostGeneros}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ descGenero })
    })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function putGenero(obj: GenerosModel, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apiUrl}${apiPutGeneros}`, { method: 'PUT', data: obj })
        .then(response => callback(response.data))
        .catch(error => errorCallback(error));
}

export function deleteGenero(id: number, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apiUrl}${apiDeleteGeneros}/${id}`, { method: 'DELETE' })
        .then(response => {
            callback(response.data as ResponseModel<string>);
        })
        .catch(error => {
            errorCallback(error);
        });
}