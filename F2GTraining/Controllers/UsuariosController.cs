using F2GTraining.Extensions;
using F2GTraining.Models;
using F2GTraining.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult InicioSesion()
        {
            if (HttpContext.Session.GetObject<Usuario>("USUARIO") != null)
            {
                return RedirectToAction("MenuEquipo","Equipos");
            }

            return View();
        }

        [HttpPost]
        public IActionResult InicioSesion(string usuario, string contrasenha)
        {
            Usuario user = this.repo.GetUsuarioNamePass(usuario, contrasenha);
            
            if (user != null)
            {
                //guardar en sesion el usuario
                HttpContext.Session.SetObject("USUARIO", user);
                return RedirectToAction("MenuEquipo","Equipos");
            }
            else
            {
                ViewData["ERROR"] = "ERROR: Credenciales de acceso erróneas";
                return View();
            }
        }

        public IActionResult RegistroUsuario()
        {
            if (HttpContext.Session.GetObject<Usuario>("USUARIO") != null)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            return View();
        }

        [HttpPost]
        public IActionResult RegistroUsuario(string usuario, string contrasenha, string correo, int telefono)
        {
            if (usuario.Count() > 16)
            {
                ViewData["ERROR"] = "ERROR: EL NOMBRE DE USUARIO NO PUEDE TENER MAS DE 16 CARACTERES";

            }else if (this.repo.CheckUsuarioRegistro(usuario))
            {
                ViewData["ERROR"] = "ERROR: EL NOMBRE DE USUARIO INTRODUCIDO YA EXISTE";
            }
            else if (this.repo.CheckCorreoRegistro(correo))
            {
                ViewData["ERROR"] = "ERROR: EL CORREO ELECTRONICO INTRODUCIDO YA EXISTE";
            }
            else if (this.repo.CheckTelefonoRegistro(telefono))
            {
                ViewData["ERROR"] = "ERROR: EL TELEFONO INTRODUCIDO YA EXISTE";
            }
            else if (contrasenha.Count() <= 8)
            {
                ViewData["ERROR"] = "ERROR: LA CONTRASEÑA DEBE TENER UN MINIMO DE 8 CARACTERES";
            }

            if (ViewData["ERROR"] != null)
            {
                return View();
            }
            else
            {
                this.repo.InsertUsuario(usuario, correo, contrasenha, telefono);
                TempData["MENSAJE"] = "Usuario registrado correctamente";
                return RedirectToAction("InicioSesion");
            }
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("USUARIO");
            return RedirectToAction("InicioSesion");
        }

    }
}
