using Microsoft.AspNetCore.Mvc;
using UNITEC.NV.BL.Generos.Interfaz;
using UNITEC.NV.EL.Models.Generos;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IGenerosManager _manager;
        
        public GenerosController(IGenerosManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAllGenero")]
        public Response<List<GenerosRequest>> GetAllGenero()
        {
            return _manager.GetAllGenero();
        }

        [HttpPost("PostGenero")]
        public Response<string> PostGenero([FromBody] GenerosRequestPost request)
        {
            Console.WriteLine($"Recibiendo descGenero: {request?.descGenero}");
            return _manager.PostGenero(request?.descGenero!);
        }

        [HttpPut("PutGenero")]
        public Response<string> PutGenero(GenerosRequest obj)
        {
            return _manager.PutGenero(obj);
        }

        [HttpDelete("DeleteGenero/{idGenero}")]
        public Response<string> DeleteGenero(int idGenero)
        {
            return _manager.DeleteGenero(idGenero);
        }
    }
}