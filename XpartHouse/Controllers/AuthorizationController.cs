using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web;
using System.Web.Helpers;
using XpartHouse.Models;

namespace XpartHouse.Models
{
    public class AuthorizationController : Controller
    {
        public AuthorizationController()
        {
            this.db = new ContextDB();
        }
        private ContextDB db;
        public ActionResult Login(string usuario, string password)
        {
            usuario = usuario ?? "";
            password = password ?? "";
            var usr = db.Contactos.Include(x => x.Rol)
                .Where(x => x.Email == usuario && x.EsUsuario).FirstOrDefault();

            if (usr != null)
            {
                if (Crypto.VerifyHashedPassword(usr.Password, password))
                {
                    HttpContext.Session.Add("usuario", usr.Nombre + " " + usr.Apellidos);
                    HttpContext.Session.Add("idUsuario", usr.IdContacto);
                    HttpContext.Session.Add("email", usr.Email);
                    HttpContext.Session.Add("rol", usr.Rol.Nombre ?? "");

                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login", "Home", new { error = true });
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }


    }
}