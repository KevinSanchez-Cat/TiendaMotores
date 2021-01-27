using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaMotores.Models;

namespace TiendaMotores.Controllers
{
    [Authorize]
    public class DireccionController : Controller
    {
        private ContextoTienda db = new ContextoTienda();

        // GET: Direccion
        public ActionResult Index()
        {
            return View(db.Direccion.ToList());
        }

        // GET: Direccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccion.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            return View(direccion);
        }

        // GET: Direccion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Direccion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_direccion,estado,municipio,ciudad,codigo_postal,telefono,direccion_1,direccion_2")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                db.Direccion.Add(direccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(direccion);
        }

        // GET: Direccion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccion.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            return View(direccion);
        }

        // POST: Direccion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_direccion,estado,municipio,ciudad,codigo_postal,telefono,direccion_1,direccion_2")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(direccion);
        }

        // GET: Direccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccion.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            return View(direccion);
        }

        // POST: Direccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Direccion direccion = db.Direccion.Find(id);
            db.Direccion.Remove(direccion);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        //  public ActionResult Create([Bind(Include = "id_cliente,nombre_cliente,apellido_p,apellido_m,id_direccion,telefono,email,id_tarjeta,id_usuario,nombre_usuario,contrasenia")] Cliente cliente)
        //Nombres de la vista tienen que ser los nombres de los paramentros
        public ActionResult CrearDir(string nombre_usuario, string nombre_cliente, string apellido_p, string apellido_m, string estado, string municipio, string ciudad, string codigo_postal, string telefono, string calle_externa, int num_calle_externa, string calle_interna, int num_calle_interna, string ref1, string ref2, string pais, string fecha_nacimiento)
        {
            string email2 = Session["correo"].ToString();
            var query = db.Cliente.FirstOrDefault(x => x.email == email2);

            int numCallExterna = Int32.Parse(num_calle_externa.ToString());
            int numCalleInterna = Int32.Parse(num_calle_interna.ToString());

            Direccion direccion;
            if (query.id_direccion == null)
            {
                direccion = new Direccion();
            }
            else
            {
                var query2 = db.Direccion.FirstOrDefault(x => x.id_direccion == query.id_direccion);
                direccion = query2;
            }


            direccion.estado = estado;
            direccion.municipio = municipio;
            direccion.ciudad = ciudad;
            direccion.codigo_postal = codigo_postal;
            direccion.telefono = telefono;
            direccion.calle_externa = calle_externa;
            direccion.num_calle_externa = numCallExterna;
            direccion.calle_interna = calle_interna;
            direccion.num_calle_interna = numCalleInterna;
            direccion.ref1 = ref1;
            direccion.ref2 = ref2;
            direccion.pais = pais;
            int id = 1;
            if (query.id_direccion == null)
            {
                db.Direccion.Add(direccion);
                db.SaveChanges();

                id = db.Direccion.Max(o => o.id_direccion);
               
            }
            else
            {
                db.SaveChanges();
                id = (int)query.id_direccion;
            }



            Cliente cliente = db.Cliente.FirstOrDefault(x => x.email == email2);

            DateTime fecha_nacC = DateTime.Parse(fecha_nacimiento.ToString());
            cliente.nombre_cliente = nombre_cliente;
            cliente.apellido_p = apellido_p;
            cliente.apellido_m = apellido_m;
            cliente.id_direccion = id;
            cliente.telefono = telefono;
            cliente.nombre_usuario = nombre_usuario;
            cliente.fecha_nacimiento = fecha_nacC;
            cliente.email = email2;
            cliente.id_direccion = id;
            db.SaveChanges();



            string[] nombre = cliente.nombre_cliente.ToString().Split(' ');
            Session["nombre"] = nombre[0];
            Session["usuario"] = nombre_usuario;
            ViewBag.direccion = id;
            return RedirectToAction("CrearOrden", "MetodoPago");

        }

    }
}
