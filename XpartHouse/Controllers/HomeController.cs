using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XpartHouse.Models;

namespace XpartHouse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var usuario = XpartHouse.Models.Authorization.GetLoggedUser(this.HttpContext);
            if (usuario.IdContacto > 0)
            {
                ViewBag.usuario = usuario;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Login(bool? error)
        {
            ViewBag.showError = (error != null) ? "d-block" : "d-none";
            return View();
        }
    }
}