using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Semillitas.Api.Entities;

namespace Semillitas.Api.Data;

public partial class SemillitasContext : DbContext
{
    public SemillitasContext()
    {
    }

    public SemillitasContext(DbContextOptions<SemillitasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<InscripcionDetalle> Inscripciondetalles { get; set; }

    public virtual DbSet<Inscripcion> Inscripciones { get; set; }

    public virtual DbSet<Nino> Ninos { get; set; }

    public virtual DbSet<Periodo> Periodos { get; set; }

    public virtual DbSet<Taller> Talleres { get; set; }

    public virtual DbSet<Tutor> Tutores { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=localhost;port=3306;database=semillitas;user=root;password=futbolfc", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.46-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.Nombre, "IX_Categorias_Nombre").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<InscripcionDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inscripciondetalles");

            entity.HasIndex(e => new { e.InscripcionId, e.TallerId }, "IX_InscripcionDetalles_InscripcionId_TallerId").IsUnique();

            entity.HasIndex(e => e.TallerId, "IX_InscripcionDetalles_TallerId");

            entity.Property(e => e.Precio).HasPrecision(10, 2);

            entity.HasOne(d => d.Inscripcion).WithMany(p => p.InscripcionDetalles)
                .HasForeignKey(d => d.InscripcionId)
                .HasConstraintName("FK_InscripcionDetalles_Inscripciones_InscripcionId");

            entity.HasOne(d => d.Taller).WithMany(p => p.InscripcionDetalles)
                .HasForeignKey(d => d.TallerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InscripcionDetalles_Talleres_TallerId");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inscripciones");

            entity.HasIndex(e => new { e.NinoId, e.PeriodoId }, "IX_Inscripciones_NinoId_PeriodoId").IsUnique();

            entity.HasIndex(e => e.PeriodoId, "IX_Inscripciones_PeriodoId");

            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasPrecision(10, 2);

            entity.HasOne(d => d.Nino).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.NinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscripciones_Ninos_NinoId");

            entity.HasOne(d => d.Periodo).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.PeriodoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscripciones_Periodos_PeriodoId");
        });

        modelBuilder.Entity<Nino>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ninos");

            entity.HasIndex(e => e.TutorId, "IX_Ninos_TutorId");

            entity.Property(e => e.Nombre).HasMaxLength(150);

            entity.HasOne(d => d.Tutor).WithMany(p => p.Ninos)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ninos_Tutores_TutorId");
        });

        modelBuilder.Entity<Periodo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("periodos");

            entity.HasIndex(e => e.Nombre, "IX_Periodos_Nombre").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Taller>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("talleres");

            entity.HasIndex(e => e.CategoriaId, "IX_Talleres_CategoriaId");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.CostoMensual).HasPrecision(10, 2);
            entity.Property(e => e.HoraFin).HasColumnType("time");
            entity.Property(e => e.HoraInicio).HasColumnType("time");
            entity.Property(e => e.Instructor).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Talleres)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Talleres_Categorias_CategoriaId");
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tutores");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
