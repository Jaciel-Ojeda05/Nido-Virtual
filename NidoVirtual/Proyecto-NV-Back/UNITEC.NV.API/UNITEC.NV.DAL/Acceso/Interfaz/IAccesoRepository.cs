using UNITEC.NV.EL.Models.Acceso;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Acceso.Interfaz
{
    public interface IAccesoRepository
    {
        AccesoResponse Login(AccesoRequest obj);
        public Response<string> RestablePassword(int IdUsuario, string NewPassword);
    }
}
