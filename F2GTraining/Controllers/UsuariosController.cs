using F2GTraining.Extensions;
using F2GTraining.Filters;
using F2GTraining.Models;
using F2GTraining.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace F2GTraining.Controllers
{
    public class UsuariosController : Controller
    {
        private IRepositoryF2GTraining repo;

        public UsuariosController(IRepositoryF2GTraining repo)
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
        public async Task<IActionResult> InicioSesion(string usuario, string contrasenha)
        {
            Usuario user = this.repo.GetUsuarioNamePass(usuario, contrasenha);
            
            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity
                (CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = new Claim(ClaimTypes.Name, user.Nombre);
                identity.AddClaim(claimName);

                Claim claimId = new Claim("IDUSUARIO", user.IdUsuario.ToString());
                identity.AddClaim(claimId);

                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , userPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddHours(12)
                    });

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
        public async Task<IActionResult> RegistroUsuario(string usuario, string contrasenha, string correo, int telefono)
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
                await this.repo.InsertUsuario(usuario, correo, contrasenha, telefono);
                TempData["MENSAJE"] = "Usuario registrado correctamente";
                return RedirectToAction("InicioSesion");
            }
        }

        [AuthorizeUsers]
        public async Task<IActionResult> CerrarSesion()
        {
            HttpContext.Session.Remove("USUARIO");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("InicioSesion");
        }

    }
}
