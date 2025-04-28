using Microsoft.AspNetCore.Mvc;
using UNITEC.NV.BL.Reportes.Interfaz;
using UNITEC.NV.EL.Models.Reportes;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesManager _manager;

        public ReportesController(IReportesManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAllReportes")]
        public Response<List<ReportesResponse>> GetAllReportes()
        {
            return _manager.GetAllReportes();
        }

        [HttpPost("PostReporte")]
        public Response<string> PostReporte(AddReportesRequest obj)
        {
            return _manager.PostReporte(obj);
        }

        [HttpPut("PutReporte")]
        public Response<string> PutReporte(UpdateReportesRequest obj)
        {
            return _manager.PutReporte(obj);
        }

        [HttpDelete("DeleteReporte")]
        public Response<string> DeleteReporte(int idVenta)
        {
            return _manager.DeleteReporte(idVenta);
        }
    }
}