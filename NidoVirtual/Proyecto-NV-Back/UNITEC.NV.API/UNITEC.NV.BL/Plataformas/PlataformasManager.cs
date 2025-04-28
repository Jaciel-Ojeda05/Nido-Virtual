using UNITEC.NV.BL.Plataformas.Interfaz;
using UNITEC.NV.DAL.Plataformas;
using UNITEC.NV.EL.Models.Plataformas;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.BL.Plataformas
{
    public class PlataformaManager : IPlataformasManager
    {
        private readonly PlataformaRepository _repository;

        public PlataformaManager(PlataformaRepository repository)
        {
            _repository = repository;
        }

        public Response<List<PlataformasResponse>> GetAllPlataforma()
        {
            return _repository.GetAllPlataforma();
        }

        public Response<string> PostPlataforma(string descPlataforma)
        {
            return _repository.PostPlataforma(descPlataforma);
        }

        public Response<string> PutPlataforma(PlataformasRequest obj)
        {
            return _repository.PutPlataforma(obj);
        }

        public Response<string> DeletePlataforma(int idPlataforma)
        {
            return _repository.DeletePlataforma(idPlataforma);
        }
    }
}