using UNITEC.NV.DAL.NVContext.EntidadesContext;
using UNITEC.NV.DAL.Plataformas.Interfaz;
using UNITEC.NV.EL.Models.Plataformas;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Plataformas
{
    public class PlataformaRepository : IPlataformasRepository
    {
        private readonly NVContext.NVContext _context;

        public PlataformaRepository(NVContext.NVContext context)
        {
            _context = context;
        }

        public Response<List<PlataformasResponse>> GetAllPlataforma()
        {
            Response<List<PlataformasResponse>> res = new Response<List<PlataformasResponse>>();
            try
            {
                res.Data = _context.Plataformas
                                  .Select(p => new PlataformasResponse
                                  {
                                      idPlataforma = p.IdPlataforma,
                                      descPlataforma = p.DescPlataforma
                                  })
                                  .OrderBy(x => x.descPlataforma)
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

        public Response<string> PostPlataforma(string descPlataforma)
        {
            Response<string> res = new Response<string>();
            try
            {
                if (_context.Plataformas.Any(p => p.DescPlataforma == descPlataforma))
                {
                    res.Mensaje = $"La plataforma '{descPlataforma}' ya existe.";
                    res.Codigo = 409;
                    return res;
                }
                int nuevoId = _context.Plataformas.Any()
                    ? _context.Plataformas.Max(p => p.IdPlataforma) + 1
                    : 1;
                _context.Plataformas.Add(new Plataforma
                {
                    IdPlataforma = nuevoId,
                    DescPlataforma = descPlataforma
                });
                _context.SaveChanges();
                res.Mensaje = "Plataforma agregada exitosamente.";
                res.Codigo = 200;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
                res.Codigo = 500;
            }
            return res;
        }

        public Response<string> PutPlataforma(PlataformasRequest obj)
        {
            Response<string> res = new Response<string>();
            try
            {
                var plataforma = _context.Plataformas.FirstOrDefault(p => p.IdPlataforma == obj.idPlataforma);
                if (plataforma != null)
                {
                    plataforma.DescPlataforma = obj.descPlataforma;
                    _context.SaveChanges();
                    res.Mensaje = "Plataforma actualizada exitosamente.";
                    res.Codigo = 200;
                }
                else
                {
                    res.Mensaje = "Plataforma no encontrada";
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

        public Response<string> DeletePlataforma(int idPlataforma)
        {
            Response<string> res = new Response<string>();
            try
            {
                var plataforma = _context.Plataformas.FirstOrDefault(p => p.IdPlataforma == idPlataforma);
                if (plataforma != null)
                {
                    bool hayVideojuegosAsociados = _context.Videojuegos.Any(v => v.IdPlataforma == idPlataforma);

                    if (hayVideojuegosAsociados)
                    {
                        res.Mensaje = "No es posible eliminar la plataforma porque hay datos relacionados a ella.";
                        res.Codigo = 400;
                    }
                    else
                    {
                        _context.Plataformas.Remove(plataforma);
                        _context.SaveChanges();
                        res.Mensaje = "Plataforma eliminada exitosamente.";
                        res.Codigo = 200;
                    }
                }
                else
                {
                    res.Mensaje = "Plataforma no encontrada";
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