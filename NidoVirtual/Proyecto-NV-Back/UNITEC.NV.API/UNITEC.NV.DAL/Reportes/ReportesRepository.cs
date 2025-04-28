using UNITEC.NV.DAL.NVContext.EntidadesContext;
using UNITEC.NV.DAL.Reportes.Interfaz;
using UNITEC.NV.EL.Models.Reportes;
using UNITEC.NV.EL.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace UNITEC.NV.DAL.Reportes
{
    public class ReportesRepository : IReportesRepository
    {
        private readonly NVContext.NVContext _context;

        public ReportesRepository(NVContext.NVContext context)
        {
            _context = context;
        }

        public Response<List<ReportesResponse>> GetAllReportes()
        {
            Response<List<ReportesResponse>> res = new Response<List<ReportesResponse>>();
            try
            {
                res.Data = _context.Ventas
                                  .Include(v => v.IdUsuarioNavigation)
                                  .Include(v => v.IdVideojuegosNavigation)
                                  .Select(v => new ReportesResponse
                                  {
                                      idVenta = v.IdVenta,
                                      nombreUsuario = $"{v.IdUsuarioNavigation.NombreUsuario} {v.IdUsuarioNavigation.ApePaterno} {v.IdUsuarioNavigation.ApeMaterno}",
                                      videojuego = v.IdVideojuegosNavigation.Titulo,
                                      cantidad = v.Cantidad,
                                      totalVentas = v.TotalVenta,
                                      fechaVenta = v.FechaVenta.HasValue ? v.FechaVenta.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty
                                  })
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

        public Response<string> PostReporte(AddReportesRequest obj)
        {
            Response<string> res = new Response<string>();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var videojuego = _context.Videojuegos.FirstOrDefault(v => v.IdVideojuego == obj.idVideojuegos);
                var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == obj.idUsuario);
                if (videojuego == null)
                {
                    res.Mensaje = "Videojuego no encontrado.";
                    res.Codigo = 404;
                    return res;
                }
                if (usuario == null)
                {
                    res.Mensaje = "Usuario no encontrado.";
                    res.Codigo = 404;
                    return res;
                }
                if (videojuego.Stock < obj.cantidad)
                {
                    res.Mensaje = $"No hay suficiente stock para '{videojuego.Titulo}'. Stock actual: {videojuego.Stock}";
                    res.Codigo = 400;
                    return res;
                }
                int nuevoIdVenta = _context.Ventas.Any()
                    ? _context.Ventas.Max(v => v.IdVenta) + 1
                    : 1;
                _context.Ventas.Add(new Venta
                {
                    IdVenta = nuevoIdVenta,
                    IdUsuario = obj.idUsuario,
                    IdVideojuegos = obj.idVideojuegos,
                    Cantidad = obj.cantidad,
                    TotalVenta = obj.cantidad * videojuego.Precio,
                    FechaVenta = DateTime.Now   
                });
                videojuego.Stock -= obj.cantidad;
                _context.SaveChanges();
                transaction.Commit();
                res.Mensaje = "Venta registrada exitosamente.";
                res.Codigo = 200;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                res.Mensaje = ex.Message;
                res.Codigo = 500;
            }
            return res;
        }

        public Response<string> PutReporte(UpdateReportesRequest obj)
        {
            Response<string> res = new Response<string>();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var venta = _context.Ventas.Include(v => v.IdVideojuegosNavigation)
                                     .FirstOrDefault(x => x.IdVenta == obj.idVenta);
                var videojuego = _context.Videojuegos.FirstOrDefault(v => v.IdVideojuego == obj.idVideojuegos);
                var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == obj.idUsuario);

                if (venta == null)
                {
                    res.Mensaje = "Venta no encontrada.";
                    res.Codigo = 404;
                    return res;
                }

                if (videojuego == null)
                {
                    res.Mensaje = "Videojuego no encontrado.";
                    res.Codigo = 404;
                    return res;
                }

                if (usuario == null)
                {
                    res.Mensaje = "Usuario no encontrado.";
                    res.Codigo = 404;
                    return res;
                }

                venta.IdVideojuegosNavigation.Stock += venta.Cantidad;

                if (videojuego.Stock < obj.cantidad)
                {
                    res.Mensaje = $"No hay suficiente stock para '{videojuego.Titulo}'. Stock actual: {videojuego.Stock}";
                    res.Codigo = 400;
                    return res;
                }

                venta.IdUsuario = obj.idUsuario;
                venta.IdVideojuegos = obj.idVideojuegos;
                venta.Cantidad = obj.cantidad;
                venta.TotalVenta = obj.cantidad * videojuego.Precio;
                venta.FechaVenta = DateTime.Now;

                videojuego.Stock -= obj.cantidad;

                _context.SaveChanges();
                transaction.Commit();

                res.Mensaje = "Venta actualizada exitosamente.";
                res.Codigo = 200;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                res.Mensaje = ex.Message;
                res.Codigo = 500;
            }
            return res;
        }

        public Response<string> DeleteReporte(int idVenta)
        {
            Response<string> res = new Response<string>();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var venta = _context.Ventas.Include(v => v.IdVideojuegosNavigation)
                                     .FirstOrDefault(m => m.IdVenta == idVenta);
                if (venta == null)
                {
                    res.Mensaje = "Venta no encontrada.";
                    res.Codigo = 404;
                    return res;
                }

                venta.IdVideojuegosNavigation.Stock += venta.Cantidad;

                _context.Ventas.Remove(venta);
                _context.SaveChanges();
                transaction.Commit();

                res.Mensaje = "Venta eliminada exitosamente.";
                res.Codigo = 200;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                res.Mensaje = ex.Message;
                res.Codigo = 500;
            }
            return res;
        }
    }
}