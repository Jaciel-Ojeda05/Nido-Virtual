using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNITEC.NV.DAL.NVContext.EntidadesContext;

[Index("DescPlataforma", Name = "UQ__Platafor__A36AB55CB1E2D7DE", IsUnique = true)]
public partial class Plataforma
{
    [Key]
    [Column("idPlataforma")]
    public int IdPlataforma { get; set; }

    [Column("descPlataforma")]
    [StringLength(50)]
    [Unicode(false)]
    public string DescPlataforma { get; set; } = null!;

    [InverseProperty("IdPlataformaNavigation")]
    public virtual ICollection<Videojuego> Videojuegos { get; set; } = new List<Videojuego>();
}
