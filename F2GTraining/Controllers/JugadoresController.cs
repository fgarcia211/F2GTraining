using F2GTraining.Extensions;
using F2GTraining.Filters;
using F2GTraining.Models;
using F2GTraining.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class JugadoresController : Controller
    {
        private RepositoryJugadores repoJug;
        private RepositoryEquipos repoEqu;

        public JugadoresController(RepositoryJugadores repo, RepositoryEquipos repo2)
        {
            this.repoJug = repo;
            this.repoEqu = repo2;
        }

        [AuthorizeUsers]
        public IActionResult CrearJugador(int idequipo)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());

            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != idusuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }
            else
            {
                ViewData["IDEQUIPO"] = equipo.IdEquipo;
                ViewData["NOMBRE"] = equipo.Nombre;
                return View(this.repoJug.GetPosiciones());
            }

        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> CrearJugador(int idequipo, int idposicion, string nombre, int dorsal, int edad, int peso, int altura)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());

            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo != null || equipo.IdUsuario == idusuario)
            {
                await this.repoJug.InsertJugador(idequipo, idposicion, nombre, dorsal, edad, peso, altura);
            }

            return RedirectToAction("MenuEquipo", "Equipos");

        }

        [AuthorizeUsers]
        public async Task<IActionResult> DeleteJugador(int idjugador)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());
            List<Jugador> jugadoresUser = this.repoJug.JugadoresXUsuario(idusuario);

            if (jugadoresUser.Contains(this.repoJug.GetJugadorID(idjugador)))
            {
                await this.repoJug.DeleteJugador(idjugador);
            }

            return RedirectToAction("MenuEquipo", "Equipos");

        }

        [AuthorizeUsers]
        public IActionResult GraficaJugador(int idjugador)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());
            List<Jugador> jugadoresUser = this.repoJug.JugadoresXUsuario(idusuario);
            Jugador jugMostrar = this.repoJug.GetJugadorID(idjugador);

            if (jugadoresUser.Contains(jugMostrar))
            {
                EstadisticaJugador stats = this.repoJug.GetEstadisticasJugador(idjugador);
                ViewData["ESTADISTICAS"] = stats;
                return View(jugMostrar);
            }
            else
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

        }

        [AuthorizeUsers]
        public IActionResult _PartialJugadoresEquipo(int idequipo)
        {
            return PartialView(this.repoJug.GetJugadoresEquipo(idequipo));
        }
    }
}
