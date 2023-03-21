using F2GTraining.Extensions;
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

        public IActionResult CrearJugador(int idequipo)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }
            else
            {
                Equipo equipo = this.repoEqu.GetEquipo(idequipo);

                if (equipo == null || equipo.IdUsuario != user.IdUsuario)
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
            
        }

        [HttpPost]
        public async Task<IActionResult> CrearJugador(int idequipo, int idposicion, string nombre, int dorsal, int edad, int peso, int altura)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }
            else
            {
                Equipo equipo = this.repoEqu.GetEquipo(idequipo);

                if (equipo != null || equipo.IdUsuario == user.IdUsuario)
                {
                    await this.repoJug.InsertJugador(idequipo, idposicion, nombre, dorsal, edad, peso, altura);
                }

                return RedirectToAction("MenuEquipo", "Equipos");
            }

        }

        public async Task<IActionResult> DeleteJugador(int idjugador)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }
            else
            {
                List<Jugador> jugadoresUser = this.repoJug.JugadoresXUsuario(user.IdUsuario);
                
                if (jugadoresUser.Contains(this.repoJug.GetJugadorID(idjugador))){

                    await this.repoJug.DeleteJugador(idjugador);

                }
                
                return RedirectToAction("MenuEquipo", "Equipos");
            }
        }

        public IActionResult GraficaJugador(int idjugador)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }
            else
            {
                List<Jugador> jugadoresUser = this.repoJug.JugadoresXUsuario(user.IdUsuario);
                Jugador jugMostrar = this.repoJug.GetJugadorID(idjugador);

                if (jugadoresUser.Contains(jugMostrar))
                {
                    EstadisticaJugador stats = this.repoJug.GetEstadisticasJugador(idjugador);
                    ViewData["ESTADISTICAS"] = stats;
                    return View(jugMostrar);
                }

                return RedirectToAction("MenuEquipo", "Equipos");
            }
        }

        public IActionResult _PartialJugadoresEquipo(int idequipo)
        {
            return PartialView(this.repoJug.GetJugadoresEquipo(idequipo));
        }
    }
}
