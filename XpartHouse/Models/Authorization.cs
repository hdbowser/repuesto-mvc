using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XpartHouse.Models
{
    public static class Authorization
    {
        public static Contacto GetLoggedUser(System.Web.HttpContextBase context)
        {
            var user = new Contacto();
            user.Rol = new Rol();
            user.IdContacto = Convert.ToInt32(context.Session["idUsuario"] ?? "0");
            user.Nombre = (context.Session["usuario"] != null)? context.Session["usuario"].ToString() : "";
            user.Rol.Nombre = (context.Session["usuario"] != null)? context.Session["rol"].ToString() : "";

            return  user;
        }
    }
}