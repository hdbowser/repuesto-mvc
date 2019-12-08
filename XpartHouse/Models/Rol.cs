using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        public string Nombre { get; set; }
        [MaxLength(256)]
        public string Permisos { get; set; }
    }
}