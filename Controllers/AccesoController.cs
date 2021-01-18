using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaMotores.Models;

namespace TiendaMotores.Controllers
{
    [Authorize]
    public class AccesoController : Controller
    {
        private TIENDAEntities db = new TIENDAEntities();
        // GET: Acceso
        public ActionResult IndexLogin(string correo)
        {
            String rol = "";
            using (db)
            {
                var query = from st in db.Empleado
                            where st.email == correo
                            select st;
                var lista = query.ToList();
                if (lista.Count > 0)
                {
                    var empleado = query.FirstOrDefault<Empleado>();
                    string[] nombre = empleado.nombre_empleado.ToString().Split(' ');
                    Session["nombre"] = nombre[0];
                    Session["usuario"] = empleado.nombre_empleado;
                    rol = empleado.rol.ToString();
                    if (HttpContext.Request.Cookies["usuario"] == null)
                    {
                        HttpCookie cookie = new HttpCookie("usuario");
                        cookie["rol"] = rol;
                        cookie["nombre"] = Session["nombre"].ToString();
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(cookie);
                    }

                }
                else
                {
                    var query1 = from st in db.Cliente
                                 where st.email == correo
                                 select st;
                    var lista1 = query.ToList();
                    if (lista1.Count > 0)
                    {
                        var cliente = query1.FirstOrDefault<Cliente>();
                        string[] nombre = cliente.nombre_cliente.ToString().Split(' ');
                        Session["nombre"] = nombre[0];
                        Session["usuario"] = cliente.nombre_cliente;
                        rol = "Cliente";
                        if (HttpContext.Request.Cookies["usuario"] == null)
                        {
                            HttpCookie cookie = new HttpCookie("usuario");
                            cookie["rol"] = rol;
                            cookie["nombre"] = Session["nombre"].ToString();
                            cookie["itemTotal"] = "0";
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(cookie);
                        }
                    }
                }
            }

            if (rol.Equals("Administrador"))
            {
                return RedirectToAction("Index","DepAdministracion");
            }else if (rol.Equals("Reclutador"))
            {
                return RedirectToAction("Index","DepRecursosHumanos");
                
            }
            else if (rol.Equals("Empaquetador"))
            {
                return RedirectToAction("Index", "DepPaqueteria");
            }
            else if (rol.Equals("Enviador"))
            {
                return RedirectToAction("Index", "DepEnvios");
            }
            else if (rol.Equals("Comprador"))
            {
                return RedirectToAction("Index", "DepCompras");

            }
            else if (rol.Equals("Vendedor"))
            {
                return RedirectToAction("Index", "DepVentas");
            }
            else if (rol.Equals("Productor"))
            {
                return RedirectToAction("Index", "DepProducion");
            }
            else if (rol.Equals("Cliente"))
            {
                return RedirectToAction("Index", "DepCliente");
            }
            return RedirectToAction("Index", "Defecto", routeValues:new { rol=rol});
        }
        // GET: Acceso
        public ActionResult IndexRegisterCliente(string correo)
        {
            
            return RedirectToAction("Index", "DepCliente");
        }
        public ActionResult IndexRegisterEmpleado(string email)
        {

         
                using (db)
                {
                    var query = from st in db.Empleado
                                where st.email == email
                                select st;
                    var lista = query.ToList();
                    if (lista.Count > 0)
                    {
                        var empleado = query.FirstOrDefault<Empleado>();
                        string[] nombre = empleado.nombre_empleado.ToString().Split(' ');
                        Session["nombre"] = nombre[0];
                        Session["usuario"] = empleado.nombre_empleado;
                        return RedirectToAction("IndexLogin",routeValues: new { correo = email});
                    }
                }
            
            return RedirectToAction("Index", "IndexDefecto",routeValues: new { rol = "Productor" });
        }
    }
}