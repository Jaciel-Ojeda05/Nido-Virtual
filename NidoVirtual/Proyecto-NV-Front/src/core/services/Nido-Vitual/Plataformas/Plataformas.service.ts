import Interseptor from '../../interseptor/Interseptor.service';
import { PlataformasRequestModel } from '@/core/models/Nido-Virtual/Plataformas/PlataformasRequest.model';
import { PlataformasResponseModel } from '@/core/models/Nido-Virtual/Plataformas/PlataformasResponse.model';
import { ResponseModel } from "@/core/models/Response/Response.model";

const apieUrl = import.meta.env.VITE_API_DEV;
const apiGetPlataformas = import.meta.env.VITE_PLATAFORMAS_GETALLPLATAFORMAS;
const apiPostPlataformas = import.meta.env.VITE_PLATAFORMAS_POST;
const apiPutPlataformas = import.meta.env.VITE_PLATAFORMAS_PUT;
const apiDeletePlataformas = import.meta.env.VITE_PLATAFORMAS_DELETE;

export function getAllPlataformas(callback: (response: ResponseModel<PlataformasResponseModel[]>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiGetPlataformas}`, { method: 'GET' })
        .then(response => callback(response.data))
        .catch(error => errorCallback(error));
}

export function postPlataforma(descPlataforma: string, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiPostPlataformas}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ descPlataforma })
    })
        .then(response => callback(response.data))
        .catch(error => errorCallback(error));
}

export function putPlataforma(obj: PlataformasRequestModel, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiPutPlataformas}`, { method: 'PUT', data: obj })
        .then(response => callback(response.data))
        .catch(error => errorCallback(error));
}

export function deletePlataforma(idPlataforma: number, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiDeletePlataformas}/${idPlataforma}`, { method: 'DELETE' })
        .then(response => callback(response.data))
        .catch(error => errorCallback(error));
}