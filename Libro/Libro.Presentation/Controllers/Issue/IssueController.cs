using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Issue
{
    public class IssueController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ToastService _toastService;

        public IssueController(IMediator mediator, ToastService toastService = null)
        {
            _mediator = mediator;
            _toastService = toastService;
        }

        [Route("issue")]
        public IActionResult Issue()
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
