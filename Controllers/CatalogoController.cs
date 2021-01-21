using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaMotores.Models;

namespace TiendaMotores.Controllers
{
    public class CatalogoController : Controller
    {
        private ContextoTienda db = new ContextoTienda();
        // GET: Catalogo
        public ActionResult Catalogo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscaProd(string nomBuscar)
        {
            ViewBag.SearchKey = nomBuscar;
            using (db)
            {
                var query = from st in db.Producto
                            where st.nombre_producto.Contains(nomBuscar)
                            select st;

                var listProd = query.ToList();
                ViewBag.Listado = listProd;
            }

            return View();
        }
        public ActionResult prodCategoria(int id,string nombre, string descripcion)
        {
            List<Producto> mercancia = null;
            var query = from p in db.Producto
                        where p.id_categoria == id
                        select p;
          
                List<Producto> motoresE1 = query.ToList();
                mercancia = motoresE1;
        
            ViewBag.cat =nombre;
            ViewBag.desc = descripcion;
            ViewBag.productos = mercancia;
            return View();
        }
    }
}