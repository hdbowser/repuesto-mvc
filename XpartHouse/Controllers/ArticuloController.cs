using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XpartHouse.Models;

namespace XpartHouse.Controllers
{
    public class ArticuloController : Controller
    {
        public ArticuloController()
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

        [HttpPost]
        public JsonResult Crear(Articulo articulo)
        {
            articulo.FechaCreado = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(articulo);

            if (ModelState.IsValid)
            {
                db.Articulos.Add(articulo);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet); ;
        }

        [HttpGet]
        public JsonResult Buscar(string busqueda)
        {
            busqueda = busqueda ?? "";
            var articulos = db.Articulos.Where(x => x.Descripcion.Contains(busqueda) || x.CodigoBarras.Contains(busqueda))
                .Select(x => new { x.IdArticulo, x.Descripcion, x.Precio, x.Existencia }).Take(20).ToList();
            return Json(articulos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(int id)
        {
            var usuario = XpartHouse.Models.Authorization.GetLoggedUser(this.HttpContext);
            if (usuario.IdContacto > 0)
            {
                ViewBag.usuario = usuario;
                var articulo = db.Articulos.Find(id);
                if (articulo == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdFamilia = new SelectList(db.Familias, "IdFamilia", "Nombre", articulo.IdFamilia);
                return View(articulo);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public JsonResult Actualizar(Articulo articulo)
        {
            ModelState.Clear();
            TryValidateModel(articulo);

            if (ModelState.IsValid)
            {
                db.Entry(articulo).State = EntityState.Modified;
                db.Entry(articulo).Property("FechaCreado").IsModified = false;
                db.Entry(articulo).Property("Eliminado").IsModified = false;

                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet); ;
        }

    }
}