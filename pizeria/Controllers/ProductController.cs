using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pizeria.Controllers
{
    public class ProductController : Controller
    {
        //GET: Product
        public ActionResult Index( int category=1)
        {
            if (category == 1) {
                return View();
            }  else
            {
                return HttpNotFound();
            }
            
        }
    }
}