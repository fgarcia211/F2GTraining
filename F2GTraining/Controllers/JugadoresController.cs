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
                    return View();
                }
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CrearJugador(int idequipo, int idposicion, string nombre, int dorsal, int edad, decimal peso, float altura)
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
                    /*await this.repoJug.InsertJugador(idequipo, idposicion, nombre, dorsal, edad, peso, altura);*/
                    return View();

                }
            }

        }
    }
}
