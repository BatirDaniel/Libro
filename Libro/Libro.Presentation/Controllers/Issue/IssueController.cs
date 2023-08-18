using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Issue
{
    [Authorize]
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
            return View();
        }
    }
}
