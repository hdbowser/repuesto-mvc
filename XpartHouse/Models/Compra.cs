using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }
        public DateTime Fecha { get; set; }
        [MaxLength(50)]
        public string Referencia { get; set; }
        public bool Pagada { get; set; }
        public decimal Balance { get; set; }
        [Range(1,int.MaxValue)]
        public int IdProveedor { get; set; }
       [ForeignKey("IdProveedor")]
        public Contacto Proveedor { get; set; }
        public ICollection<CompraDetalleItem> CompraDetalleItems { get; set; }
    }
}