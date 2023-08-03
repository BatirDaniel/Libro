using Libro.Business.Commands.IdentityCommands;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> SignIn([FromBody] SignInUserCommand command, string? ReturnUrl)
        {
            var result = await _mediator.Send(command);

            if(result.Item1 != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Item1.Name),
                    new Claim(ClaimTypes.NameIdentifier, result.Item1.Id),
                };
                var claimIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                return Redirect(ReturnUrl == null ? "/Dashboard" : ReturnUrl);
            }
            else return View(result.Item2);
        }

        [HttpPost("auth/signOut")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("");
        }

        [Route("auth/login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
