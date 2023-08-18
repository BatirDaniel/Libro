using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Analytics
{
    [Authorize]
    public class Analytics : Controller
    {
        private readonly IMediator _mediator;
        public Analytics(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
