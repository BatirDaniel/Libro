using Libro.Infrastructure.Services.ToastService;
using Libro.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Libro.Presentation.Controllers
{
    public class LibroController : Controller
    {
        private readonly IMediator _mediator;

        public LibroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            if (TempData.ContainsKey("message") && TempData.ContainsKey("svg"))
            {
                ViewBag.ToastMessage = TempData["ToastMessage"];
                ViewBag.ToastSvg = TempData["ToastSvg"];
            }
            return View("~/Views/Home/Index.cshtml");
        }

        [Route("Errors/403")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Error403()
        {
            return View("~/Views/Errors/Error403.cshtml");
        }

        [Route("Errors/404")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Error404()
        {
            return View("~/Views/Errors/Error404.cshtml");
        }

        [Route("Errors/500")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Error500()
        {
            return View("~/Views/Errors/Error500.cshtml");
        }
    }
}
