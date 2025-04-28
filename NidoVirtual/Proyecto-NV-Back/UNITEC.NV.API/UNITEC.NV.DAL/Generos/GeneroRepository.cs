using UNITEC.NV.DAL.Generos.Interfaz;
using UNITEC.NV.DAL.NVContext.EntidadesContext;
using UNITEC.NV.EL.Models.Generos;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Generos
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly NVContext.NVContext _context;

        public GeneroRepository(NVContext.NVContext context)
        {
            _context = context;
        }

        public Response<List<GenerosRequest>> GetAllGenero()
        {
            Response<List<GenerosRequest>> res = new Response<List<GenerosRequest>>();
            try
            {
                res.Data = (from g in _context.Generos
                            select new GenerosRequest
                            {
                                idGenero = g.IdGenero,
                                descGenero = g.DescGenero
                            }).OrderBy(x => x.descGenero).ToList();

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

        public Response<string> PostGenero(string descGenero)
        {
            Response<string> res = new Response<string>();
            try
            {
                if (_context.Generos.Any(g => g.DescGenero == descGenero))
                {
                    res.Mensaje = $"El género '{descGenero}' ya existe.";
                    res.Codigo = 409;
                    return res;
                }
                int nuevoId = _context.Generos.Any()
                    ? _context.Generos.Max(g => g.IdGenero) + 1
                    : 1;
                _context.Generos.Add(new Genero
                {
                    IdGenero = nuevoId,
                    DescGenero = descGenero
                });
                _context.SaveChanges();
                res.Mensaje = "Género agregado exitosamente.";
                res.Codigo = 200;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
                res.Codigo = 500;
            }
            return res;
        }

        public Response<string> PutGenero(GenerosRequest obj)
        {
            Response<string> res = new Response<string>();
            try
            {
                Genero? gro = _context.Generos.FirstOrDefault(x => x.IdGenero == obj.idGenero);
                if (gro != null)
                {
                    gro.DescGenero = obj.descGenero;
                    _context.SaveChanges();
                    res.Mensaje = "Género actualizado exitosamente.";
                    res.Codigo = 200;
                }
                else
                {
                    res.Mensaje = "Género no encontrado";
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

        public Response<string> DeleteGenero(int idGenero)
        {
            Response<string> res = new Response<string>();
            try
            {
                var gro = _context.Generos.FirstOrDefault(m => m.IdGenero == idGenero);
                if (gro != null)
                {
                    bool hayVideojuegosAsociados = _context.Videojuegos.Any(v => v.IdGenero == idGenero);

                    if (hayVideojuegosAsociados)
                    {
                        res.Mensaje = "No es posible eliminar el género porque hay datos relacionados a él.";
                        res.Codigo = 400;
                    }
                    else
                    {
                        _context.Generos.Remove(gro);
                        _context.SaveChanges();
                        res.Mensaje = "Género eliminado exitosamente.";
                        res.Codigo = 200;
                    }
                }
                else
                {
                    res.Mensaje = "Género no encontrado";
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
