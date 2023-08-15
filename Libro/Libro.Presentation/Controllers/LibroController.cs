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
        private readonly ToastService _toastService;

        public LibroController(IMediator mediator, ToastService toastService)
        {
            _mediator = mediator;
            _toastService = toastService;
        }

        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
