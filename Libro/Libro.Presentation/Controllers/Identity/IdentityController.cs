using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.User
{
    public class IdentityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
