using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public class Contacto
    {
        public Contacto()
        {
            this.EsCliente = false;
            this.EsProveedor = false;
            this.EsUsuario = false;
        }
        [Key]
        public int IdContacto { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Apellidos { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(512)]
        public string Password { get; set; }
        [MaxLength(20)]
        public string RNC { get; set; }
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(200)]
        public string Direccion{ get; set; }
        public bool EsUsuario { get; set; }
        public bool EsCliente { get; set; }
        public bool EsProveedor { get; set; }
        public DateTime FechaCreado { get; set; }

        [ForeignKey("Rol")]
        public int? IdRol { get; set; }
        public Rol Rol { get; set; }
    }
}