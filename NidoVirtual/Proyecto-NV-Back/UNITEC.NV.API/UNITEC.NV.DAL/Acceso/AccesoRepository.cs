using UNITEC.NV.DAL.Acceso.Interfaz;
using UNITEC.NV.DAL.NVContext.EntidadesContext;
using UNITEC.NV.EL.Models.Acceso;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Acceso
{
    public class AccesoRepository : IAccesoRepository
    {
        private readonly NVContext.NVContext _context;
        public AccesoRepository(NVContext.NVContext context)
        {
            _context = context;
        }

        public AccesoResponse Login(AccesoRequest obj)
        {
            try
            {
                AccesoResponse usr = (from i in _context.Usuarios
                                      where i.Correo == obj.correo
                                      && i.UserPassword == obj.userPassword
                                      select new AccesoResponse
                                      {
                                          idCliente = i.IdUsuario,
                                          nombreUsuario = $"{i.NombreUsuario} {i.ApePaterno} {i.ApeMaterno}",
                                          correo = i.Correo,
                                          token = "", 
                                          isAdmin = i.IsAdmin
                                      }).FirstOrDefault() ?? new AccesoResponse();
                return usr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<string> RestablePassword(int IdUsuario, string NewPassword)
        {
            var res = new Response<string>();
            try
            {
                Usuario? user = _context.Usuarios.FirstOrDefault(x => x.IdUsuario == IdUsuario);
                if (user != null)
                {
                    user.UserPassword = NewPassword;
                    _context.SaveChanges();
                    res.Mensaje = "Éxito";
                    res.Codigo = 200;
                }
                else
                {
                    res.Mensaje = "Usuario no encontrado";
                    res.Codigo = 404;
                }
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
                res.Codigo = 500;
            }
            return res;
        }
    }
}
