using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TiendaMotores.Models;

namespace TiendaMotores.Controllers
{
    [Authorize]
    public class MetodoPagoController : Controller
    {
        private ContextoTienda db = new ContextoTienda();
        private string NumConfirPago;
        // GET: Checkout

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CrearOrden()
        {

            return View();

        }
        public ActionResult HacerCompra()
        {

            if (!User.Identity.IsAuthenticated)
            {
                Session["CrearOrden"] = "pend";
                return RedirectToAction("Login", "Account");
            }
            string correo = User.Identity.Name;

            DateTime fechCreacion = DateTime.Today;
            DateTime fechaProbEntrega = DateTime.Today.AddDays(3);
            var cliente = (from c in db.Cliente
                           where c.email == correo
                           select c);

            if (cliente.Count() > 0)
            {
                Cliente cliente2 = cliente.FirstOrDefault();
                Session["dirCliente"] = cliente2.id_direccion;
                Session["fechaOrden"] = fechCreacion;
                Session["fechaEntrga"] = fechaProbEntrega;
                //Solicitar si hay tarjeta

                if (cliente2.id_tarjeta != null)
                {
                    var tarjeta = (from c in db.Tarjeta
                                   where c.id_tarjeta == cliente2.id_tarjeta
                                   select c).FirstOrDefault();

                    //Identificar el numero de tarjeta
                    if (tarjeta.numTarjeta.ToString().StartsWith("4"))
                    {
                        Session["tTarj"] = "1";
                    }
                    if (tarjeta.numTarjeta.ToString().StartsWith("5"))
                    {
                        Session["tTarj"] = "2";
                    }
                    if (tarjeta.numTarjeta.ToString().StartsWith("3"))
                    {
                        Session["tTarj"] = "3";
                    }
                    Session["nTarj"] = tarjeta.numTarjeta;


                }
                else
                {
                    //Solicitar la tarjeta
                }
            }
            else
            {
                //Solicitar el registro de todo

            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagarCon(string tipoPago)
        {
            string correo = User.Identity.Name;
            DateTime fechaCreacion = DateTime.Today;
            DateTime fechaProbEntrega = fechaCreacion.AddDays(3);
            var cliente = (from c in db.Cliente
                           where c.email == correo
                           select c).ToList().FirstOrDefault();

            int idD=Int32.Parse( Session["idDir"].ToString() );
            int idC= Int32.Parse(Session["idCli"].ToString() );
            if (tipoPago.Equals("Tarjeta"))
            {
                validaPago(cliente);
                return RedirectToActionPermanent("PagoTarjeta", routeValues: new { idC = idC, idD = idD});
            }

            if (tipoPago.Equals("PayPal"))
            {
               
                   validaPago(cliente);
                  return RedirectToActionPermanent("PagoPayPal", routeValues: new { idC=idC, idD=idD});
            }
            return View();
        }

        public ActionResult PagoTarjeta(int idC, int idD)
        {
            Session["idDir"] = idD;
            Session["idCli"] = idC;
            return View();
        }
        public ActionResult PagoPayPal(int idC, int idD)
        {
            Session["idDir"] = idD;
            Session["idCli"] = idC;
            return View();
        }


        public ActionResult PagandoPayPal(int idC, int idD)
        {

            Session["idDir"] = idD;
            Session["idCli"] = idC;
            return View();
        }

        public ActionResult PagoNoAceptado()
        {
            return View();
        }
        public ActionResult PagoAceptado(int idC, int idD)
        {
            Oden_cliente orde = new Oden_cliente();
            int id = 0;
            if (!(db.Oden_cliente.Max(o => (int?)o.Id_orden) == null))
            {
                id = db.Oden_cliente.Max(o => o.Id_orden);
            }
            else
            {
                id = 0;

            }
            id++;

            orde.Id_orden = id;
            orde.fecha_creacion = DateTime.Today;
            orde.num_comfirmacion = Convert.ToInt32(Session["nConfirma"].ToString());
            var carro = Session["cart"] as List<Item>;
            var total = carro.Sum(item => item.Producto.precio * item.Cantidad);
            orde.total = (double)Convert.ToDecimal(total.ToString());
            //id_cliente
            //id_dirEntrega

            db.Oden_cliente.Add(orde);
            db.SaveChanges();
            Detalle_compra compra;

            List<Detalle_compra> listaProd = new List<Detalle_compra>();
            foreach (Item linea in carro)
            {
                compra = new Detalle_compra();
                compra.Id_compra = orde.Id_orden;
                compra.id_producto = linea.Producto.Id_producto;
                compra.cantidad = linea.Cantidad;
                db.Detalle_compra.Add(compra);
            }
            db.SaveChanges();

            Session["cart"] = null;
            Session["nConfirma"] = null;
            Session["itemTotal"] = 0;
            return View();

        }
        private bool validaPago(Cliente cliente)
        {
            bool retorna = true;
            //Se debe comunicar con el sistema de pago enviando los datos de la tarjeta
            //Y el sistema de pago nos regresa un numero de confirmacion, vamos a simularlo usando un numero random pero seguro
            int randomValue;
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] val = new byte[6];
                crypto.GetBytes(val);
                randomValue = BitConverter.ToInt32(val, 1);
            }


            NumConfirPago = Math.Abs(randomValue * 1000).ToString();
            Session["nConfirma"] = NumConfirPago;
            return retorna;
        }
       

    }
}