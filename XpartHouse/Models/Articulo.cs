using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class Articulo
    {
        [Key]
        public int IdArticulo { get; set; }
        [StringLength(150)]
        public string Descripcion { get; set; }
        [Range(1,int.MaxValue)]
        [MaxLength(150)]
        public string CodigoBarras { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public int LineaRoja { get; set; }
        public int Existencia { get; set; }
        public DateTime FechaCreado { get; set; }
        [MaxLength(50)]
        public string Unidad { get; set; }
        public bool Eliminado { get; set; }

        [ForeignKey("Familia")]
        public int IdFamilia { get; set; }
        public Familia Familia { get; set; }

    }
}