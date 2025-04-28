using UNITEC.NV.BL.Generos.Interfaz;
using UNITEC.NV.DAL.Generos;
using UNITEC.NV.EL.Models.Generos;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.BL.Generos
{
    public class GeneroManager : IGenerosManager
    {
        private readonly GeneroRepository _repository;

        public GeneroManager(GeneroRepository repository)
        {
            _repository = repository;
        }

        public Response<List<GenerosRequest>> GetAllGenero()
        {
            return _repository.GetAllGenero();
        }

        public Response<string> PostGenero(string descGenero)
        {
            return _repository.PostGenero(descGenero);
        }

        public Response<string> PutGenero(GenerosRequest obj)
        {
            return _repository.PutGenero(obj);
        }

        public Response<string> DeleteGenero(int idGenero)
        {
            return _repository.DeleteGenero(idGenero);
        }
    }
}
