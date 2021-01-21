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
    public class ProductoController : Controller
    {
        private ContextoTienda db = new ContextoTienda();

        // GET: Producto
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Categoria).Include(p => p.Galeria).Include(p => p.marca).Include(p => p.Producto2);
            return View(producto.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre_categoria");
            ViewBag.id_galeria = new SelectList(db.Galeria, "id_galeria", "nombre_galeria");
            ViewBag.id_marca = new SelectList(db.marca, "Id_marca", "nombre_marca");
            ViewBag.id_producto_relacionado = new SelectList(db.Producto, "Id_producto", "nombre_producto");

            return View();
        }

        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Create([Bind(Include = "Id_producto,nombre_producto,descripcion_producto,precio,coste,imagen_producto,id_categoria,ultima_actualizacion,id_marca,id_producto_relacionado,clave_unica,id_galeria,id_usuario")] Producto producto)
        public ActionResult Create(string nombre_producto, string descripcion_producto, string precio, string coste, string imagen_producto, string id_categoria, string ultima_actualizacion, string id_marca, string id_producto_relacionado, string clave_unica, string nombre_gal1, string nombre_gal2, string nombre_gal3, string nombre_gal4, string email_usuario, string potencia, string Tipo, string ancho, string alto, string profundidad, string peso, string polos, string rpm, string voltaje, string cantidad, string estatus)
        {
            Galeria galeria = new Galeria();
            // int? idGaleria =db.Galeria.Max(g=>(int?)g.id_galeria);
            //int ideF=(int)idGaleria;
            int ideF = 0;
            if (!(db.Galeria.Max(o => (int?)o.id_galeria) == null))
            {
                ideF = db.Galeria.Max(o => o.id_galeria);
            }
            else
            {
                ideF = 0;
            }
            ideF++;
            galeria.id_galeria = ideF;
            galeria.nombre_gal1 = nombre_gal1;
            galeria.nombre_gal2 = nombre_gal2;
            galeria.nombre_gal3 = nombre_gal3;
            galeria.nombre_gal4 = nombre_gal4;
            db.Galeria.Add(galeria);
            db.SaveChanges();

            decimal precioP = Decimal.Parse(precio.ToString());
            decimal costeP = Decimal.Parse(coste.ToString());
            int id_categoriaP = Int32.Parse(id_categoria.ToString());
            decimal anchoP = Decimal.Parse(ancho.ToString());
            int id_marcaP = Int32.Parse(id_marca.ToString());
            decimal altoP = Decimal.Parse(alto.ToString());
            decimal profundidadP = Decimal.Parse(profundidad.ToString());
            int id_producto_relacionadoP = 1;
            DateTime ultimaActualizacions;
            int clave_unicaP = Int32.Parse(clave_unica.ToString());
            decimal pesoP = Decimal.Parse(peso.ToString());
            decimal voltajeP = Decimal.Parse(voltaje.ToString());


            ultimaActualizacions = System.DateTime.Now;
            email_usuario = Session["correo"].ToString();
            int id = 0;
            id = db.Galeria.Max(g => g.id_galeria);
            Producto producto = new Producto();
            producto.nombre_producto = nombre_producto;
            producto.descripcion_producto = descripcion_producto;
            producto.precio = precioP;
            producto.coste = costeP;
            producto.imagen_producto = imagen_producto;
            producto.id_categoria = id_categoriaP;
            producto.ultima_actualizacion = DateTime.Parse(ultimaActualizacions.ToString());
            producto.id_marca = id_marcaP;
            producto.id_producto_relacionado = id_producto_relacionadoP;
            producto.clave_unica = clave_unicaP;
            producto.id_galeria = id;
            producto.email_usuario = email_usuario;
            producto.potencia = potencia;
            producto.Tipo = Tipo;
            producto.ancho = anchoP;
            producto.alto = altoP;
            producto.profundidad = profundidadP;
            producto.peso = pesoP;
            producto.polos = polos;
            producto.rpm = rpm;
            producto.voltaje = voltajeP;

                db.Producto.Add(producto);
                db.SaveChanges();

                int idProd = db.Producto.Max(p=>p.Id_producto);

                int cantidadP = Int32.Parse(cantidad.ToString());
                Inventario inventario = new Inventario();
            inventario.Id_producto = idProd;
            inventario.cantidad_inventario = cantidadP;
                inventario.estatus = estatus;
               
                db.Inventario.Add(inventario);
                db.SaveChanges();


            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre_categoria", producto.id_categoria);
            ViewBag.id_galeria = new SelectList(db.Galeria, "id_galeria", "nombre_galeria", producto.id_galeria);
            ViewBag.id_marca = new SelectList(db.marca, "Id_marca", "nombre_marca", producto.id_marca);
            ViewBag.id_producto_relacionado = new SelectList(db.Producto, "Id_producto", "nombre_producto", producto.id_producto_relacionado);


            return RedirectToAction("Index");
        }


        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre_categoria", producto.id_categoria);
            ViewBag.id_galeria = new SelectList(db.Galeria, "id_galeria", "nombre_galeria", producto.id_galeria);
            ViewBag.id_marca = new SelectList(db.marca, "Id_marca", "nombre_marca", producto.id_marca);
            ViewBag.id_producto_relacionado = new SelectList(db.Producto, "Id_producto", "nombre_producto", producto.id_producto_relacionado);
           
            return View(producto);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string nombre_producto, string descripcion_producto, string precio, string coste, string imagen_producto, string id_categoria, string ultima_actualizacion, string id_marca, string id_producto_relacionado, string clave_unica, string nombre_gal1, string nombre_gal2, string nombre_gal3, string nombre_gal4, string email_usuario, string potencia, string Tipo, string ancho, string alto, string profundidad, string peso, string polos, string rpm, string voltaje, string cantidad, string estatus)
        {
            Galeria galeria = new Galeria();
            // int? idGaleria =db.Galeria.Max(g=>(int?)g.id_galeria);
            //int ideF=(int)idGaleria;
            int ideF = 0;
            if (!(db.Galeria.Max(o => (int?)o.id_galeria) == null))
            {
                ideF = db.Galeria.Max(o => o.id_galeria);
            }
            else
            {
                ideF = 0;
            }
            ideF++;
            galeria.id_galeria = ideF;
            galeria.nombre_gal1 = nombre_gal1;
            galeria.nombre_gal2 = nombre_gal2;
            galeria.nombre_gal3 = nombre_gal3;
            galeria.nombre_gal4 = nombre_gal4;
            db.Galeria.Add(galeria);
            db.SaveChanges();

            decimal precioP = Decimal.Parse(precio.ToString());
            decimal costeP = Decimal.Parse(coste.ToString());
            int id_categoriaP = Int32.Parse(id_categoria.ToString());
            decimal anchoP = Decimal.Parse(ancho.ToString());
            int id_marcaP = Int32.Parse(id_marca.ToString());
            decimal altoP = Decimal.Parse(alto.ToString());
            decimal profundidadP = Decimal.Parse(profundidad.ToString());
            int id_producto_relacionadoP = 1;
            DateTime ultimaActualizacions;
            int clave_unicaP = Int32.Parse(clave_unica.ToString());
            decimal pesoP = Decimal.Parse(peso.ToString());
            decimal voltajeP = Decimal.Parse(voltaje.ToString());


            ultimaActualizacions = System.DateTime.Now;
            email_usuario = Session["correo"].ToString();
            int id = 0;
            id = db.Galeria.Max(g => g.id_galeria);
            Producto producto = new Producto();
            producto.nombre_producto = nombre_producto;
            producto.descripcion_producto = descripcion_producto;
            producto.precio = precioP;
            producto.coste = costeP;
            producto.imagen_producto = imagen_producto;
            producto.id_categoria = id_categoriaP;
            producto.ultima_actualizacion = DateTime.Parse(ultimaActualizacions.ToString());
            producto.id_marca = id_marcaP;
            producto.id_producto_relacionado = id_producto_relacionadoP;
            producto.clave_unica = clave_unicaP;
            producto.id_galeria = id;
            producto.email_usuario = email_usuario;
            producto.potencia = potencia;
            producto.Tipo = Tipo;
            producto.ancho = anchoP;
            producto.alto = altoP;
            producto.profundidad = profundidadP;
            producto.peso = pesoP;
            producto.polos = polos;
            producto.rpm = rpm;
            producto.voltaje = voltajeP;

            db.Producto.Add(producto);
            db.SaveChanges();

            int idProd = db.Producto.Max(p => p.Id_producto);

            int cantidadP = Int32.Parse(cantidad.ToString());
            Inventario inventario = new Inventario();
            inventario.Id_producto = idProd;
            inventario.cantidad_inventario = cantidadP;
            inventario.estatus = estatus;

            db.Inventario.Add(inventario);
            db.SaveChanges();


            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre_categoria", producto.id_categoria);
            ViewBag.id_galeria = new SelectList(db.Galeria, "id_galeria", "nombre_galeria", producto.id_galeria);
            ViewBag.id_marca = new SelectList(db.marca, "Id_marca", "nombre_marca", producto.id_marca);
            ViewBag.id_producto_relacionado = new SelectList(db.Producto, "Id_producto", "nombre_producto", producto.id_producto_relacionado);


            return RedirectToAction("Index");
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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


        [AllowAnonymous]
        public ActionResult DetalleProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.DetalleProducto=producto;
            return View();
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
