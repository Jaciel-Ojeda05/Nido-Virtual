using UNITEC.NV.EL.Models.Plataformas;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.BL.Plataformas.Interfaz
{
    public interface IPlataformasManager
    {
        Response<List<PlataformasResponse>> GetAllPlataforma();
        Response<string> PostPlataforma(string descPlataforma);
        Response<string> PutPlataforma(PlataformasRequest obj);
        Response<string> DeletePlataforma(int idPlataforma);
    }
}