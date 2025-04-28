using Microsoft.AspNetCore.Mvc;
using UNITEC.NV.BL.Usuarios.Interfaz;
using UNITEC.NV.EL.Models.Usuarios;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosManager _manager;

        public UsuariosController(IUsuariosManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAllUsuarios")]
        public Response<List<UsuariosResponse>> GetAllUsuarios()
        {
            return _manager.GetAllUsuarios();
        }

        [HttpPost("PostUsuario")]
        public Response<string> PostUsuario(UsuariosRequest obj)
        {
            return _manager.PostUsuario(obj);
        }

        [HttpPut("PutUsuario")]
        public Response<string> PutUsuario(UsuariosResponse obj)
        {
            return _manager.PutUsuario(obj);
        }

        [HttpDelete("DeleteUsuario")]
        public Response<string> DeleteUsuario(int idUsuario)
        {
            return _manager.DeleteUsuario(idUsuario);
        }
    }
}