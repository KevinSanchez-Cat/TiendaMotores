using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaMotores.Models;

namespace TiendaMotores.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Agregar(int id, int cantidad)
        {
            //Cantidad agregar otro parametro 
            ProdCarro carro = new ProdCarro();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                Producto p = carro.find(id);
                string nam = p.nombre_producto;
                cart.Add(new Item { Producto = carro.find(id), Cantidad = cantidad });
                Session["cart"] = cart;
                Session["itemTotal"] = cantidad;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Cantidad+=cantidad;
                    Session["itemTotal"] = ((int)Session["itemTotal"]) + cantidad;
                }
                else
                {
                    Producto p = carro.find(id);
                    string nam = p.nombre_producto;
                    cart.Add(new Item { Producto = carro.find(id), Cantidad = cantidad });
                    Session["itemTotal"] = ((int)Session["itemTotal"])+cantidad;
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Producto.Id_producto.Equals(id))
                {
                    return i;
                }

            }
            return -1;
        }
        public ActionResult Quitar(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            int quit = cart[index].Cantidad;
            cart.RemoveAt(index);
            Session["cart"] = cart;
            int items =(int) Session["itemTotal"];
            Session["itemTotal"] = items - quit;
            return RedirectToAction("Index");
        }
    }
}