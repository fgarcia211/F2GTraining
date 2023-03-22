using F2GTraining.Extensions;
using F2GTraining.Filters;
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

        [AuthorizeUsers]
        public IActionResult MenuEquipo()
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());

            List<Equipo> equipos = this.repoEqu.GetEquiposUser(idusuario);

            if (equipos.Count == 0)
            {
                return View();
            }
            else
            {
                ViewData["JUGADORESUSUARIO"] = this.repoJug.JugadoresXUsuario(idusuario);
                return View(equipos);
            }

        }

        [AuthorizeUsers]
        public IActionResult _PartialVistaEquipo(int idequipo)
        {
            return PartialView(this.repoEqu.GetEquipo(idequipo));
        }

        [AuthorizeUsers]
        public IActionResult CrearEquipo()
        {
            return View();
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> CrearEquipo(string nombre, IFormFile imagen)
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst("IDUSUARIO").Value.ToString());

            string extension = System.IO.Path.GetExtension(imagen.FileName);

            if (extension == ".png")
            {
                string path = await this.helperArchivos.UploadFileAsync(imagen, nombre.ToLower());
                await this.repoEqu.InsertEquipo(idusuario, nombre, path);
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
