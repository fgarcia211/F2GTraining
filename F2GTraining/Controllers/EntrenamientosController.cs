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

            Entrenamiento entrena = this.repoEnt.GetEntrenamiento(identrenamiento);
            List<Jugador> jugadoresequipo;

            if (entrena.Activo == false)
            {
                jugadoresequipo = this.repoJug.GetJugadoresEquipo(idequipo);
            }
            else
            {
                //AQUI HAY QUE HACER PROCEDURE PARA RECOGER LOS QUE ESTEN EN ESA SESION
                jugadoresequipo = this.repoJug.GetJugadoresEquipo(idequipo);
            }
            
            ViewData["JUGADORES"] = jugadoresequipo;
            ViewData["IDEQUIPO"] = idequipo;
            ViewData["IDENTRENAMIENTO"] = identrenamiento;

            return View(entrena);
        }

        [HttpPost]
        public async Task<IActionResult> VistaEntrenamiento(int identrenamiento, int idequipo, int[] seleccionados)
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

            Entrenamiento entrena = this.repoEnt.GetEntrenamiento(identrenamiento);
            List<Jugador> jugadoresequipo;

            if (entrena.Activo == false)
            {
                //AQUI VA EL CODIGO PARA AÑADIRLO A LA AUXILIAR DE ENTRENAMIENTOJUGADOR
                //AQUI VA EL CODIGO PARA QUE LA SESION SE INICIE
                await this.repoEnt.EmpezarEntrenamiento(identrenamiento);
                //AQUI VA EL CODIGO PARA RECOGER LOS JUGADORES QUE ESTAN APUNTADOS A ESA SESION
                jugadoresequipo = this.repoJug.GetJugadoresEquipo(idequipo);
            }
            else 
            {
                //AQUI HAY QUE HACER PROCEDURE PARA RECOGER LOS QUE ESTEN EN ESA SESION
                jugadoresequipo = this.repoJug.GetJugadoresEquipo(idequipo);
                //AQUI HAY QUE HACER PROCEDURE PARA ASIGNAR NOTAS A CADA JUGADOR APUNTADO
                //AQUI VA EL CODIGO PARA QUE LA SESION SE ACABE
                await this.repoEnt.FinalizarEntrenamiento(identrenamiento);
            }

            ViewData["JUGADORES"] = jugadoresequipo;
            ViewData["IDEQUIPO"] = idequipo;
            ViewData["IDENTRENAMIENTO"] = identrenamiento;

            return RedirectToAction("VistaEntrenamiento", new { idequipo = idequipo, identrenamiento = identrenamiento });

        }
    }
}
