using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNITEC.NV.DAL.NVContext.EntidadesContext;

public partial class Videojuego
{
    [Key]
    [Column("idVideojuego")]
    public int IdVideojuego { get; set; }

    [Column("titulo")]
    [StringLength(255)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [Column("descVideojuego", TypeName = "text")]
    public string DescVideojuego { get; set; } = null!;

    [Column("fechaLanzamiento")]
    public DateOnly FechaLanzamiento { get; set; }

    [Column("precio", TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("idGenero")]
    public int IdGenero { get; set; }

    [Column("idPlataforma")]
    public int IdPlataforma { get; set; }

    [Column("imgVideojuego")]
    [StringLength(255)]
    [Unicode(false)]
    public string ImgVideojuego { get; set; } = null!;

    [ForeignKey("IdGenero")]
    [InverseProperty("Videojuegos")]
    public virtual Genero IdGeneroNavigation { get; set; } = null!;

    [ForeignKey("IdPlataforma")]
    [InverseProperty("Videojuegos")]
    public virtual Plataforma IdPlataformaNavigation { get; set; } = null!;

    [InverseProperty("IdVideojuegosNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
