using UNITEC.NV.DAL.Acceso.Interfaz;
using UNITEC.NV.BL.Acceso.Interfaz;
using UNITEC.NV.EL.Models.Acceso;
using UNITEC.NV.EL.Models.AppSettings;
using UNITEC.NV.EL.Models.Response;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UNITEC.NV.BL.Acceso
{
    public class AccesoManager : IAccesoManager
    {
        private readonly IAccesoRepository _repository;
        private readonly AppSettings _appSettings;

        public AccesoManager(IAccesoRepository repository, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _appSettings = appSettings.Value;
        }
        public Response<AccesoResponse> Login(AccesoRequest obj)
        {
            return GetUserLdapForNet(obj);
        }
        private Response<AccesoResponse> GetUserLdapForNet(AccesoRequest obj)
        {
            Response<AccesoResponse> res = new Response<AccesoResponse>();
            try
            {
                res.Data = _repository.Login(obj);

                if (res.Data.idCliente == 0)
                {
                    res.Codigo = 500;
                    res.Mensaje = "El usuario o la contraseña ingresados son incorrectos, o el usuario no tiene acceso al sistema. Por favor, consulte al administrador.";
                }
                else
                {
                    string token = GenerateJwtToken(res.Data.correo);
                    res.Data.token = token;
                    res.Mensaje = "Usuario logeado con éxito";
                    res.Codigo = 200;
                }

            }
            catch (Exception ex)
            {
                res.Codigo = 500;
                res.Mensaje = ex.Message;
            }
            return res;
        }
        private string GenerateJwtToken(string userDesc)
        {
            string token = string.Empty;

            var keyBytes = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userDesc));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            token = tokenHandler.WriteToken(tokenConfig);

            return token;
        }

        public Response<string> RestablePassword(RestablecerPasswordRequest obj)
        {
            return _repository.RestablePassword(obj.IdUsuario, obj.NewPassword);
        }
    }
}
