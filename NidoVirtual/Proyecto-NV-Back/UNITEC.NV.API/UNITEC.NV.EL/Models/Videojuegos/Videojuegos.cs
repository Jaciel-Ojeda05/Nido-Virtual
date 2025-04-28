using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace UNITEC.NV.EL.Models.Videojuegos
{
    public class VideojuegosRequest
    {
        public string titulo { get; set; } = string.Empty;
        public string descVideojuego { get; set; } = string.Empty;
        public string fechaLanzamiento { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int stock { get; set; }
        public int idGenero { get; set; }
        public int idPlataforma { get; set; }
        public string ImagenArchivo { get; set; } = string.Empty;
    }

    public class VideojuegosResponse
    {
        public int idVideojuegos { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string descVideojuego { get; set; } = string.Empty;
        public string fechaLanzamiento { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string descGenero { get; set; } = string.Empty;
        public string descPlataforma { get; set; } = string.Empty;
        public string imgVideojuego { get; set; } = string.Empty;
    }

    public class AddVideojuegosResponse
    {
        public int idVideojuegos { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string descVideojuego { get; set; } = string.Empty;
        public string fechaLanzamiento { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int stock { get; set; }
        public int idGenero { get; set; }
        public int idPlataforma { get; set; }
        public string imgVideojuego { get; set; } = string.Empty;
    }

    public class VideojuegosFormRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string DescVideojuego { get; set; } = string.Empty;
        public string FechaLanzamiento { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int IdGenero { get; set; }
        public int IdPlataforma { get; set; }
        public IFormFile ImagenArchivo { get; set; }
    }
}