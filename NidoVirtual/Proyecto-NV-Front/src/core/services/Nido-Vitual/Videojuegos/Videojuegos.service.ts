import Interseptor from '../../interseptor/Interseptor.service';
import { ResponseModel } from "@/core/models/Response/Response.model";
import { AddVideojuegosResponse } from '@/core/models/Nido-Virtual/Videojuegos/AddVideojuegosResponse.model';
import { VideojuegosRequestModel } from '@/core/models/Nido-Virtual/Videojuegos/VideojuegosRequest.model';
import { VideojuegosResponseModel } from '@/core/models/Nido-Virtual/Videojuegos/VideojuegosResponse.model';

const apieUrl = import.meta.env.VITE_API_DEV;
const apiGetVideojuegos = import.meta.env.VITE_VIDEOJUEGOS_GETALLVIDEOJUEGOS;
const apiPostVideojuegos = import.meta.env.VITE_VIDEOJUEGOS_POST;
const apiPutVideojuegos = import.meta.env.VITE_VIDEOJUEGOS_PUT;
const apiDeleteVideojuegos = import.meta.env.VITE_VIDEOJUEGOS_DELETE;

export function getAllVideojuegos(callback: (response: ResponseModel<VideojuegosResponseModel[]>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiGetVideojuegos}`, { method: 'GET' })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function postVideojuego(obj: VideojuegosRequestModel, imageFile: File | null, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    const formData = new FormData();
    formData.append('titulo', obj.titulo);
    formData.append('descVideojuego', obj.descVideojuego);
    formData.append('fechaLanzamiento', obj.fechaLanzamiento);
    formData.append('precio', obj.precio.toString());
    formData.append('stock', obj.stock.toString());
    formData.append('idGenero', obj.idGenero.toString());
    formData.append('idPlataforma', obj.idPlataforma.toString());
    if (imageFile) {
        formData.append('ImagenArchivo', imageFile);
    }

    Interseptor(`${apieUrl}${apiPostVideojuegos}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'multipart/form-data',
        },
        data: formData,
    })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function putVideojuego(obj: AddVideojuegosResponse, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiPutVideojuegos}`, { method: 'PUT', data: obj })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function deleteVideojuego(idVideojuego: number, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiDeleteVideojuegos}?idVideojuego=${idVideojuego}`, { method: 'DELETE' })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}