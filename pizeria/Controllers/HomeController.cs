using pizeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pizeria.Controllers
{
    public class HomeController : Controller
    {
        private ContextoDBContext db = new ContextoDBContext();

        public ActionResult Index()
        {
            // Al cargar la página buscamos si hay una cookie de sesión, si existe tomamos su ID y buscamos el usuario
            // en la base de datos. Luego lo agregamos a la sesión para que el Layout lo cargue.
            
            // Si no hay una sesión iniciada
            if (Session["ID"] == null)
            {
                Usuario usuario = null;

                // Pedimos la cookie
                HttpCookie cookie = Request.Cookies["usuario"];
                // Si hay
                if (cookie != null)
                {
                    // Buscamos el usuario en la base de datos que tenga el ID de la cookie
                    try
                    {
                        usuario = db.usuarios.Find(int.Parse(cookie.Value));

                        // Si el ID que está en la cookie no se encuentra más en la base de datos
                    }
                    catch (Exception e)
                    {
                        return HttpNotFound();
                    }

                    // Ponemos los datos del usuario en la sesión
                    Session["ID"] = usuario.ID;
                    Session["Nombre"] = usuario.NombreApellido;
                }
            }
            
            return View();
        }

        public ActionResult LoginIncorrecto ()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}