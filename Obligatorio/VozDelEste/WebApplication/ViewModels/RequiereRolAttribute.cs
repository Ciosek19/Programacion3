using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.ViewModels
{
    public class RequiereRolAttribute : AuthorizeAttribute
    {
        private readonly string[] rolesPermitidos;
        public RequiereRolAttribute(params RolUsuario[] roles) 
        {
            rolesPermitidos = roles.Select(x => x.ToString()).ToArray();        
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var rol = httpContext.Session["Rol"]?.ToString();

            return rol != null && rolesPermitidos.Any(e => e.Equals(rol, StringComparison.OrdinalIgnoreCase));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/usuarios/login");
        }
    }
}