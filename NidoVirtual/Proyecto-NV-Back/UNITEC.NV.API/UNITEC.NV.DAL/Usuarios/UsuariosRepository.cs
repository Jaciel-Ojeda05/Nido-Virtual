using UNITEC.NV.DAL.NVContext.EntidadesContext;
using UNITEC.NV.DAL.Usuarios.Interfaz;
using UNITEC.NV.EL.Models.Usuarios;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Usuarios
{
    public class UsuarioRepository : IUsuariosRepository
    {
        private readonly NVContext.NVContext _context;

        public UsuarioRepository(NVContext.NVContext context)
        {
            _context = context;
        }

        public Response<List<UsuariosResponse>> GetAllUsuarios()
        {
            Response<List<UsuariosResponse>> res = new Response<List<UsuariosResponse>>();
            try
            {
                res.Data = _context.Usuarios
                                  .Select(c => new UsuariosResponse
                                  {
                                      idCliente = c.IdUsuario,
                                      nombreUsuario = c.NombreUsuario,
                                      apePaterno = c.ApePaterno,
                                      apeMaterno = c.ApeMaterno,
                                      correo = c.Correo,
                                      isAdmin = c.IsAdmin
                                  })
                                  .OrderBy(x => x.nombreUsuario)
                                  .ToList();

                res.Mensaje = "Éxito";
                res.Codigo = 200;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
                res.Codigo = 500;
            }
            return res;
        }

        public Response<string> PostUsuario(UsuariosRequest obj)
        {
            Response<string> res = new Response<string>();
            try
            {
                if (_context.Usuarios.Any(u => u.Correo == obj.correo))
                {
                    res.Mensaje = $"El correo '{obj.correo}' ya está registrado.";
                    res.Codigo = 409;
                    return res;
                }
                int nuevoIdUsuario = _context.Usuarios.Any()
                    ? _context.Usuarios.Max(u => u.IdUsuario) + 1
                    : 1;
                _context.Usuarios.Add(new Usuario
                {
                    IdUsuario = nuevoIdUsuario,
                    NombreUsuario = obj.nombreUsuario,
                    ApePaterno = obj.apePaterno,
                    ApeMaterno = obj.apeMaterno,
                    Correo = obj.correo,
                    UserPassword = obj.userPassword,
                    IsAdmin = obj.isAdmin,
                    FechaRegistro = DateTime.Now
                });
                _context.SaveChanges();
                res.Mensaje = "Usuario agregado exitosamente.";
                res.Codigo = 200;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
                res.Codigo = 500;
            }
            return res;
        }

        public Response<string> PutUsuario(UsuariosResponse obj)
        {
            Response<string> res = new Response<string>();
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(x => x.IdUsuario == obj.idCliente);
                if (usuario != null)
                {
                    usuario.NombreUsuario = obj.nombreUsuario;
                    usuario.ApePaterno = obj.apePaterno;
                    usuario.ApeMaterno = obj.apeMaterno;
                    usuario.Correo = obj.correo;
                    usuario.UserPassword = obj.userPassword;
                    usuario.IsAdmin = obj.isAdmin;
                    _context.SaveChanges();
                    res.Mensaje = "Usuario actualizado exitosamente.";
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

        public Response<string> DeleteUsuario(int idUsuario)
        {
            Response<string> res = new Response<string>();
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(m => m.IdUsuario == idUsuario);
                if (usuario != null)
                {
                    bool tieneVentasAsociadas = _context.Ventas.Any(v => v.IdUsuario == idUsuario);

                    if (tieneVentasAsociadas)
                    {
                        res.Mensaje = "No es posible eliminar el usuario porque tiene ventas asociadas.";
                        res.Codigo = 400;
                    }
                    else
                    {
                        _context.Usuarios.Remove(usuario);
                        _context.SaveChanges();
                        res.Mensaje = "Usuario eliminado exitosamente.";
                        res.Codigo = 200;
                    }
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