using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BP_API.Models
{
    public partial class BPContext : DbContext
    {
        public BPContext()
        {
        }

        public BPContext(DbContextOptions<BPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cuentum> Cuenta { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<Parametro> Parametros { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
                //optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnectionTest");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__D5946642CCBC73A0");

                entity.ToTable("Cliente");

                entity.HasIndex(e => e.PersonaId, "UQ__Cliente__7C34D32208425B87")
                    .IsUnique();

                entity.Property(e => e.Contrasena).HasMaxLength(100);

                entity.Property(e => e.FechaActualizacion).HasColumnType("date");

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.PersonaId).HasColumnName("PersonaID");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_Parametro");

                entity.HasOne(d => d.Persona)
                    .WithOne(p => p.Cliente)
                    .HasForeignKey<Cliente>(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_Persona");
            });

            modelBuilder.Entity<Cuentum>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__Cuenta__D41FD706264724A1");

                entity.HasIndex(e => e.NumeroCuenta, "UQ__Cuenta__E039507B7BBB02C7")
                    .IsUnique();

                entity.Property(e => e.FechaActualizacion).HasColumnType("date");

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.NumeroCuenta).HasMaxLength(50);

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.CuentumEstadoNavigations)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuenta_Cliente");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.CuentumTipoNavigations)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuenta_Parametro");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento)
                    .HasName("PK__Movimien__881A6AE0587B2040");

                entity.ToTable("Movimiento");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaActualizacion).HasColumnType("date");

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimiento_Cuenta");

                entity.HasOne(d => d.TipoMovimientoNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.TipoMovimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimiento_Parametro");
            });

            modelBuilder.Entity<Parametro>(entity =>
            {
                entity.HasKey(e => e.IdParametro)
                    .HasName("PK__Parametr__37B016F47B1C8F26");

                entity.ToTable("Parametro");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualizacion).HasColumnType("date");

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.Valor)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.HasIndex(e => e.Identificacion, "UQ__Persona__D6F931E5BFDA4CCB")
                    .IsUnique();

                entity.Property(e => e.Direccion).HasMaxLength(200);

                entity.Property(e => e.FechaActualizacion).HasColumnType("date");

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Identificacion).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(15);

                entity.HasOne(d => d.GeneroNavigation)
                    .WithMany(p => p.PersonaGeneroNavigations)
                    .HasForeignKey(d => d.Genero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Parametro");

                entity.HasOne(d => d.TipoIdentificacionNavigation)
                    .WithMany(p => p.PersonaTipoIdentificacionNavigations)
                    .HasForeignKey(d => d.TipoIdentificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Parametro_Tipo_Identificacion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
