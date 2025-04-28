using UNITEC.NV.BL.Reportes.Interfaz;
using UNITEC.NV.DAL.Reportes;
using UNITEC.NV.EL.Models.Reportes;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.BL.Reportes
{
    public class ReportesManager : IReportesManager
    {
        private readonly ReportesRepository _repository;

        public ReportesManager(ReportesRepository repository)
        {
            _repository = repository;
        }

        public Response<List<ReportesResponse>> GetAllReportes()
        {
            return _repository.GetAllReportes();
        }

        public Response<string> PostReporte(AddReportesRequest obj)
        {
            return _repository.PostReporte(obj);
        }

        public Response<string> PutReporte(UpdateReportesRequest obj)
        {
            return _repository.PutReporte(obj);
        }

        public Response<string> DeleteReporte(int idVenta)
        {
            return _repository.DeleteReporte(idVenta);
        }
    }
}