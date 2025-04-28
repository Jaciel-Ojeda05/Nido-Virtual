using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNITEC.NV.DAL.NVContext.EntidadesContext;

[Index("DescGenero", Name = "UQ__Generos__42FE13E1788EDCD1", IsUnique = true)]
public partial class Genero
{
    [Key]
    [Column("idGenero")]
    public int IdGenero { get; set; }

    [Column("descGenero")]
    [StringLength(50)]
    [Unicode(false)]
    public string DescGenero { get; set; } = null!;

    [InverseProperty("IdGeneroNavigation")]
    public virtual ICollection<Videojuego> Videojuegos { get; set; } = new List<Videojuego>();
}
