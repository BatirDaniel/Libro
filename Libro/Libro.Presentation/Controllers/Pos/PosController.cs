using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Pos
{
    public class PosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
