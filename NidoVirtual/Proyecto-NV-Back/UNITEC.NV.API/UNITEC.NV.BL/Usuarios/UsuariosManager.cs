using UNITEC.NV.BL.Usuarios.Interfaz;
using UNITEC.NV.DAL.Usuarios;
using UNITEC.NV.EL.Models.Usuarios;
using UNITEC.NV.EL.Models.Response;

namespace UNITEC.NV.BL.Usuarios
{
    public class UsuarioManager : IUsuariosManager
    {
        private readonly UsuarioRepository _repository;

        public UsuarioManager(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public Response<List<UsuariosResponse>> GetAllUsuarios()
        {
            return _repository.GetAllUsuarios();
        }

        public Response<string> PostUsuario(UsuariosRequest obj)
        {
            return _repository.PostUsuario(obj);
        }

        public Response<string> PutUsuario(UsuariosResponse obj)
        {
            return _repository.PutUsuario(obj);
        }

        public Response<string> DeleteUsuario(int idUsuario)
        {
            return _repository.DeleteUsuario(idUsuario);
        }
    }
}