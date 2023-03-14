using F2GTraining.Extensions;
using F2GTraining.Models;
using F2GTraining.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class EntrenamientosController : Controller
    {
        private RepositoryEquipos repo;

        public EntrenamientosController(RepositoryEquipos repo)
        {
            this.repo = repo;
        }

        public IActionResult ListaSesiones(int idequipo)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            Equipo equipo = this.repo.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != user.IdUsuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            ViewData["IDEQUIPO"] = idequipo;
            return View();
            
        }

        public IActionResult EliminaEntrenamiento(int idsesion, int idequipo)
        {
            return RedirectToAction("ListaSesiones");
        }

        public IActionResult VistaEntrenamiento(int idsesion, int idequipo)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            Equipo equipo = this.repo.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != user.IdUsuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            ViewData["IDEQUIPO"] = idequipo;
            return View();
        }
    }
}
