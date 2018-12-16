using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using pizeria.Models;

namespace pizeria.Controllers
{
    public class PedidosController : Controller
    {
        private ContextoDBContext db = new ContextoDBContext();



        [HttpPost]
        public ActionResult addProductToChart(int id,int qty)
        {
            Producto producto;
            Pedido pedido;
            Usuario user;

            Session["ID"] = 1000;

            if (Session["ID"] != null){
                producto = db.productos.Find(id);
                user = db.usuarios.Find(Session["ID"]);
                /**
                 pedido = buscarPedido(user.ID);
                 if(pedido != null){
                    crearPedido();
                }else{
                    agregarProductoAlPedido();
                }
                 
                 */

                //test Inicia
                Usuario usuario = new Usuario();
                usuario.ID = (int)Session["ID"];
                usuario.NombreApellido = "Jorge Gomez";
                usuario.Email = "abutelman@gmail.com";

                Sucursal sucursal = new Sucursal();
                sucursal.ID = 23;
                sucursal.Nombre = "Caballito";
                sucursal.Direccion = "Curapaligue 19";
                sucursal.administrador = usuario;


                List<Producto> productos = new List<Producto>();
                productos.Add(producto);
                
                pedido = new Pedido();
                pedido.ID = 4;
                pedido.fecha = new DateTime(2018, 12, 18);
                pedido.importe = 312.31;
                pedido.direccion = "Sanabria 724, 4to B";
                pedido.telefono = "4912-3135";
                pedido.estado = EstadoPedido.PENDIENTE;
                pedido.sucursal = sucursal;
                pedido.productos = productos;
                Session["cartQty"] = productos.Count;
                //Test Finaliza

                return Json(pedido);
            }else{
                return Json("Debe iniciar sesión");
            }

            
           
            
        }



        // GET: Pedidos/SetearPendiente/1
        public string SetearPendiente(int id)
        {
            Pedido pedidoAModificar = db.pedidos.Find(id);
            pedidoAModificar.estado = EstadoPedido.PENDIENTE;

            db.SaveChanges();

            return ("PENDIENTE " + id.ToString());
        }

        // GET: Pedidos/SetearCancelado/1
        public string SetearCancelado(int id)
        {
            Pedido pedidoAModificar = db.pedidos.Find(id);
            pedidoAModificar.estado = EstadoPedido.CANCELADO;

            db.SaveChanges();

            return ("CANCELADO " + id.ToString());
        }

        // GET: Pedidos/SetearEntregado/1
        public string SetearEntregado(int id)
        {
            Pedido pedidoAModificar = db.pedidos.Find(id);
            pedidoAModificar.estado = EstadoPedido.ENTREGADO;

            db.SaveChanges();

            return ("ENTREGADO " + id.ToString());
        }

        // GET: Pedidos
        public ActionResult Index()
        {
            List<Pedido> pedidos;
            Usuario usuario;

            // Si hay una sesión iniciada
            if (Session["ID"] != null)
            {
                // Buscamos al usuario que tenga la ID de la sesión
                try
                {
                    usuario = db.usuarios.Find(Session["ID"]);

                    if (usuario.esAdministrador)
                    {

                        // Tomamos todos los pedidos cuyo ID del administrador coincida con el ID del usuario
                        pedidos = db.pedidos.Where(n => n.sucursal.administrador.ID == usuario.ID).ToList();

                    }
                    else
                    {
                        // Si no, acceso denegado
                        return HttpNotFound();
                    }
                } catch (Exception e)
                // Si no se encuentra, fue borrado de la db, acceso denegado
                {
                    return HttpNotFound();
                }
                
            } else
            // Si no, acceso denegado;
            {
                return HttpNotFound();
            }
            
            return View(pedidos);
        }

        public ActionResult mandarMail(Pedido pedido)
        {
            // Creamos un pedido de prueba, luego borrar
            Usuario usuario = new Usuario();
            usuario.ID = 1000;
            usuario.NombreApellido = "Jorge Gomez";
            usuario.Email = "abutelman@gmail.com";

            Sucursal sucursal = new Sucursal();
            sucursal.ID = 23;
            sucursal.Nombre = "Caballito";
            sucursal.Direccion = "Curapaligue 19";
            sucursal.administrador = usuario;

            Producto producto1 = new Producto();
            producto1.Descripcion = "Pizza grande de muzzarella";

            Producto producto2 = new Producto();
            producto2.Descripcion = "Empanada de carne";

            List<Producto> productos = new List<Producto>();
            productos.Add(producto1);
            productos.Add(producto2);
            
            pedido = new Pedido();
            pedido.ID = 4;
            pedido.fecha = new DateTime(2018, 12, 18);
            pedido.importe = 312.31;
            pedido.direccion = "Sanabria 724, 4to B";
            pedido.telefono = "4912-3135";
            pedido.estado = EstadoPedido.PENDIENTE;
            pedido.sucursal = sucursal;
            pedido.productos = productos;
            /////////////////
            
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Credentials = new NetworkCredential("abutelman@gmail.com", "anwrda3711");
            client.EnableSsl = true;
            client.Port = 25; //465, 587

            MailAddress EmailPizzeria = new MailAddress("abutelman@gmail.com");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = EmailPizzeria;
            mailMessage.To.Add(pedido.sucursal.administrador.Email);
            mailMessage.Subject = "Pedido para la sucursal " + pedido.sucursal.Nombre;

            string body = "ID: " + pedido.ID.ToString() + "\n";
            body += "Fecha: " + pedido.fecha.ToShortDateString() + "\n";
            body += "Dirección de envío: " + pedido.direccion + "\n";
            body += "Importe: $" + pedido.importe.ToString() + "\n";
            body += "Teléfono de contacto: " + pedido.telefono + "\n\n";
            body += "Pedido: \n\n";

            foreach (Producto producto in pedido.productos)
            {
                body += producto.Descripcion + "\n";
            }

            mailMessage.Body = body;

            client.Send(mailMessage);

            return View();
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,fecha,importe,direccion,telefono,pedido")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,fecha,importe,direccion,telefono,pedido")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.pedidos.Find(id);
            db.pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
