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
    public class PaqueteriasController : Controller
    {
        private TIENDAEntities db = new TIENDAEntities();

        // GET: Paqueterias
        public ActionResult Index()
        {
            var paqueteria = db.Paqueteria.Include(p => p.Direccion);
            return View(paqueteria.ToList());
        }

        // GET: Paqueterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // GET: Paqueterias/Create
        public ActionResult Create()
        {
            ViewBag.id_direccion = new SelectList(db.Direccion, "id_direccion", "estado");
            return View();
        }

        // POST: Paqueterias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_paqueteria,nombre_paqueteria,telefono,sitio_web,rfc,contacto,telefono_contacto,id_direccion")] Paqueteria paqueteria)
        public ActionResult Create(string nombre_paqueteria, string telefono, string sitio_web, string rfc, string contacto, string telefono_contacto, string estado, string municipio, string ciudad, string codigo_postal, string calle_externa, string num_calle_externa, string calle_interna, string num_calle_interna, string ref1, string ref2, string pais)
        {
            int numCallExterna = Int32.Parse(num_calle_externa.ToString());
            int numCalleInterna = Int32.Parse(num_calle_interna.ToString());
            Direccion direccion = new Direccion();

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
            id = db.Direccion.Max(o => o.id_direccion);

            db.Direccion.Add(direccion);
            db.SaveChanges();

            int telefonoP = Int32.Parse(telefono.ToString());
            int rfcP = Int32.Parse(rfc.ToString());
            int telefono_contactoP = Int32.Parse(telefono_contacto.ToString());
            Paqueteria paqueteria = new Paqueteria();
            paqueteria.nombre_paqueteria = nombre_paqueteria;
            paqueteria.telefono = telefonoP;
            paqueteria.sitio_web = sitio_web;
            paqueteria.rfc = rfcP;
            paqueteria.contacto = contacto;
            paqueteria.telefono_contacto = telefono_contactoP;
            paqueteria.id_direccion = id;
           
                db.Paqueteria.Add(paqueteria);
                db.SaveChanges();
                return RedirectToAction("Index");
          
        }

        // GET: Paqueterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_direccion = new SelectList(db.Direccion, "id_direccion", "estado", paqueteria.id_direccion);
            return View(paqueteria);
        }

        // POST: Paqueterias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_paqueteria,nombre_paqueteria,telefono,sitio_web,rfc,contacto,telefono_contacto,id_direccion")] Paqueteria paqueteria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paqueteria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_direccion = new SelectList(db.Direccion, "id_direccion", "estado", paqueteria.id_direccion);
            return View(paqueteria);
        }

        // GET: Paqueterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // POST: Paqueterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paqueteria paqueteria = db.Paqueteria.Find(id);
            db.Paqueteria.Remove(paqueteria);
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
