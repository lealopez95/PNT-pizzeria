using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace pizeria.Models
{
    public class Producto
    {
        [Key]
        public int ID { get; set; }
        public CategoriaProducto Categoria { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
    }
}