using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Analytics
{
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
