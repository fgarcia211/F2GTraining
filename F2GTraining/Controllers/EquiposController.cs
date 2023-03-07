using F2GTraining.Extensions;
using F2GTraining.Models;
using F2GTraining.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class EquiposController : Controller
    {
        private RepositoryEquipos repo;

        public EquiposController(RepositoryEquipos repo)
        {
            this.repo = repo;
        }

        public IActionResult MenuEquipo()
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            List<Equipo> equipos = this.repo.GetEquiposUser(user.IdUsuario);

            return View();
        }

        public IActionResult CrearEquipo()
        {
            return View();
        }
    }
}
