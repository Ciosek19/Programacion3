using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class UsuariosController : Controller
    {
        private VozDelEsteBDEntities db = new VozDelEsteBDEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string nombreUsuario, string clave)
        {
            var usuario = db.Usuario.FirstOrDefault(u =>
                u.NombreUsuario == nombreUsuario && u.Clave == clave);

            if (usuario != null)
            {
                // Crea la cookie de autenticación
                FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, false);

                // Guarda el rol en la sesión
                Session["Rol"] = usuario.RolUsuario.ToString();
                Session["UsuarioActual"] = usuario;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensaje = "Usuario o contraseña incorrectos.";
            return View();
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registro(RegistroViewModel registro)
        {
            if (ModelState.IsValid)
            {
                var persona = new Persona()
                {
                    Nombre = registro.PersonaVM.Nombre,
                    Apellido = registro.PersonaVM.Apellido,
                    ImagenUrl = "",
                    FechaNacimiento = registro.PersonaVM.FechaNacimiento,
                };
                db.Persona.Add(persona);
                db.SaveChanges();

                var usuario = new Usuario()
                {
                    PersonaID = persona.Id,
                    NombreUsuario = registro.UsuarioVM.NombreUsuario,
                    Email = registro.UsuarioVM.Email,
                    Clave = registro.UsuarioVM.Clave,
                    Rol = 3
                };
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            
            return View(registro);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
