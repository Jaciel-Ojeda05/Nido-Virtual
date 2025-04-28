using Microsoft.AspNetCore.Mvc;
using UNITEC.NV.BL.Videojuegos.Interfaz;
using UNITEC.NV.EL.Models.Videojuegos;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideojuegosController : ControllerBase
    {
        private readonly IVideojuegosManager _manager;

        public VideojuegosController(IVideojuegosManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAllVideojuegos")]
        public Response<List<VideojuegosResponse>> GetAllVideojuegos()
        {
            return _manager.GetAllVideojuegos();
        }

        [HttpPost("PostVideojuego")]
        public async Task<Response<string>> PostVideojuego([FromForm] VideojuegosFormRequest obj)
        {
            return await _manager.PostVideojuego(obj);
        }

        [HttpPut("PutVideojuego")]
        public Response<string> PutVideojuego(AddVideojuegosResponse obj)
        {
            return _manager.PutVideojuego(obj);
        }

        [HttpDelete("DeleteVideojuego")]
        public Response<string> DeleteVideojuego(int idVideojuego)
        {
            return _manager.DeleteVideojuego(idVideojuego);
        }
    }
}