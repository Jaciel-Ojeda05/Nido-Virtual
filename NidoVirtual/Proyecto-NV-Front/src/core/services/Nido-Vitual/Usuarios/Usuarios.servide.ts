import Interseptor from '../../interseptor/Interseptor.service';
import { ResponseModel } from "@/core/models/Response/Response.model";
import { UsuariosRequestModel } from '@/core/models/Nido-Virtual/Usuarios/UsuariosRequest.model';
import { UsuariosResponseModel } from '@/core/models/Nido-Virtual/Usuarios/UsuariosResponse.model';

const apieUrl = import.meta.env.VITE_API_DEV;
const apiGetUsuarios = import.meta.env.VITE_USUARIOS_GETALLUSUARIOS;
const apiPostUsuarios = import.meta.env.VITE_USUARIOS_POST;
const apiPutUsuarios = import.meta.env.VITE_USUARIOS_PUT;
const apiDeleteUsuarios = import.meta.env.VITE_USUARIOS_DELETE;

export function getAllUsuarios(callback: (response: ResponseModel<UsuariosResponseModel[]>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiGetUsuarios}`, { method: 'GET' })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function postUsuario(obj: UsuariosRequestModel, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiPostUsuarios}`, { method: 'POST', data: obj })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function putUsuario(obj: UsuariosResponseModel, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiPutUsuarios}`, { method: 'PUT', data: obj })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function deleteUsuario(idUsuario: number, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiDeleteUsuarios}?idUsuario=${idUsuario}`, { method: 'DELETE' })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}