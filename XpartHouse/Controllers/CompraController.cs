using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XpartHouse.Models;

namespace XpartHouse.Controllers
{
    public class CompraController : Controller
    {
        public CompraController()
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
        public ActionResult Editar(int? id)
        {
            var usuario = XpartHouse.Models.Authorization.GetLoggedUser(this.HttpContext);
            if (usuario.IdContacto > 0)
            {
                ViewBag.usuario = usuario;
                ViewBag.id = id;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public JsonResult Detalle(int? id)
        {
            var compra = db.Compras.Find(id);
            if (compra != null)
            {
                var proveedor = db.Contactos.Find(compra.IdProveedor);
                proveedor.Password = "";
                compra.Proveedor = proveedor;

                return Json(compra, JsonRequestBehavior.AllowGet);
            }
            return Json(new Compra(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Buscar(string busqueda)
        {
            busqueda = busqueda ?? "";
            var compras = from C in db.Compras
                          join P in db.Contactos
                          on C.IdProveedor equals P.IdContacto
                          where P.Nombre.Contains(busqueda) || P.Apellidos.Contains(busqueda)
                          select new { IdCompra = C.IdCompra, Proveedor = P.Nombre + " " + P.Apellidos, Fecha = C.Fecha.ToString()};
            return Json(compras, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Items(int idCompra)
        {
            var items = from I in db.CompraDetalleItems
                        join B in db.Articulos
                        on I.IdArticulo equals B.IdArticulo
                        where I.IdCompra.Equals(idCompra)
                        select new { Articulo = B.Descripcion, NumLinea = I.NumLinea, IdArticul = I.IdArticulo, Cantidad = I.Cantidad, Precio = I.Precio, Descuento = I.Descuento, Total = (I.Precio * I.Cantidad) - I.Descuento };

            //var items = db.CompraDetalleItems.Join(Articulo, x => )
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Crear(Compra compra)
        {
            compra.Fecha = DateTime.Now;
            compra.Pagada = false;

            ModelState.Clear();
            TryValidateModel(compra);

            if (ModelState.IsValid)
            {
                db.Compras.Add(compra);
                db.SaveChanges();
                return Json(compra.IdCompra, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet); ;
        }
        public JsonResult AgregarItem(CompraDetalleItem item)
        {
            ModelState.Clear();
            TryValidateModel(item);

            if (ModelState.IsValid)
            {
                db.CompraDetalleItems.Add(item);
                int max = db.CompraDetalleItems.Where(x => x.IdCompra == item.IdCompra).Select(x => x.NumLinea).DefaultIfEmpty(0).Max();
                item.NumLinea = max + 1;

                db.SaveChanges();
                var articulo = db.Articulos.Find(item.IdArticulo);
                articulo.Existencia = articulo.Existencia + item.Cantidad;
                db.Entry(articulo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet); ;
        }
    }
}