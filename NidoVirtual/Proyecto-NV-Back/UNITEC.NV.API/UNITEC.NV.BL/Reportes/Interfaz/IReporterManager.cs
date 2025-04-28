using UNITEC.NV.EL.Models.Reportes;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.BL.Reportes.Interfaz
{
    public interface IReportesManager
    {
        Response<List<ReportesResponse>> GetAllReportes();
        Response<string> PostReporte(AddReportesRequest obj);
        Response<string> PutReporte(UpdateReportesRequest obj);
        Response<string> DeleteReporte(int idVenta);
    }
}