using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class CompraDetalleItem
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Compra")]
        public int IdCompra { get; set; }
        [Key, Column(Order = 1)]
        public int NumLinea { get; set; }
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int Descuento { get; set; }
        [ForeignKey("Articulo")]
        public int IdArticulo { get; set; }
        public Articulo Articulo { get; set; }
        public Compra Compra { get; set; }
    }
}