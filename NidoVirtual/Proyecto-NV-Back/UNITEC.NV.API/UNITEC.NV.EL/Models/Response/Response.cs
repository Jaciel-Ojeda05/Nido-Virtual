namespace UNITEC.NV.EL.Models.Response
{
    public class Response<T>
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Data { get; set; }

        public Response() { }

        public Response(int codigo, string mensaje, T data)
        {
            Codigo = codigo;
            Mensaje = mensaje;
            Data = data;
        }
    }
}
