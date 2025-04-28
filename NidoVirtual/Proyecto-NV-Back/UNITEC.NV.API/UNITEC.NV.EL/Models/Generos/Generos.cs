namespace UNITEC.NV.EL.Models.Generos
{
    public class GenerosRequest 
    {
        public int idGenero { get; set; }
        public string descGenero { get; set; } = string.Empty;
    }

    public class GenerosRequestPost
    {
        public string? descGenero { get; set; }
    }
}
