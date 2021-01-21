using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TiendaMotores.Models;

namespace TiendaMotores.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private ContextoTienda db = new ContextoTienda();

        // GET: Clientes
        public ActionResult Index()
        {
            var cliente = db.Cliente.Include(c => c.Direccion).Include(c => c.Tarjeta);
            return View(cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.id_direccion = new SelectList(db.Direccion, "id_direccion", "estado");
            ViewBag.id_tarjeta = new SelectList(db.Tarjeta, "id_tarjeta", "numTarjeta");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //  public ActionResult Create([Bind(Include = "id_cliente,nombre_cliente,apellido_p,apellido_m,id_direccion,telefono,email,id_tarjeta,id_usuario,nombre_usuario,contrasenia")] Cliente cliente)
        //Nombres de la vista tienen que ser los nombres de los paramentros
        public ActionResult Create(string nombre_cliente, string apellido_p, string apellido_m, string estado, string municipio, string ciudad, string codigo_postal, string telefono, string calle_externa, int num_calle_externa, string calle_interna, int num_calle_interna, string ref1, string ref2, string pais, string email, string numTarjeta, string mes_vencimiento, string anio_vencimiento, string nombre_titular, int tipo_tarjeta,string estado_tarj, string municipio_tarj, string ciudad_tarj, string codigo_postal_tarj, string calle_externa_tarj, int num_calle_externa_tarj, string calle_interna_tarj, int num_calle_interna_tarj, string ref1_tarj, string ref2_tarj, string pais_tarj, string cvv, string nombre_usuario, string contrasenia, string fecha_nacimiento, int usarMismaDireccion)
        {
            using(db)
            {
                var query = from p in db.Cliente
                            where (p.nombre_cliente==nombre_cliente) && (p.apellido_p==apellido_p) && (p.apellido_m==apellido_m)
                            select p;
                if (query.Count() > 0)
                {
                    return RedirectToAction("Modificar", routeValues: new { nombre_cliente, apellido_p, apellido_m, estado, municipio, ciudad, codigo_postal, telefono, calle_externa, num_calle_externa, calle_interna, num_calle_interna, ref1, ref2, pais, email, numTarjeta, mes_vencimiento, anio_vencimiento, nombre_titular, tipo_tarjeta, estado_tarj, municipio_tarj, ciudad_tarj, codigo_postal_tarj, calle_externa_tarj, num_calle_externa_tarj, calle_interna_tarj, num_calle_interna_tarj, ref1_tarj, ref2_tarj, pais_tarj, cvv, nombre_usuario, contrasenia, fecha_nacimiento, usarMismaDireccion});

                }
            }
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

            db.Direccion.Add(direccion);
            db.SaveChanges();
            int id = 1;
            id = db.Direccion.Max(o => o.id_direccion);

            int idTarj = 1;
            //Si es 1 entonces se usa la misma direccion
            //Sino se crea otra para la tarjeta
            if (usarMismaDireccion==1)
            {
            }
            else
            {
                int numCallExternaTarj = Int32.Parse(num_calle_externa_tarj.ToString());
                int numCalleInternaTarj = Int32.Parse(num_calle_interna_tarj.ToString());
                Direccion direccionTarj = new Direccion();

                direccionTarj.estado = estado_tarj;
                direccionTarj.municipio = municipio_tarj;
                direccionTarj.ciudad = ciudad_tarj;
                direccionTarj.codigo_postal = codigo_postal_tarj;
                direccionTarj.telefono = telefono;
                direccionTarj.calle_externa = calle_externa_tarj;
                direccionTarj.num_calle_externa = numCallExternaTarj;
                direccionTarj.calle_interna = calle_interna_tarj;
                direccionTarj.num_calle_interna = numCalleInternaTarj;
                direccionTarj.ref1 = ref1_tarj;
                direccionTarj.ref2 = ref2_tarj;
                direccionTarj.pais = pais_tarj;

                db.Direccion.Add(direccion);
                db.SaveChanges();
            }
            
            Tarjeta tarj = new Tarjeta();

            tarj.numTarjeta = numTarjeta;
            tarj.mes_vencimiento = mes_vencimiento;
            tarj.anio_vencimiento = anio_vencimiento;
            tarj.nombre_titular = nombre_titular;
            tarj.tipo_tarjeta = tipo_tarjeta;
            if (usarMismaDireccion == 1)
            {
                tarj.id_direccion = id;
            }
            else
            {
                idTarj = db.Direccion.Max(o => o.id_direccion);
                tarj.id_direccion = idTarj;
            }

            db.Direccion.Add(direccion);
            db.Tarjeta.Add(tarj);
            db.SaveChanges();
            int idTarjCliente= 1;
            idTarjCliente= db.Tarjeta.Max(o => o.id_tarjeta);
            

            Cliente cliente = new Cliente();

            string tipoT = String.Format(tipo_tarjeta.ToString());
             if(Tarjeta(numTarjeta, tipoT,mes_vencimiento, anio_vencimiento, cvv)){

                if (validaPago(nombre_titular, pais_tarj, codigo_postal_tarj, ciudad_tarj, numTarjeta, anio_vencimiento, mes_vencimiento, cvv)) {

                    DateTime fecha_nacC = DateTime.Parse(fecha_nacimiento.ToString());
                    cliente.nombre_cliente = nombre_cliente;
                    cliente.apellido_p = apellido_p;
                    cliente.apellido_m = apellido_m;
                    cliente.id_direccion = id;
                    cliente.telefono = telefono;
                    cliente.id_tarjeta = idTarjCliente;
                    cliente.nombre_usuario = nombre_usuario;
                    cliente.contrasenia = contrasenia;
                    cliente.fecha_nacimiento = fecha_nacC;
                      cliente.email=Session["correo"].ToString();

                    db.Cliente.Add(cliente);
                    db.SaveChanges();



                    string[] nombre = cliente.nombre_cliente.ToString().Split(' ');
                    Session["nombre"] = nombre[0];
                    Session["usuario"] = nombre_usuario;

                    if (Session["CrearOrden"] != null)
                    {
                        if (Session["CrearOrden"].Equals("pend"))
                        {
                            return RedirectToAction("CrearOrden","MetodoPago");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
                else
                {
                    return RedirectToAction("Invalida");
                }
            }
            else
            {
                return RedirectToAction("Invalida");
            }
            return View(cliente);
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //  public ActionResult Create([Bind(Include = "id_cliente,nombre_cliente,apellido_p,apellido_m,id_direccion,telefono,email,id_tarjeta,id_usuario,nombre_usuario,contrasenia")] Cliente cliente)
        //Nombres de la vista tienen que ser los nombres de los paramentros
        public ActionResult Modificar(string nombre_cliente, string apellido_p, string apellido_m, string estado, string municipio, string ciudad, string codigo_postal, string telefono, string calle_externa, int num_calle_externa, string calle_interna, int num_calle_interna, string ref1, string ref2, string pais, string email, string numTarjeta, string mes_vencimiento, string anio_vencimiento, string nombre_titular, int tipo_tarjeta, string estado_tarj, string municipio_tarj, string ciudad_tarj, string codigo_postal_tarj, string calle_externa_tarj, int num_calle_externa_tarj, string calle_interna_tarj, int num_calle_interna_tarj, string ref1_tarj, string ref2_tarj, string pais_tarj, string cvv, string nombre_usuario, string contrasenia, string fecha_nacimiento, int usarMismaDireccion)
        {
            int idCliente = 0;
            int idDir = 0;
            int idTarj = 0;
            using (db)
            {
                //Mdofica todo lo del cliente primero

                var queryCliente = from p in db.Cliente
                            where (p.nombre_cliente == nombre_cliente) && (p.apellido_p == apellido_p) && (p.apellido_m == apellido_m)
                            select p;
                idCliente = queryCliente.First().id_cliente;

                var cliente = db.Cliente.FirstOrDefault(c => c.id_cliente == idCliente);
                DateTime fecha_nacC = DateTime.Parse(fecha_nacimiento.ToString());
                cliente.nombre_cliente = nombre_cliente;
                cliente.apellido_p = apellido_p;
                cliente.apellido_m = apellido_m;
                cliente.telefono = telefono;
                cliente.nombre_usuario = nombre_usuario;
                cliente.contrasenia = contrasenia;
                cliente.fecha_nacimiento = fecha_nacC;
                cliente.email = Session["correo"].ToString();
                db.SaveChanges();



                idDir = (int)cliente.id_direccion;
                idTarj= (int)cliente.id_tarjeta;
                if (idDir!=0)
                {
                    idDir = Int32.Parse(idDir.ToString());
                    int numCallExterna = Int32.Parse(num_calle_externa.ToString());
                    int numCalleInterna = Int32.Parse(num_calle_interna.ToString());
                    
                    Direccion direccion = new Direccion();
                    //direccion =idDir;
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

                }
                else
                {
                    //Crea la direccion
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

                    db.Direccion.Add(direccion);
                    db.SaveChanges();
                }

              //  if (idTarjeta != null)
                {

                }
             //   else
                {
                    //Crea la tarjeta

                    string tipoT = String.Format(tipo_tarjeta.ToString());
                    if (Tarjeta(numTarjeta, tipoT, mes_vencimiento, anio_vencimiento, cvv))
                    {

                        if (validaPago(nombre_titular, pais_tarj, codigo_postal_tarj, ciudad_tarj, numTarjeta, anio_vencimiento, mes_vencimiento, cvv))
                        {


                            Tarjeta tarj = new Tarjeta();

                            tarj.numTarjeta = numTarjeta;
                            tarj.mes_vencimiento = mes_vencimiento;
                            tarj.anio_vencimiento = anio_vencimiento;
                            tarj.nombre_titular = nombre_titular;
                            tarj.tipo_tarjeta = tipo_tarjeta;
                            if (usarMismaDireccion == 1)
                            {
                           //     tarj.id_direccion = id;
                            }
                            else
                            {
                                idTarj = db.Direccion.Max(o => o.id_direccion);
                                tarj.id_direccion = idTarj;
                            }


                            string[] nombre = cliente.nombre_cliente.ToString().Split(' ');
                            Session["nombre"] = nombre[0];
                            Session["usuario"] = nombre_usuario;

                            if (Session["CrearOrden"] != null)
                            {
                                if (Session["CrearOrden"].Equals("pend"))
                                {
                                    return RedirectToAction("CrearOrden", "MetodoPago");
                                }
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Invalida");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Invalida");
                    }

                    if (usarMismaDireccion==1)
                    {

                    }
                    else
                    {
                        
                    }
                }

            }
            return View();

        }


















        private bool Tarjeta(string tarj, string tipo, string mes, string anio, string cvv)
        {
            //llamar al metodo Luhn
            bool retorna = validaTarjeta(tarj);
            if (retorna)
            {
                if ((tarj.StartsWith("4") && (tipo.Equals("Visa"))))
                {
                    retorna = true;
                }
                else
                {
                    if ((tarj.StartsWith("5") && (tipo.Equals("Masterd"))))
                    {
                        retorna = true;
                    }
                    else
                    {
                        if ((tarj.StartsWith("3") && (tipo.Equals("American"))))
                        {
                            retorna = true;
                        }
                        else
                        {
                            retorna = false;
                        }
                    }
                }
            }
            DateTime hoy = new DateTime();
            if (Convert.ToInt32(anio) >= hoy.Year)
            {
                if (Convert.ToInt32(mes)>hoy.Month)
                {
                    retorna=true;
                }
                else
                {
                    retorna = false;
                }
            }
            else
            {
                retorna = false;
            }

            return retorna;
        }
        private bool validaTarjeta(string tarj)
        {
            bool retorna = true;
            StringBuilder digitsOnly = new StringBuilder();
            foreach (Char c in tarj)
            {
                if(Char.IsDigit(c)) digitsOnly.Append(c);
            };
            if (digitsOnly.Length > 18 || digitsOnly.Length < 15)
                return false;

            int sum = 0;
            int digit = 0;
            int addend = 0;
            bool timesTwo = false;

            for ( int i=digitsOnly.Length-1;i>=0;i--)
            {
                digit = Int32.Parse(digitsOnly.ToString(i,1));
                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                    {
                        addend -= 9;
                    }
                }
                else
                {
                    addend = digit;
                }
                sum += addend;
                timesTwo = !timesTwo;
            }
            retorna = ((sum % 10) == 0);

            return retorna;
        }
        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_direccion = new SelectList(db.Direccion, "id_direccion", "estado", cliente.id_direccion);
            ViewBag.id_tarjeta = new SelectList(db.Tarjeta, "id_tarjeta", "numTarjeta", cliente.id_tarjeta);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,nombre_cliente,apellido_p,apellido_m,id_direccion,telefono,email,id_tarjeta,id_usuario,nombre_usuario,contrasenia")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_direccion = new SelectList(db.Direccion, "id_direccion", "estado", cliente.id_direccion);
            ViewBag.id_tarjeta = new SelectList(db.Tarjeta, "id_tarjeta", "numTarjeta", cliente.id_tarjeta);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
        public ActionResult Invalida()
        {
            return View();
        }
        public ActionResult BorraUser()
        {
            string idUser = User.Identity.Name;

            return RedirectToAction("Delete", "Account", routeValues: new { id = idUser });
        }
        private bool validaPago(string nombre_titular, string pais_tarj, string codigo_postal_tajr, string ciudad_tarj, string numTarjeta, string anio_vencimiento, string mes_vencimiento, string cvv)
        {
            bool retorna = true;
            //Se debe comunicar con el sistema de pago enviando los datos de la tarjeta
            //y los datos del cliente
            return retorna;
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
