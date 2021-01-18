using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaMotores.Controllers
{
    [Authorize]
    public class RecursosHumanosController : Controller
    {
        // GET: RecursosHumanos
        public ActionResult Index()
        {
            return View();
        }
    }
}