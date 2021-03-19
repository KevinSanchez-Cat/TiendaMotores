using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HacerCompraConTarjeta(string nombre_titular,string numero_tarjeta, string mes_vencimiento, string anio_vencimiento, string cvv, string municipio, string codigo_postal, string calle_externa, string num_calle_externa,  string num_calle_interna, string pais)
        {
            string tipoTarjeta = "";
            if (!User.Identity.IsAuthenticated)
            {
                Session["CrearOrden"] = "pend";
                return RedirectToAction("Login", "Account");
            }
            string correo = User.Identity.Name;

           
           
            var cliente = (from c in db.Cliente
                           where c.email == correo
                           select c);

                Cliente cliente2 = cliente.FirstOrDefault();

               
                

                    if (numero_tarjeta.StartsWith("4"))
                    {
                        tipoTarjeta = "1";
                    }
                    if (numero_tarjeta.StartsWith("5"))
                    {
                        tipoTarjeta = "2";
                    }
                    if (numero_tarjeta.ToString().StartsWith("3"))
                    {
                        tipoTarjeta = "3";
                    }

                    if (tarjeta(numero_tarjeta, tipoTarjeta, mes_vencimiento, anio_vencimiento, cvv))
                    {

                    if (!validaPago())
                    {
                            return RedirectToAction("PagoNoAceptado");
                    }
                    else
                    {
                    int numCalleE = Int32.Parse(num_calle_externa);
                    int numCalleI = Int32.Parse(num_calle_interna);
                    Direccion dir = new Direccion();
                    dir.pais = pais;
                    dir.estado = "";
                    dir.ciudad = "";
                    dir.municipio = municipio;
                    dir.calle_externa = calle_externa;
                    dir.calle_interna = "";
                    dir.codigo_postal = codigo_postal;
                    dir.num_calle_externa = numCalleE;
                    dir.num_calle_interna = numCalleI;
                    dir.telefono = "";
                    dir.ref1 = "";
                    dir.ref2 = "";
                    db.Direccion.Add(dir);
                    db.SaveChanges();
                    int idDireccionTar = db.Direccion.Max(i=>i.id_direccion);

                    
                    Tarjeta tarjerta = new Tarjeta();
                    tarjerta.nombre_titular = nombre_titular;
                    tarjerta.id_direccion = idDireccionTar;
                    tarjerta.mes_vencimiento = mes_vencimiento;
                    tarjerta.anio_vencimiento = anio_vencimiento;
                    tarjerta.numTarjeta = numero_tarjeta;
                    tarjerta.tipo_tarjeta =Int32.Parse(tipoTarjeta);
                    db.Tarjeta.Add(tarjerta);
                    db.SaveChanges();



                        validaPago();
                        return RedirectToAction("PagoAceptado");
                    }
                    }
                    else
                    {
                        return RedirectToAction("PagoNoAceptado");
                    }
                
            
        }


        public ActionResult HacerCompraConPay()
        {

            validaPago();
            return RedirectToAction("PagoAceptado");
        }

        public ActionResult PagarCon(string tipoPago)
        {
            string correo = User.Identity.Name;
            DateTime fechaCreacion = DateTime.Today;
            DateTime fechaProbEntrega = fechaCreacion.AddDays(3);
            var cliente = (from c in db.Cliente
                           where c.email == correo
                           select c).ToList().FirstOrDefault();

            int idD=(int)Session["idDir"];
            int idC = cliente.id_cliente;
            if (tipoPago.Equals("Tarjeta"))
            {
                validaPago();
                return RedirectToAction("PagoTarjeta", routeValues: new { idC = idC, idD = idD});
            }else if (tipoPago.Equals("PayPal"))
            {
               
                   validaPago();
                  return RedirectToAction("PagoPayPal", routeValues: new { idC=idC, idD=idD});
            }
            return View();
        }

        public ActionResult PagoTarjeta(int idC, int idD)
        {

            return View();
        }
        public ActionResult PagoPayPal(int idC, int idD)
        {

            return View();
        }
        public ActionResult PagandoPayPal()
        {
       
            return View();
        }

        public ActionResult PagoNoAceptado()
        {
            return View();
        }
        public ActionResult PagoAceptado()
        {
            var carro = Session["cart"] as List<Item>;
            var total = carro.Sum(item => item.Producto.precio * item.Cantidad);
            int idCli = (int)Session["idCli"];
            int idDir = (int)Session["idDir"];
            int nConfirmacion = Convert.ToInt32(Session["nConfirma"]);
            Compra compra = new Compra();
            compra.total = total;
            compra.id_cliente = idCli;
            compra.fecha_compra = System.DateTime.Now;
            compra.id_direccion_entrega = idDir;
            db.Compra.Add(compra);
            db.SaveChanges();
            int id = 0;
            if (!(db.Compra.Max(o => (int?)o.id_compra) == null))
            {
                id = db.Compra.Max(o => o.id_compra);
            }
            else
            {
                id = 1;
            }


            Oden_cliente orde = new Oden_cliente();
           
            
            orde.id_compra = id;
            orde.num_comfirmacion = nConfirmacion;
            orde.fecha_creacion = DateTime.Today;
            orde.total = (double)Convert.ToDecimal(total.ToString());
            orde.id_paqueteria = 1;
            orde.fecha_creacion = null;
            orde.fecha_entrega = null;
            db.Oden_cliente.Add(orde);
            db.SaveChanges();


            Detalle_compra compraP;

            List<Detalle_compra> listaProd = new List<Detalle_compra>();
            foreach (Item linea in carro)
            {
                compraP = new Detalle_compra();
                compraP.Id_compra = orde.id_compra;
                compraP.id_producto = linea.Producto.Id_producto;
                compraP.cantidad = linea.Cantidad;
                compraP.subtotal = (linea.Producto.precio * linea.Cantidad);
                db.Detalle_compra.Add(compraP);
            }
            db.SaveChanges();

            Session["cart"] = null;
            Session["nConfirma"] = null;
            Session["itemTotal"] = 0;
            return View();

        }
        private bool validaPago()
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
       
    private bool tarjeta(string tarj, string tipo, string mes, string anio, string cvv)
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
            if (Convert.ToInt32(mes) > hoy.Month)
            {
                retorna = true;
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
            if (Char.IsDigit(c)) digitsOnly.Append(c);
        };
        if (digitsOnly.Length > 18 || digitsOnly.Length < 15)
            return false;

        int sum = 0;
        int digit = 0;
        int addend = 0;
        bool timesTwo = false;

        for (int i = digitsOnly.Length - 1; i >= 0; i--)
        {
            digit = Int32.Parse(digitsOnly.ToString(i, 1));
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

    }


}