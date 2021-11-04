using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace bancaVi.Models
{
    public partial class Db1 : DbContext
    {
        public Db1()
            : base("name=Db1")
        {
        }

        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<cuenta> cuenta { get; set; }
        public virtual DbSet<pago> pago { get; set; }
        public virtual DbSet<servicio> servicio { get; set; }
        public virtual DbSet<transferencia> transferencia { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cliente>()
                .Property(e => e.id_cliente)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.nombre_cliente)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.correo_cliente)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.contraseña_cliente)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.cuenta)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cuenta>()
                .Property(e => e.id_cliente)
                .IsUnicode(false);

            modelBuilder.Entity<cuenta>()
                .HasMany(e => e.pago)
                .WithRequired(e => e.cuenta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cuenta>()
                .HasMany(e => e.transferencia)
                .WithRequired(e => e.cuenta)
                .HasForeignKey(e => e.id_cuenta_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cuenta>()
                .HasMany(e => e.transferencia1)
                .WithRequired(e => e.cuenta1)
                .HasForeignKey(e => e.id_cuenta_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<servicio>()
                .Property(e => e.nombre_servicio)
                .IsUnicode(false);

            modelBuilder.Entity<servicio>()
                .HasMany(e => e.pago)
                .WithRequired(e => e.servicio)
                .WillCascadeOnDelete(false);
        }
    }
}
