using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers
{
    public class LibroController : Controller
    {
        protected readonly IToastService _toastService;
        protected IUnitOfWork _unitOfWork;
        protected ApplicationDbContext _context;
        protected IMediator _mediator;

        public LibroController(
            IToastService toastService,
            IUnitOfWork unitOfWork,
            ApplicationDbContext context,
            IMediator mediator)
        {
            _toastService = toastService;
            _unitOfWork = unitOfWork;
            _context = context;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            if (TempData.ContainsKey("message") && TempData.ContainsKey("svg"))
            {
                ViewBag.ToastMessage = TempData["ToastMessage"];
                ViewBag.ToastSvg = TempData["ToastSvg"];
            }
            if (User.Identity.IsAuthenticated)
                return View("~/Views/Analytics/Dashboard.cshtml");

            return View("~/Views/Home/Index.cshtml");
        }

        public  IActionResult ResponseResult(string? message, ToastStatus? status, string? additional = null)
        {
            if ((status == ToastStatus.Success || status == ToastStatus.Info))
                return Ok(new
                {
                    redirectUrl = additional,
                    toast = _toastService.GetToastData(status.Value, message)
                });

            return BadRequest(new
            {
                redirectUrl = additional,
                toast = _toastService.GetToastData(status.Value, message)
            });
        }

        [Route("error/403")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Error403()
        {
            return View("~/Views/Errors/Error403.cshtml");
        }

        [Route("error/404")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Error404()
        {
            return View("~/Views/Errors/Error404.cshtml");
        }

        [Route("error/500")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Error500()
        {
            return View("~/Views/Errors/Error500.cshtml");
        }
    }
}
