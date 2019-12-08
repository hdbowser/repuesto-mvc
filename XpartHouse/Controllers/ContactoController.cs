using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XpartHouse.Models;

namespace XpartHouse.Controllers
{
    public class ContactoController : Controller
    {
        private ContextDB db = new ContextDB();
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

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = XpartHouse.Models.Authorization.GetLoggedUser(this.HttpContext);
            if (usuario.IdContacto > 0)
            {
                ViewBag.usuario = usuario;
                var contacto = db.Contactos.Find(id);
                if (contacto == null)
                {
                    return HttpNotFound();
                }

                return View(contacto);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public JsonResult Crear(Contacto contacto)
        {
            contacto.FechaCreado = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(contacto);

            if (ModelState.IsValid)
            {
                db.Contactos.Add(contacto);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet); ;
        }

        [HttpGet]
        public JsonResult Buscar(string busqueda)
        {
            if (busqueda != null)
            {
                var usuarios = db.Contactos.Where(x => x.Nombre.Contains(busqueda) || x.Apellidos.Contains(busqueda))
                    .Select(x => new { x.IdContacto, x.Nombre, x.Apellidos, x.Email }).ToList();
                return Json(usuarios, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var usuarios = db.Contactos
                    .Select(x => new { x.IdContacto, x.Nombre, x.Apellidos, x.Email }).ToList();
                return Json(usuarios, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult BuscarProveedores(string busqueda)
        {
            busqueda = busqueda ?? "";
            var usuarios = db.Contactos.Where(x => x.EsProveedor && (x.Nombre.Contains(busqueda) || x.Apellidos.Contains(busqueda)))
                .Select(x => new { x.IdContacto, x.EsProveedor, x.Nombre, x.Apellidos, x.Email }).ToList();
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actualizar(Contacto contacto)
        {
            ModelState.Clear();
            TryValidateModel(contacto);

            if (ModelState.IsValid)
            {
                db.Entry(contacto).State = EntityState.Modified;

                db.Entry(contacto).Property("Password").IsModified = false;
                db.Entry(contacto).Property("IdRol").IsModified = false;
                db.Entry(contacto).Property("FechaCreado").IsModified = false;
                db.Entry(contacto).Property("EsUsuario").IsModified = false;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet); ;
        }
    }
}
