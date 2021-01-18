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
    public class Orden_clienteController : Controller
    {
        private TIENDAEntities db = new TIENDAEntities();

        // GET: Orden_cliente
        public ActionResult Index()
        {
            var oden_cliente = db.Oden_cliente.Include(o => o.Compra).Include(o => o.Paqueteria);
            return View(oden_cliente.ToList());
        }

        // GET: Orden_cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oden_cliente oden_cliente = db.Oden_cliente.Find(id);
            if (oden_cliente == null)
            {
                return HttpNotFound();
            }
            return View(oden_cliente);
        }

        // GET: Orden_cliente/Create
        public ActionResult Create()
        {
            ViewBag.id_compra = new SelectList(db.Compra, "id_compra", "id_compra");
            ViewBag.id_paqueteria = new SelectList(db.Paqueteria, "id_paqueteria", "nombre_paqueteria");
            return View();
        }

        // POST: Orden_cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_orden,id_compra,num_comfirmacion,fecha_creacion,total,num_serie,fecha_envio,fecha_entrega,id_paqueteria")] Oden_cliente oden_cliente)
        {
            if (ModelState.IsValid)
            {
                db.Oden_cliente.Add(oden_cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_compra = new SelectList(db.Compra, "id_compra", "id_compra", oden_cliente.id_compra);
            ViewBag.id_paqueteria = new SelectList(db.Paqueteria, "id_paqueteria", "nombre_paqueteria", oden_cliente.id_paqueteria);
            return View(oden_cliente);
        }

        // GET: Orden_cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oden_cliente oden_cliente = db.Oden_cliente.Find(id);
            if (oden_cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_compra = new SelectList(db.Compra, "id_compra", "id_compra", oden_cliente.id_compra);
            ViewBag.id_paqueteria = new SelectList(db.Paqueteria, "id_paqueteria", "nombre_paqueteria", oden_cliente.id_paqueteria);
            return View(oden_cliente);
        }

        // POST: Orden_cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_orden,id_compra,num_comfirmacion,fecha_creacion,total,num_serie,fecha_envio,fecha_entrega,id_paqueteria")] Oden_cliente oden_cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oden_cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_compra = new SelectList(db.Compra, "id_compra", "id_compra", oden_cliente.id_compra);
            ViewBag.id_paqueteria = new SelectList(db.Paqueteria, "id_paqueteria", "nombre_paqueteria", oden_cliente.id_paqueteria);
            return View(oden_cliente);
        }

        // GET: Orden_cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oden_cliente oden_cliente = db.Oden_cliente.Find(id);
            if (oden_cliente == null)
            {
                return HttpNotFound();
            }
            return View(oden_cliente);
        }

        // POST: Orden_cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oden_cliente oden_cliente = db.Oden_cliente.Find(id);
            db.Oden_cliente.Remove(oden_cliente);
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
        public ActionResult Eliminar()
        {
            return View();
        }
        public ActionResult Modificar()
        {
            return View();
        }

        public ActionResult Ver()
        {
            return View();
        }

        public ActionResult Desplegar()
        {
            return View();
        }
        public ActionResult Buscar()
        {
            return View();
        }
    }

}
