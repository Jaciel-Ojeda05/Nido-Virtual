using UNITEC.NV.EL.Models.Usuarios;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.DAL.Usuarios.Interfaz
{
    public interface IUsuariosRepository
    {
        Response<List<UsuariosResponse>> GetAllUsuarios();
        Response<string> PostUsuario(UsuariosRequest obj);
        Response<string> PutUsuario(UsuariosResponse obj);
        Response<string> DeleteUsuario(int idUsuario);
    }
}