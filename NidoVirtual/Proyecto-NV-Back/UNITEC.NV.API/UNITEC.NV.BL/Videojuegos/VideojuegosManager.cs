using UNITEC.NV.BL.Videojuegos.Interfaz;
using UNITEC.NV.DAL.Videojuegos;
using UNITEC.NV.EL.Models.Videojuegos;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.BL.Videojuegos
{
    public class VideojuegoManager : IVideojuegosManager
    {
        private readonly VideojuegoRepository _repository;

        public VideojuegoManager(VideojuegoRepository repository)
        {
            _repository = repository;
        }

        public Response<List<VideojuegosResponse>> GetAllVideojuegos()
        {
            return _repository.GetAllVideojuegos();
        }

        public async Task<Response<string>> PostVideojuego(VideojuegosFormRequest obj)
        {
            return await _repository.PostVideojuego(obj);
        }

        public Response<string> PutVideojuego(AddVideojuegosResponse obj)
        {
            return _repository.PutVideojuego(obj);
        }

        public Response<string> DeleteVideojuego(int idVideojuego)
        {
            return _repository.DeleteVideojuego(idVideojuego);
        }
    }
}