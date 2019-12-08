using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class Familia
    {
        [Key]
        public int IdFamilia { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        ICollection<Articulo> Articulos;
    }
}