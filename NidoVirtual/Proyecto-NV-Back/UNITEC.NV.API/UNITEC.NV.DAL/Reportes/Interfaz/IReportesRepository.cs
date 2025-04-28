using UNITEC.NV.EL.Models.Reportes;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Reportes.Interfaz
{
    public interface IReportesRepository
    {
        Response<List<ReportesResponse>> GetAllReportes();
        Response<string> PostReporte(AddReportesRequest obj);
        Response<string> PutReporte(UpdateReportesRequest obj);
        Response<string> DeleteReporte(int idVenta);
    }
}