using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNITEC.NV.DAL.NVContext.EntidadesContext;

public partial class Venta
{
    [Key]
    [Column("idVenta")]
    public int IdVenta { get; set; }

    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column("idVideojuegos")]
    public int IdVideojuegos { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("totalVenta", TypeName = "decimal(10, 2)")]
    public decimal TotalVenta { get; set; }

    [Column("fechaVenta", TypeName = "datetime")]
    public DateTime? FechaVenta { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Venta")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [ForeignKey("IdVideojuegos")]
    [InverseProperty("Venta")]
    public virtual Videojuego IdVideojuegosNavigation { get; set; } = null!;
}
