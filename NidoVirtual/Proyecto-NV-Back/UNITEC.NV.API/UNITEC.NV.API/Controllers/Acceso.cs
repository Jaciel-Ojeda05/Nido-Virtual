using UNITEC.NV.BL.Acceso.Interfaz;
using UNITEC.NV.EL.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UNITEC.NV.EL.Models.Acceso;

namespace UNITEC.NV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Acceso : ControllerBase
    {
        private readonly IAccesoManager _Manager;

        public Acceso(IAccesoManager Manager)
        {
            _Manager = Manager;
        }

        [HttpPost("Login", Name = nameof(Login))]

        public ActionResult Login(AccesoRequest obj)
        {
            return Ok(_Manager.Login(obj));
        }

        [HttpPost("RestablePassword", Name = nameof(RestablePassword))]
        public Response<string> RestablePassword(RestablecerPasswordRequest obj)
        {
            return _Manager.RestablePassword(obj);
        }
    }
}
