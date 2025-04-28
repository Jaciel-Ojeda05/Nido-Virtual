using UNITEC.NV.DAL.NVContext.EntidadesContext;
using UNITEC.NV.DAL.Videojuegos.Interfaz;
using UNITEC.NV.EL.Models.Videojuegos;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Videojuegos
{
    public class VideojuegoRepository : IVideojuegosRepository
    {
        private readonly NVContext.NVContext _context;

        public VideojuegoRepository(NVContext.NVContext context)
        {
            _context = context;
        }

        public Response<List<VideojuegosResponse>> GetAllVideojuegos()
        {
            Response<List<VideojuegosResponse>> res = new Response<List<VideojuegosResponse>>();
            try
            {
                res.Data = _context.Videojuegos
                                    .Select(v => new VideojuegosResponse
                                    {
                                        idVideojuegos = v.IdVideojuego,
                                        titulo = v.Titulo,
                                        descVideojuego = v.DescVideojuego,
                                        fechaLanzamiento = v.FechaLanzamiento.ToString("dd-MM-yyyy"),
                                        precio = v.Precio,
                                        stock = v.Stock,
                                        descGenero = v.IdGeneroNavigation.DescGenero,
                                        descPlataforma = v.IdPlataformaNavigation.DescPlataforma,
                                        imgVideojuego = v.ImgVideojuego!
                                    })
                                    .OrderBy(x => x.titulo)
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

        public async Task<Response<string>> PostVideojuego(VideojuegosFormRequest obj)
        {
            var res = new Response<string>();
            try
            {
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(obj.ImagenArchivo.FileName);
                string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);
                string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await obj.ImagenArchivo.CopyToAsync(stream);
                }
                int nuevoId = _context.Videojuegos.Any()
                    ? _context.Videojuegos.Max(v => v.IdVideojuego) + 1
                    : 1;
                var videojuego = new Videojuego
                {
                    IdVideojuego = nuevoId,
                    Titulo = obj.Titulo,
                    DescVideojuego = obj.DescVideojuego,
                    FechaLanzamiento = DateOnly.Parse(obj.FechaLanzamiento),
                    Precio = obj.Precio,
                    Stock = obj.Stock,
                    IdGenero = obj.IdGenero,
                    IdPlataforma = obj.IdPlataforma,
                    ImgVideojuego = nombreArchivo
                };
                _context.Videojuegos.Add(videojuego);
                _context.SaveChanges();
                res.Mensaje = "Videojuego agregado exitosamente.";
                res.Codigo = 200;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.InnerException?.Message ?? ex.Message;
                res.Codigo = 500;
            }
            return res;
        }

        public Response<string> PutVideojuego(AddVideojuegosResponse obj)
        {
            Response<string> res = new Response<string>();
            try
            {
                var videojuego = _context.Videojuegos.FirstOrDefault(x => x.IdVideojuego == obj.idVideojuegos);
                if (videojuego != null)
                {
                    videojuego.Titulo = obj.titulo;
                    videojuego.DescVideojuego = obj.descVideojuego;
                    videojuego.FechaLanzamiento = DateOnly.Parse(obj.fechaLanzamiento);
                    videojuego.Precio = obj.precio;
                    videojuego.Stock = obj.stock;
                    videojuego.IdGenero = obj.idGenero;
                    videojuego.IdPlataforma = obj.idPlataforma;
                    videojuego.ImgVideojuego = obj.imgVideojuego;
                    _context.SaveChanges();
                    res.Mensaje = "Videojuego actualizado exitosamente.";
                    res.Codigo = 200;
                }
                else
                {
                    res.Mensaje = "Videojuego no encontrado";
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

        public Response<string> DeleteVideojuego(int idVideojuego)
        {
            Response<string> res = new Response<string>();
            try
            {
                var videojuego = _context.Videojuegos.FirstOrDefault(m => m.IdVideojuego == idVideojuego);
                if (videojuego != null)
                {
                    bool tieneVentasAsociadas = _context.Ventas.Any(v => v.IdVideojuegos == idVideojuego);

                    if (tieneVentasAsociadas)
                    {
                        res.Mensaje = "No es posible eliminar el videojuego porque tiene ventas asociadas.";
                        res.Codigo = 400;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(videojuego.ImgVideojuego))
                        {
                            string rutaEliminar = Path.Combine(Environment.CurrentDirectory, "wwwroot", "images", "videojuegos", videojuego.ImgVideojuego);
                            if (File.Exists(rutaEliminar))
                            {
                                File.Delete(rutaEliminar);
                            }
                        }

                        _context.Videojuegos.Remove(videojuego);
                        _context.SaveChanges();
                        res.Mensaje = "Videojuego eliminado exitosamente.";
                        res.Codigo = 200;
                    }
                }
                else
                {
                    res.Mensaje = "Videojuego no encontrado";
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