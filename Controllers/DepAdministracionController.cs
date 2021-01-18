using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaMotores.Controllers
{
    [Authorize]
    public class DepAdministracionController : Controller
    {
        // GET: DepAdministracion
        public ActionResult Index()
        {
            return View();
        }
    }
}