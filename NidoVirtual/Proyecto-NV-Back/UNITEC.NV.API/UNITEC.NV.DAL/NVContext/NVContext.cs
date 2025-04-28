using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UNITEC.NV.DAL.NVContext.EntidadesContext;

namespace UNITEC.NV.DAL.NVContext;

public partial class NVContext : DbContext
{
    public NVContext(DbContextOptions<NVContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Plataforma> Plataformas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<Videojuego> Videojuegos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Generos__85223DA36DE651C6");

            entity.Property(e => e.IdGenero).ValueGeneratedNever();
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.IdPlataforma).HasName("PK__Platafor__43607CCE6FDBF44A");

            entity.Property(e => e.IdPlataforma).ValueGeneratedNever();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A6520A8F66");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__077D5614E9DC50AF");

            entity.Property(e => e.IdVenta).ValueGeneratedNever();
            entity.Property(e => e.Cantidad).HasDefaultValue(1);
            entity.Property(e => e.FechaVenta).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idUsuario");

            entity.HasOne(d => d.IdVideojuegosNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idVideojuegos");
        });

        modelBuilder.Entity<Videojuego>(entity =>
        {
            entity.HasKey(e => e.IdVideojuego).HasName("PK__Videojue__A87B06EA9D04CD64");

            entity.Property(e => e.IdVideojuego).ValueGeneratedNever();

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Videojuegos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idGenero");

            entity.HasOne(d => d.IdPlataformaNavigation).WithMany(p => p.Videojuegos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idPlataforma");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
