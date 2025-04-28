using UNITEC.NV.DAL.NVContext.EntidadesContext;
using UNITEC.NV.EL.Models.Response;
using UNITEC.NV.EL.Models.Videojuegos;
using Microsoft.AspNetCore.Http;

namespace UNITEC.NV.DAL.Videojuegos.Interfaz
{
    public interface IVideojuegosRepository
    {
        Response<List<VideojuegosResponse>> GetAllVideojuegos();
        Task<Response<string>> PostVideojuego(VideojuegosFormRequest obj);
        Response<string> PutVideojuego(AddVideojuegosResponse obj);
        Response<string> DeleteVideojuego(int idVideojuego);
    }
}