using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Pos
{
    public class PosController : Controller
    {
        private readonly IMediator _mediator;
        public PosController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
