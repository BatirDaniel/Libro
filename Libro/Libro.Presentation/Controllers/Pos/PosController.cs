using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Pos
{
    public class PosController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ToastService _toastService;
        public PosController(IMediator mediator, ToastService toastService = null)
        {
            _mediator = mediator;
            _toastService = toastService;
        }

        [Route("pos")]
        public IActionResult Pos()
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewData["ToastData"] = _toastService.GetToastData(ToastStatus.Warning, "Please login first");
                return View("~/Views/Error-Pages/404.cshtml");
            }
            return View();
        }
    }
}
