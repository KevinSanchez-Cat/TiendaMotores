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
                RedirectToAction("CuentaCliente");
            }
            else
            {
                RedirectToAction("CuentaEmpleado");
            }
            return View();
        }
        public ActionResult Modificar()
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
        public ActionResult VerPedidos()
        {
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