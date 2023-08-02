using MediatR;

namespace Libro.Presentation.Controllers.Issue
{
    public class IssueController : LibroController
    {
        private readonly IMediator _mediator;

        public IssueController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }
    }
}
