using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace pizeria.Models
{
    public class Usuario
    {

        [Key]
        public int ID { get; set; }
        public string NombreApellido { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Password { get; set; }
        public string ConfirmacionPassword { get; set; }
        public string Telefono { get; set; }
        public bool esAdministrador { get; set; }
        public int? IDSucursalAdministrada { get; set; }    // Un entero que puede ser NULL
    }
}