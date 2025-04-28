using Microsoft.AspNetCore.Mvc;
using UNITEC.NV.BL.Plataformas.Interfaz;
using UNITEC.NV.EL.Models.Plataformas;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformasController : ControllerBase
    {
        private readonly IPlataformasManager _manager;

        public PlataformasController(IPlataformasManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAllPlataforma")]
        public Response<List<PlataformasResponse>> GetAllPlataforma()
        {
            return _manager.GetAllPlataforma();
        }

        [HttpPost("PostPlataforma")]
        public Response<string> PostPlataforma([FromBody] PlataformasRequestPost request)
        {
            Console.WriteLine($"Recibiendo descPlataforma: {request?.descPlataforma}");
            return _manager.PostPlataforma(request?.descPlataforma!);
        }

        [HttpPut("PutPlataforma")]
        public Response<string> PutPlataforma(PlataformasRequest obj)
        {
            return _manager.PutPlataforma(obj);
        }

        [HttpDelete("DeletePlataforma/{idPlataforma}")]
        public Response<string> DeletePlataforma(int idPlataforma)
        {
            return _manager.DeletePlataforma(idPlataforma);
        }
    }
}