using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Back_Examen_Fn.Models;

public partial class Parcial02Context : DbContext
{
    public Parcial02Context()
    {
    }

    public Parcial02Context(DbContextOptions<Parcial02Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=FERNANDOBD\\SQLEXPRESS;database=Parcial02;Trusted_Connection=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingresos__3213E83FE094771C");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdTipo).HasColumnName("idTipo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(8, 4)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingresos__idTipo__398D8EEE");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipos__3213E83F6BF6F3A9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
