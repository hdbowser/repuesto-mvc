using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class TransaccionCaja
    {
        [Key]
        public int IdTransaccionCaja { get; set; }
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        [Range(1, int.MaxValue)]
        public decimal Valor { get; set; }
        [MaxLength(1)]
        [RegularExpression("[SsEe]")]
        public string Tipo { get; set; }
        [ForeignKey("PeriodoCaja")]
        public int IdPeriodoCaja { get; set; }
        public PeriodoCaja PeriodoCaja { get; set; }
    }
}