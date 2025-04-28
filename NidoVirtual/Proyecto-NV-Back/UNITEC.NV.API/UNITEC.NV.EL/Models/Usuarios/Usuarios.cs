namespace UNITEC.NV.EL.Models.Usuarios
{
    public class UsuariosRequest
    {
        public string nombreUsuario { get; set; } = string.Empty;
        public string apePaterno { get; set; } = string.Empty;
        public string apeMaterno { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string userPassword { get; set; } = string.Empty;
        public bool isAdmin { get; set; }
    }

    public class UsuariosResponse
    {
        public int idCliente { get; set; }
        public string nombreUsuario { get; set; } = string.Empty;
        public string apePaterno { get; set; } = string.Empty;
        public string apeMaterno { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string userPassword { get; set; } = string.Empty;
        public bool isAdmin { get; set; }
    }
}