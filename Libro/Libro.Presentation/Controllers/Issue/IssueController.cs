using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Issue
{
    public class IssueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
