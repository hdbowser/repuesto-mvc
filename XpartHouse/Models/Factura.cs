using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Balance { get; set; }
        public bool Pagada { get; set; }

        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Contacto Cliente { get; set; }

        [Range(1,int.MaxValue)]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Contacto Usuario { get; set; }

        ICollection<FacturaDetalleItem> FacturaDetalleItems { get; set; }


    }
}