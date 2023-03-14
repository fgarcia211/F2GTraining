using F2GTraining.Extensions;
using F2GTraining.Models;
using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class EntrenamientosController : Controller
    {
        public IActionResult ListaSesiones(int idequipo)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            ViewData["MENSAJE"] = "Ver sesiones para el equipo con ID" + idequipo;
            return View();
        }

        public IActionResult EliminaEntrenamiento(int idsesion)
        {
            return RedirectToAction("ListaSesiones");
        }

        public IActionResult VistaEntrenamiento(int idsesion)
        {
            ViewData["MENSAJE"] = idsesion;
            return View();
        }
    }
}
