using F2GTraining.Extensions;
using F2GTraining.Helpers;
using F2GTraining.Models;
using F2GTraining.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class EquiposController : Controller
    {
        private RepositoryEquipos repoEqu;
        private RepositoryJugadores repoJug;
        private HelperSubirFicheros helperArchivos;

        public EquiposController(RepositoryEquipos repo, RepositoryJugadores repo2, HelperSubirFicheros helperPath)
        {
            this.repoEqu = repo;
            this.repoJug = repo2;
            this.helperArchivos = helperPath;
        }

        public IActionResult MenuEquipo()
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            List<Equipo> equipos = this.repoEqu.GetEquiposUser(user.IdUsuario);

            if (equipos.Count == 0)
            {
                return View();
            }
            else
            {
                List<Jugador> jugadoresUsuario = new List<Jugador>();

                foreach (Equipo equipo in equipos)
                {
                    foreach (Jugador jugador in this.repoJug.GetJugadoresEquipo(equipo.IdEquipo))
                    {
                        jugadoresUsuario.Add(jugador);
                    }
                }

                ViewData["JUGADORESUSUARIO"] = jugadoresUsuario;
                return View(equipos);
            }

        }

        public IActionResult CrearEquipo()
        {

            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearEquipo(string nombre, IFormFile imagen)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            string extension = System.IO.Path.GetExtension(imagen.FileName);

            if (extension == ".png")
            {
                string path = await this.helperArchivos.UploadFileAsync(imagen, nombre.ToLower());
                await this.repoEqu.InsertEquipo(user.IdUsuario, nombre, path);
                return RedirectToAction("MenuEquipo","Equipos");
            }
            else
            {
                ViewData["ERROR"] = "ERROR: La imagen debe ser en formato PNG";
                return View();
            }
        
        }

    }
}
