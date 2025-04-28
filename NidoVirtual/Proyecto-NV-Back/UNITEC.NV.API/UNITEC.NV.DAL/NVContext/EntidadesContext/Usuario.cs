using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNITEC.NV.DAL.NVContext.EntidadesContext;

public partial class Usuario
{
    [Key]
    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column("nombreUsuario")]
    [StringLength(100)]
    [Unicode(false)]
    public string NombreUsuario { get; set; } = null!;

    [Column("apePaterno")]
    [StringLength(100)]
    [Unicode(false)]
    public string ApePaterno { get; set; } = null!;

    [Column("apeMaterno")]
    [StringLength(100)]
    [Unicode(false)]
    public string ApeMaterno { get; set; } = null!;

    [Column("correo")]
    [StringLength(100)]
    [Unicode(false)]
    public string Correo { get; set; } = null!;

    [Column("userPassword")]
    [StringLength(10)]
    [Unicode(false)]
    public string UserPassword { get; set; } = null!;

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [Column("isAdmin")]
    public bool IsAdmin { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
