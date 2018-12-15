using pizeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pizeria.Controllers
{
    public class ProductController : Controller
    {

        private ContextoDBContext db = new ContextoDBContext();

        //GET: Product
        public ActionResult Index(int category)
        {
            if (category == 0) {
                
                ViewBag.categoriaProducto = pizeria.Models.CategoriaProducto.PIZZA;

                return View(db.productos.ToList());
            } else if (category == 1)
            {
                ViewBag.categoriaProducto = pizeria.Models.CategoriaProducto.EMPANADA;

                return View(db.productos.ToList());
            }
            else
            {
                return HttpNotFound();
            }
            
        }
    }
}