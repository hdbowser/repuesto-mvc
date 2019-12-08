using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace XpartHouse.Models
{
    public class ContextDB : DbContext
    {
        public ContextDB() : base("Conexion")
        {

        }
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            dbModelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraDetalleItem> CompraDetalleItems { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalleItem> FacturaDetalleItems { get; set; }
        public DbSet<Familia> Familias { get; set; }
        public DbSet<PeriodoCaja> PeriodoCajas { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<TransaccionCaja> TransaccionesCaja { get; set; }
    }
}