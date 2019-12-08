using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XpartHouse.Models;

namespace XpartHouse.Controllers
{
    public class FamiliaController : Controller
    {
        public FamiliaController()
        {
            this.db = new ContextDB();
        }
        ContextDB db;
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

        public JsonResult Buscar()
        {
            var familias = db.Familias.ToList();
            return Json(familias, JsonRequestBehavior.AllowGet);
        }
    }
}