using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using XpartHouse.Models;

namespace XpartHouse.Controllers
{
    public class UsuarioController : Controller
    {
        private ContextDB db = new ContextDB();

        // GET: Contacto
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

        [HttpPost]
        public JsonResult Crear(Contacto contacto)
        {
            contacto.FechaCreado = DateTime.Now;
            contacto.EsUsuario = true;
            contacto.Password = Crypto.HashPassword(contacto.Password);
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
                var usuarios = db.Contactos.Where(x => x.EsUsuario == true && x.Nombre.Contains(busqueda))
                    .Select(x => new { x.IdContacto, x.Nombre, x.Apellidos, x.Email }).ToList();
                return Json(usuarios, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var usuarios = db.Contactos.Where(x => x.EsUsuario == true)
                    .Select(x => new { x.IdContacto, x.Nombre, x.Apellidos, x.Email }).ToList();
                return Json(usuarios, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Editar(int id)
        {
            var usuario = XpartHouse.Models.Authorization.GetLoggedUser(this.HttpContext);
            if (usuario.IdContacto > 0)
            {
                ViewBag.usuario = usuario;
                var usr = db.Contactos.Find(id);
                if (usr == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdRol = new SelectList(db.Rols, "IdRol", "Nombre", usr.IdRol);
                return View(usr);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
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
                db.Entry(contacto).Property("EsUsuario").IsModified = false;
                db.Entry(contacto).Property("EsProveedor").IsModified = false;
                db.Entry(contacto).Property("EsCliente").IsModified = false;
                db.Entry(contacto).Property("FechaCreado").IsModified = false;
                db.Entry(contacto).Property("RNC").IsModified = false;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet); ;
        }

        public JsonResult ListadoRoles()
        {
            return Json(db.Rols, JsonRequestBehavior.AllowGet);
        }

    }
}
