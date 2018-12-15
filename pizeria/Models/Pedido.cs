using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace pizeria.Models
{
    public class Pedido
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }
        public double importe { get; set; }
        public String direccion { get; set; }
        public String telefono { get; set; }
        public List<Producto> productos { get; set; }
        public EstadoPedido estado { get; set; }
        public Sucursal sucursal { get; set; }
        public Usuario usuario { get; set; }
    }
}