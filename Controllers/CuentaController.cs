using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaMotores.Models;
namespace TiendaMotores.Controllers
{
    [Authorize]
    public class CuentaController : Controller
    {

        private TIENDAEntities db = new TIENDAEntities();
        // GET: Cuenta
        public ActionResult CuentaCliente()
        {
            
            return View();
        }

        // GET: Cuenta
        public ActionResult CuentaEmpleado()
        {
            return View();
        }
        public ActionResult Ver()
        {

            if (Session["rol"].Equals("Cliente"))
            {
                return RedirectToAction("CuentaCliente");
            }
            else
            {
                return RedirectToAction("CuentaEmpleado");
            }
            return View();
        }
        public ActionResult Modificar()
        {
            if (Session["rol"].Equals("Cliente"))
            {
                return RedirectToAction("CuentaCliente");
            }
            else
            {
                return RedirectToAction("CuentaEmpleado");
            }
            return View();
        }
        public ActionResult VerPedidos()
        {
            List<Compra> compra;

            List<PedidoCliente> pedidos = new List<PedidoCliente>();
            PedidoCliente pedido;
            List<Detalle_compra> detalle_compra;
            List<ItemPedido> itemPed = new List<ItemPedido>();

            ItemPedido iPed;

            string correo = User.Identity.Name;
            Cliente cl = (from c in db.Cliente
                          where c.email==correo
                          select c).ToList().FirstOrDefault();
            if (cl != null)
            {
                var query = from o in db.Compra
                            where o.id_cliente == cl.id_cliente
                            orderby o.fecha_compra ascending
                            select o;
                var comprass = query.FirstOrDefault();
                compra = query.ToList();


                var queryOrden = (from k in db.Oden_cliente
                                  where k.id_compra == comprass.id_compra
                                  select k).FirstOrDefault();

                foreach (Compra c in compra)
                {
                    pedido = new PedidoCliente();
                    pedido.compra = c;
                    pedido.fecha = c.fecha_compra.ToString();
                    if (queryOrden.fecha_envio.HasValue)
                    {
                        pedido.envio = queryOrden.fecha_envio.GetValueOrDefault().ToShortDateString();
                    }
                    else
                    {
                        pedido.envio = "Proximamente";
                    }
                    if (queryOrden.fecha_entrega.HasValue)
                    {
                        pedido.estatus = queryOrden.fecha_entrega.GetValueOrDefault().ToShortDateString();
                    }
                    else
                    {
                        pedido.estatus = "Sin entregar";
                    }
                    pedido.total = queryOrden.total.ToString();
                    pedidos.Add(pedido);
                    detalle_compra = (from oP in db.Detalle_compra
                                      where oP.Id_compra == c.id_compra
                                      select oP).ToList();
                    pedido.compraProductos = detalle_compra;
                    foreach (Detalle_compra op in detalle_compra)
                    {
                        iPed = new ItemPedido();
                        iPed.idOrd = op.Id_compra;
                        iPed.product = db.Producto.First(p => p.Id_producto == op.id_producto);
                        iPed.Cantidad = op.cantidad;
                        itemPed.Add(iPed);
                    }
                }
            }
            else
            {

            }
          
            Session["misPedidos"] = pedidos;
            Session["Pedido"] = itemPed;

            return View();
        }
        public ActionResult VerDirecciones()
        {
            if (Session["rol"].Equals("Cliente"))
            {
                RedirectToAction("CuentaCliente");
            }
            else
            {
                RedirectToAction("CuentaEmpleado");
            }
            return View();
        }
     
    }
}