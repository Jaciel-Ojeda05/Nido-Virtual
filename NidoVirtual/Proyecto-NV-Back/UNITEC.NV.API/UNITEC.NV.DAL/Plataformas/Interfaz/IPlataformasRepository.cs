using UNITEC.NV.EL.Models.Plataformas;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Plataformas.Interfaz
{
    public interface IPlataformasRepository
    {
        Response<List<PlataformasResponse>> GetAllPlataforma();
        Response<string> PostPlataforma(string descPlataforma);
        Response<string> PutPlataforma(PlataformasRequest obj);
        Response<string> DeletePlataforma(int idPlataforma);
    }
}