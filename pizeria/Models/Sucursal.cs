using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pizeria.Models
{
    public class Sucursal
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public List<Pedido> pedidos { get; set; }
        public Usuario administrador { get; set; }
    }
}