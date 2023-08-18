using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Pos
{
    [Authorize]
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
            return View();
        }
    }
}
