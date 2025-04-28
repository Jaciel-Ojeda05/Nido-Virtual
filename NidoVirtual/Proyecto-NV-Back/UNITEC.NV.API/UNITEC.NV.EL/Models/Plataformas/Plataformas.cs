namespace UNITEC.NV.EL.Models.Plataformas
{
    public class PlataformasRequest
    {
        public int idPlataforma { get; set; }
        public string descPlataforma { get; set; } = string.Empty;
    }

    public class PlataformasResponse
    {
        public int idPlataforma { get; set; }
        public string descPlataforma { get; set; } = string.Empty;
    }

    public class PlataformasRequestPost
    {
        public string? descPlataforma { get; set; }
    }
}