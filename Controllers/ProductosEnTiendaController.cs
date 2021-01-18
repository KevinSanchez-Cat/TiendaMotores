using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaMotores.Models;
namespace TiendaMotores.Controllers
{
    public class ProductosEnTiendaController : Controller
    {
        private TIENDAEntities db =new TIENDAEntities();
        // GET: ProductosEnTienda
        public ActionResult Index()
        {

            List<Producto> productos = null;
            var query = from p in db.Producto
                        orderby p.nombre_producto, p.precio
                        select p;

            productos = query.ToList();


            ViewBag.productoTienda = productos;


            return View();
        }
    }
}