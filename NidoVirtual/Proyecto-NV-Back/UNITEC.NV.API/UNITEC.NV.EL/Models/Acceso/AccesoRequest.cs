namespace UNITEC.NV.EL.Models.Acceso
{
    public class AccesoRequest
    {
        public string correo { get; set; } = string.Empty;
        public string userPassword { get; set; } = string.Empty;
    }
    public class AccesoResponse
    {
        public string token { get; set; } = string.Empty;
        public int idCliente { get; set; }
        public string nombreUsuario { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public bool isAdmin { get; set; }
    }
    public class RestablecerPasswordRequest
    {
        public int IdUsuario { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }
}
