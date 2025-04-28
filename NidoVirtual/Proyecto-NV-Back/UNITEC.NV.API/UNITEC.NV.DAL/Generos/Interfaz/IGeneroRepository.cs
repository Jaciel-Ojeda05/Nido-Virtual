using UNITEC.NV.EL.Models.Generos;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Generos.Interfaz
{
    public interface IGeneroRepository
    {
        public Response<List<GenerosRequest>> GetAllGenero();
        public Response<string> PostGenero(string descGenero);
        public Response<string> PutGenero(GenerosRequest obj);
        public Response<string> DeleteGenero(int idGenero);
    }
}
