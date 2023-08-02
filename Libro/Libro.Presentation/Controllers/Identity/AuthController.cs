using Libro.Business.Commands.IdentityCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Identity
{
    public class AuthController : LibroController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("auth/signIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost("auth/signOut")]
        public async Task<IActionResult> SignOut([FromBody] string coockie)
        {
            var result = await _mediator.Send(coockie);

            return result != null ? Ok(result) : NotFound();
        }

        [Route("auth/login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("auth/register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
