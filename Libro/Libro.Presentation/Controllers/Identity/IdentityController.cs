using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.User
{
    public class IdentityController : LibroController
    {
        private readonly IMediator _mediator;
        public IdentityController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("user/Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result != null ? Ok() : NotFound();
        }

        [HttpPost("user/Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result != null ? Ok() : NotFound();
        }

        [HttpDelete("user/Remove/{userid?}")]
        public async Task<IActionResult> Remove([FromQuery] string userid)
        {
            var query = new UserRemoveQuery(userid);
            var result = await _mediator.Send(query);

            return result != null ? Ok() : NotFound();
        }

        [HttpGet("user/GetAllCompleteUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);

            return result != null ? Ok() : NotFound();
        }
    }
}
