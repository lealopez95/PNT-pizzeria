using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pizeria.Models;

namespace pizeria.Controllers
{
    public class UsuariosController : Controller
    {
        private ContextoDBContext db = new ContextoDBContext();
        /*
        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }
        */
        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NombreApellido,Email,Direccion,Password,ConfirmacionPassword,Telefono")] Usuario usuario)
        {
            // Si la password entra
            if (usuario.Password != null && usuario.Password.Length > 8 && usuario.Password.Equals(usuario.ConfirmacionPassword))
            {

                // Buscamos si no hay otro usuario igual
                Usuario otroIgual;

                try
                {

                    otroIgual = db.usuarios.Where(n => n.Email == usuario.Email).Single();
                }
                // Si no hay lanza una excepción, ingresamos al nuevo usuario y volvemos a home
                catch (Exception e)
                {
                    if (ModelState.IsValid)
                    {
                        db.usuarios.Add(usuario);
                        db.SaveChanges();
                    }

                    // Creamos una cookie por 15 minutos y se la pasamos al usuario
                    HttpCookie UserCookie = new HttpCookie("usuario", usuario.ID.ToString());
                    UserCookie.Expires.AddMinutes(15);
                    HttpContext.Response.SetCookie(UserCookie);

                    return RedirectToAction("Index", "Home");
                }

                // Había un usuario igual
                return HttpNotFound();
            }

            // La password era menor a 8 caracteres, o no coincidía con la confirmación
            return HttpNotFound();
            
        }

        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Password")] Login login)
        {
            // Buscamos el usuario cuyo mail y password coincidan
            Usuario usuario = null;
            try {
                
                usuario = db.usuarios.Where(n => n.Email == login.Email && n.Password == login.Password).Single();
            // Si no existe se lanza una excepcion
            } catch(Exception e)
            {
                return RedirectToAction("LoginIncorrecto", "Home");
            }

            // Creamos una cookie por 15 minutos y se la pasamos al usuario
            HttpCookie UserCookie = new HttpCookie("usuario", usuario.ID.ToString());
            UserCookie.Expires.AddMinutes(15);
            HttpContext.Response.SetCookie(UserCookie);

            // Ponemos los datos del usuario en la sesión
            Session["ID"] = usuario.ID;
            Session["Nombre"] = usuario.NombreApellido;

            // Si el usuario es administrador vamos a pedidos
            if (usuario.esAdministrador)
            {
                return RedirectToAction("Index", "Pedidos");
            }

            // Si es un usuario normal, volvemos a Home/Index
            return RedirectToAction("Index", "Home");
        }

        // POST: Usuarios/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {

            // Para hacer logout primero buscamos si existe la cookie en el usuario
            HttpCookie cookie = Request.Cookies["usuario"];
            if (cookie != null)
            {
                // Si existe, la borramos, haciendo que expiró ayer
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.SetCookie(cookie);

                // Borramos los datos de la sesión actual
                Session["ID"] = null;
                Session["Nombre"] = null;
            }

            // Volvemos a Home/Index
            return RedirectToAction("Index", "Home");
        }

    }
}
