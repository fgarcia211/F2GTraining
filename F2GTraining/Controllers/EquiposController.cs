using F2GTraining.Extensions;
using F2GTraining.Helpers;
using F2GTraining.Models;
using F2GTraining.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class EquiposController : Controller
    {
        private RepositoryEquipos repo;
        private HelperSubirFicheros helperArchivos;

        public EquiposController(RepositoryEquipos repo, HelperSubirFicheros helperPath)
        {
            this.repo = repo;
            this.helperArchivos = helperPath;
        }

        public IActionResult MenuEquipo()
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            if (user == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios");
            }

            List<Equipo> equipos = this.repo.GetEquiposUser(user.IdUsuario);

            if (equipos.Count == 0)
            {
                return View();
            }
            else
            {
                return View(equipos);
            }

        }

        public IActionResult CrearEquipo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearEquipo(string nombre, IFormFile imagen)
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USUARIO");

            string extension = System.IO.Path.GetExtension(imagen.FileName);

            if (extension == ".png")
            {
                string path = await this.helperArchivos.UploadFileAsync(imagen, nombre.ToLower());
                this.repo.InsertEquipo(user.IdUsuario, nombre, path);
                return RedirectToAction("MenuEquipo","Equipos");
            }
            else
            {
                ViewData["ERROR"] = "ERROR: La imagen debe ser en formato PNG";
                return View();
            }
            
            return View();
        }
    }
}
