using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class PeriodoCaja
    {
        [Key]
        public int IdPriodo { get; set; }
        [Range(1,int.MaxValue)]
        public decimal MontoInicial { get; set; }
        public DateTime FechaInicial { get; set; }
        public decimal MontoCierre { get; set; }
        public DateTime FechaCierre { get; set; }
        public bool Finalizado { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Contacto Usuario { get; set; }
        public ICollection<TransaccionCaja> Transacciones { get; set; }
    }
}