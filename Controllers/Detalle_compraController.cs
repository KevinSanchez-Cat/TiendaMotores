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
    public class Detalle_compraController : Controller
    {

        private TIENDAEntities db = new TIENDAEntities();

        // GET: Detalle_compra
        public ActionResult Index()
        {
            var detalle_compra = db.Detalle_compra.Include(d => d.Compra).Include(d => d.Producto);
            return View(detalle_compra.ToList());
        }

        // GET: Detalle_compra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_compra detalle_compra = db.Detalle_compra.Find(id);
            if (detalle_compra == null)
            {
                return HttpNotFound();
            }
            return View(detalle_compra);
        }

        // GET: Detalle_compra/Create
        public ActionResult Create()
        {
            ViewBag.Id_compra = new SelectList(db.Compra, "id_compra", "id_compra");
            ViewBag.id_producto = new SelectList(db.Producto, "Id_producto", "nombre_producto");
            return View();
        }

        // POST: Detalle_compra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_compra,id_producto,cantidad,subtotal")] Detalle_compra detalle_compra)
        {
            if (ModelState.IsValid)
            {
                db.Detalle_compra.Add(detalle_compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_compra = new SelectList(db.Compra, "id_compra", "id_compra", detalle_compra.Id_compra);
            ViewBag.id_producto = new SelectList(db.Producto, "Id_producto", "nombre_producto", detalle_compra.id_producto);
            return View(detalle_compra);
        }

        // GET: Detalle_compra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_compra detalle_compra = db.Detalle_compra.Find(id);
            if (detalle_compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_compra = new SelectList(db.Compra, "id_compra", "id_compra", detalle_compra.Id_compra);
            ViewBag.id_producto = new SelectList(db.Producto, "Id_producto", "nombre_producto", detalle_compra.id_producto);
            return View(detalle_compra);
        }

        // POST: Detalle_compra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_compra,id_producto,cantidad,subtotal")] Detalle_compra detalle_compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle_compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_compra = new SelectList(db.Compra, "id_compra", "id_compra", detalle_compra.Id_compra);
            ViewBag.id_producto = new SelectList(db.Producto, "Id_producto", "nombre_producto", detalle_compra.id_producto);
            return View(detalle_compra);
        }

        // GET: Detalle_compra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_compra detalle_compra = db.Detalle_compra.Find(id);
            if (detalle_compra == null)
            {
                return HttpNotFound();
            }
            return View(detalle_compra);
        }

        // POST: Detalle_compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle_compra detalle_compra = db.Detalle_compra.Find(id);
            db.Detalle_compra.Remove(detalle_compra);
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
