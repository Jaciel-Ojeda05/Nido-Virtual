import Interseptor from '../../interseptor/Interseptor.service';
import { ResponseModel } from "@/core/models/Response/Response.model";
import { PostReportesModel } from '@/core/models/Nido-Virtual/Reportes/PostRepostesRequest.model';
import { PutReportesRquestModel } from '@/core/models/Nido-Virtual/Reportes/PutReportesRequest.model';
import { ReportesResponseModel } from '@/core/models/Nido-Virtual/Reportes/ReportesResponse.model';

const apieUrl = import.meta.env.VITE_API_DEV;
const apiGetReportes = import.meta.env.VITE_REPORTES_GETALLREPORTES;
const apiPostReportes = import.meta.env.VITE_REPORTES_POST;
const apiPutReportes = import.meta.env.VITE_REPORTES_PUT;
const apiDeleteReportes = import.meta.env.VITE_REPORTES_DELETE;

export function getAllReportes(callback: (response: ResponseModel<ReportesResponseModel[]>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiGetReportes}`, { method: 'GET' })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function postReporte(obj: PostReportesModel, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiPostReportes}`, { method: 'POST', data: obj })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function putReporte(obj: PutReportesRquestModel, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiPutReportes}`, { method: 'PUT', data: obj })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}

export function deleteReporte(idVenta: number, callback: (response: ResponseModel<string>) => void, errorCallback: (error: Error) => void): void {
    Interseptor(`${apieUrl}${apiDeleteReportes}?idVenta=${idVenta}`, { method: 'DELETE' })
        .then(response => {
            callback(response.data);
        })
        .catch(error => {
            errorCallback(error);
        });
}