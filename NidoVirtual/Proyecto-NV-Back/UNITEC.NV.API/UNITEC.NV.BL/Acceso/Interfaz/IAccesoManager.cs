using UNITEC.NV.EL.Models.Acceso;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.BL.Acceso.Interfaz
{
    public interface IAccesoManager
    {
        Response<AccesoResponse> Login(AccesoRequest obj);
        public Response<string> RestablePassword(RestablecerPasswordRequest obj);
    }
}
