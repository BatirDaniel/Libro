using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Queries.IdentityQueries;
using Libro.DataAccess.Entities;
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
        public async Task<IActionResult> Create(AddUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await _mediator.Send(command);
            return result == null ? Ok("Success") : BadRequest(result);
        }

        [HttpPost("user/Update")]
        public async Task<IActionResult> Update(AddUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result == null ? Ok("Success") : BadRequest(result);
        }

        [HttpDelete("user/Remove/{userid?}")]
        public async Task<IActionResult> Remove(string userid)
        {
            var command = new RemoveUserCommand(userid);
            var result = await _mediator.Send(command);

            return result == null ? Ok("Succes") : BadRequest(result);
        }

        [HttpGet("user/GeAutocompleteUsers/{filter?}")]
        public async Task<IActionResult> GetUsers(string filter)
        {
            var query = new GeAutocompleteUsersQuery(filter);
            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : BadRequest(result);
        }

        [Route("auth/register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
