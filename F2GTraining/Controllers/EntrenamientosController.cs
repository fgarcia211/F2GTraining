using F2GTraining.Extensions;
using F2GTraining.Models;
using F2GTraining.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class EntrenamientosController : Controller
    {
        private RepositoryEntrenamientos repoEnt;
        private RepositoryEquipos repoEqu;
        private RepositoryJugadores repoJug;

        public EntrenamientosController(RepositoryEquipos repo, RepositoryEntrenamientos repo2, RepositoryJugadores repo3)
        {
            this.repoEqu = repo;
            this.repoEnt = repo2;
            this.repoJug = repo3;
        }

        public IActionResult ListaSesiones(int idequipo)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != user.IdUsuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            ViewData["IDEQUIPO"] = idequipo;
            return View(this.repoEnt.GetEntrenamientosEquipo(idequipo));
            
        }

        [HttpPost]
        public async Task<IActionResult> ListaSesiones(int idequipo, string nombre)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != user.IdUsuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            await this.repoEnt.InsertEntrenamiento(idequipo, nombre);
            return RedirectToAction("ListaSesiones", new { idequipo = idequipo });
        }

        public async Task<IActionResult> EliminaEntrenamiento(int identrenamiento, int idequipo)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != user.IdUsuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            await this.repoEnt.BorrarEntrenamiento(identrenamiento);
            return RedirectToAction("ListaSesiones",new {idequipo = idequipo});
        }

        public IActionResult VistaEntrenamiento(int identrenamiento, int idequipo)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != user.IdUsuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            List<Jugador> jugadoresequipo = this.repoJug.GetJugadoresEquipo(idequipo);

            ViewData["JUGADORES"] = jugadoresequipo;
            ViewData["IDEQUIPO"] = idequipo;

            return View(this.repoEnt.GetEntrenamiento(identrenamiento));
        }
    }
}
