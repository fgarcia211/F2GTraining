using F2GTraining.Extensions;
using F2GTraining.Filters;
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

        [AuthorizeUsers]
        public IActionResult ListaSesiones(int idequipo)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());
            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != idusuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }
            else
            {
                ViewData["IDEQUIPO"] = idequipo;
                ViewData["NOMBREEQUIPO"] = equipo.Nombre;
                return View(this.repoEnt.GetEntrenamientosEquipo(idequipo));
            }

        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> ListaSesiones(int idequipo, string nombre)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());
            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != idusuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }
            else
            {
                await this.repoEnt.InsertEntrenamiento(idequipo, nombre);
                return RedirectToAction("ListaSesiones", new { idequipo = idequipo });
            }

        }

        [AuthorizeUsers]
        public async Task<IActionResult> EliminaEntrenamiento(int identrenamiento, int idequipo)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());
            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != idusuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }
            else
            {
                await this.repoEnt.BorrarEntrenamiento(identrenamiento);
                return RedirectToAction("ListaSesiones", new { idequipo = idequipo });
            }

        }

        [AuthorizeUsers]
        public IActionResult VistaEntrenamiento(int identrenamiento, int idequipo)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString()); 
            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != idusuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            Entrenamiento entrena = this.repoEnt.GetEntrenamiento(identrenamiento);
            List<Jugador> jugadoresequipo;

            if (entrena.Activo == false && entrena.FechaFin == null)
            {
                jugadoresequipo = this.repoJug.GetJugadoresEquipo(idequipo);
            }
            else if (entrena.Activo == false && entrena.FechaFin != null)
            {
                jugadoresequipo = this.repoJug.JugadoresXSesion(identrenamiento);
                List<JugadorEntrenamiento> notas = this.repoJug.GetNotasSesion(identrenamiento);
                ViewData["NOTAS"] = notas;
            }
            else
            {
                jugadoresequipo = this.repoJug.JugadoresXSesion(identrenamiento);
            }
            
            ViewData["JUGADORES"] = jugadoresequipo;
            ViewData["IDEQUIPO"] = idequipo;
            ViewData["IDENTRENAMIENTO"] = identrenamiento;

            return View(entrena);
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> VistaEntrenamiento(int identrenamiento, int idequipo, List<int> seleccionados, List<int> valoraciones)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString()); 
            Equipo equipo = this.repoEqu.GetEquipo(idequipo);

            if (equipo == null || equipo.IdUsuario != idusuario)
            {
                return RedirectToAction("MenuEquipo", "Equipos");
            }

            Entrenamiento entrena = this.repoEnt.GetEntrenamiento(identrenamiento);
            List<Jugador> jugadoresequipo;

            if (entrena.Activo == false)
            {
                //AQUI VA EL CODIGO PARA AÑADIRLO A LA AUXILIAR DE ENTRENAMIENTOJUGADOR
                await this.repoJug.AniadirJugadoresSesion(seleccionados, identrenamiento);
                //AQUI VA EL CODIGO PARA QUE LA SESION SE INICIE
                await this.repoEnt.EmpezarEntrenamiento(identrenamiento);

                jugadoresequipo = this.repoJug.JugadoresXSesion(identrenamiento);
            }
            else 
            {
                jugadoresequipo = this.repoJug.JugadoresXSesion(identrenamiento);
                //AQUI HAY QUE HACER PROCEDURE PARA ASIGNAR NOTAS A CADA JUGADOR APUNTADO
                await this.repoJug.AniadirPuntuacionesEntrenamiento(seleccionados, valoraciones, identrenamiento);
                //AQUI VA EL CODIGO PARA QUE LA SESION SE ACABE
                await this.repoEnt.FinalizarEntrenamiento(identrenamiento);

                List<JugadorEntrenamiento> notas = this.repoJug.GetNotasSesion(identrenamiento);
                ViewData["NOTAS"] = notas;
            }

            ViewData["JUGADORES"] = jugadoresequipo;
            ViewData["IDEQUIPO"] = idequipo;
            ViewData["IDENTRENAMIENTO"] = identrenamiento;

            return RedirectToAction("VistaEntrenamiento", new { idequipo = idequipo, identrenamiento = identrenamiento });

        }
    }
}
