using Microsoft.AspNetCore.Mvc;

namespace F2GTraining.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InicioSesion(string usuario, string contrasenha)
        {
            return RedirectToAction("InicioSesion");
        }

        public IActionResult RegistroUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistroUsuario(string usuario, string contrasenha, string correo, string telefono)
        {
            return RedirectToAction("InicioSesion");
        }

        public IActionResult CerrarSesion()
        {
            return RedirectToAction("InicioSesion");
        }

    }
}
