using UNITEC.NV.EL.Models.Response;
using UNITEC.NV.EL.Models.Videojuegos;

namespace UNITEC.NV.BL.Videojuegos.Interfaz
{
    public interface IVideojuegosManager
    {
        Response<List<VideojuegosResponse>> GetAllVideojuegos();
        Task<Response<string>> PostVideojuego(VideojuegosFormRequest obj);
        Response<string> PutVideojuego(AddVideojuegosResponse obj);
        Response<string> DeleteVideojuego(int idVideojuego);
    }
}