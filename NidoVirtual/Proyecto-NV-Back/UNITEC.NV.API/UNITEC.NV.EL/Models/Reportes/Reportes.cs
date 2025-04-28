namespace UNITEC.NV.EL.Models.Reportes
{
    public class ReportesResponse
    {
        public int idVenta { get; set; }
        public string nombreUsuario { get; set; } = string.Empty;
        public string videojuego { get; set; } = string.Empty;
        public int cantidad { get; set; }
        public decimal totalVentas { get; set; }
        public string fechaVenta { get; set; } = string.Empty;
    }

    public class AddReportesRequest
    {
        public int idUsuario { get; set; }
        public int idVideojuegos { get; set; }
        public int cantidad { get; set; }
    }

    public class UpdateReportesRequest
    {
        public int idVenta { get; set; }
        public int idUsuario { get; set; }
        public int idVideojuegos { get; set; }
        public int cantidad { get; set; }
    }
}