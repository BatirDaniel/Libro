using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers
{
    public class LibroController : Controller
    {
        protected readonly IToastService _toastService;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ApplicationDbContext _context;

        public LibroController(
            IToastService toastService,
            IUnitOfWork unitOfWork)
        {
            _toastService = toastService;
            _unitOfWork = unitOfWork;
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
                toast = _toastService.GetToastData(status.Value, message)
            });
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
