using Libro.Business.Commands.IdentityCommands;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInUserCommand command, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _mediator.Send(command);

            if (string.IsNullOrEmpty(result.Item2))
            {
                var claimIdentity = new ClaimsIdentity(result.Item1, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                return Redirect(ReturnUrl == null ? "/auth/register" : ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
        }

        public new async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        [Route("auth/login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
