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
    public class EmpleadoController : Controller
    {
        private ContextoTienda db = new ContextoTienda();

        // GET: Empleado
        public ActionResult Index()
        {
            var empleado = db.Empleado.Include(e => e.Departamento).Include(e => e.Direccion).Include(e => e.Puesto);
            return View(empleado.ToList());
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "nombre_departamento");
            ViewBag.id_puesto = new SelectList(db.Puesto, "id_puesto", "nombre_puesto");
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //   public ActionResult Create([Bind(Include = "Id_empleado,nombre_empleado,apellido_p,apellido_m,fecha_nac,estatus,salario,email,telefono,id_direccion,id_departamento,id_puesto,id_usuario,rfc,rol,nombre_usuario,contrasenia")] Empleado empleado)
        public ActionResult Create(string nombre_empleado, string apellido_p, string apellido_m, string fecha_nac, string estatus, string salario, string email, string telefono, string estado, string municipio, string ciudad, string codigo_postal, string calle_externa, string num_calle_externa, string calle_interna, int num_calle_interna, string ref1, string ref2, string pais, string id_departamento, string id_puesto, string rfc, string rol, string nombre_usuario, string contrasenia) {

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

            Empleado empleado = new Empleado();
            int id = 1;
                id = db.Direccion.Max(o=>o.id_direccion);
         

            DateTime fechaNac = DateTime.Parse(fecha_nac.ToString());
            decimal salarioE = Decimal.Parse(salario.ToString());
            int id_departamentoE = Int32.Parse(id_departamento.ToString());
            int id_puestoE = Int32.Parse(id_departamento.ToString());
            empleado.nombre_empleado = nombre_empleado;
            empleado.apellido_m = apellido_m;
            empleado.apellido_p = apellido_p;
            empleado.fecha_nac = fechaNac;
            empleado.telefono = telefono;
            empleado.estatus = estatus;
            empleado.salario = salarioE;
            empleado.email = email;
            empleado.rfc = rfc;
            empleado.rol = rol;
            empleado.id_direccion = id;
            empleado.nombre_usuario = nombre_usuario;
            empleado.contrasenia = contrasenia;
            empleado.id_departamento = id_departamentoE;
            empleado.id_puesto = id_puestoE;

            if (ModelState.IsValid)
            {
                db.Empleado.Add(empleado);
                db.Direccion.Add(direccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "nombre_departamento", empleado.id_departamento);
            ViewBag.id_direccion = new SelectList(db.Direccion, "id_direccion", "estado", empleado.id_direccion);
            ViewBag.id_puesto = new SelectList(db.Puesto, "id_puesto", "nombre_puesto", empleado.id_puesto);
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_empleado,nombre_empleado,apellido_p,apellido_m,fecha_nac,estatus,salario,email,telefono,id_direccion,id_departamento,id_puesto,id_usuario,rfc,rol,nombre_usuario,contrasenia")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "nombre_departamento", empleado.id_departamento);
            ViewBag.id_direccion = new SelectList(db.Direccion, "id_direccion", "estado", empleado.id_direccion);
            ViewBag.id_puesto = new SelectList(db.Puesto, "id_puesto", "nombre_puesto", empleado.id_puesto);
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            db.Empleado.Remove(empleado);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        //  public ActionResult Create([Bind(Include = "id_cliente,nombre_cliente,apellido_p,apellido_m,id_direccion,telefono,email,id_tarjeta,id_usuario,nombre_usuario,contrasenia")] Cliente cliente)
        //Nombres de la vista tienen que ser los nombres de los paramentros
        public ActionResult Modificar(string nombre_usuario, string nombre_empleado, string apellido_p, string apellido_m, string estado, string municipio, string ciudad, string codigo_postal, string telefono, string calle_externa, int num_calle_externa, string calle_interna, int num_calle_interna, string ref1, string ref2, string pais, string fecha_nac)
        {
            string email2 = Session["correo"].ToString();
            var query = db.Empleado.FirstOrDefault(x => x.email == email2);

            int numCallExterna = Int32.Parse(num_calle_externa.ToString());
            int numCalleInterna = Int32.Parse(num_calle_interna.ToString());

            Direccion direccion;
           
                var query2 = db.Direccion.FirstOrDefault(x => x.id_direccion == query.id_direccion);
                direccion = query2;
            


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
      
                db.SaveChanges();
                id = (int)query.id_direccion;
            



            Empleado cliente = db.Empleado.FirstOrDefault(x => x.email == email2);

            DateTime fecha_nacC = DateTime.Parse(fecha_nac.ToString());
            cliente.nombre_empleado = nombre_empleado;
            cliente.apellido_p = apellido_p;
            cliente.apellido_m = apellido_m;
            cliente.id_direccion = id;
            cliente.telefono = telefono;
            cliente.nombre_usuario = nombre_usuario;
            cliente.fecha_nac = fecha_nacC;
            cliente.email = email2;
            cliente.id_direccion = id;
            db.SaveChanges();



            string[] nombre = cliente.nombre_empleado.ToString().Split(' ');
            Session["nombre"] = nombre[0];
            Session["usuario"] = nombre_usuario;
            return RedirectToAction("Ver", "Cuenta");

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
