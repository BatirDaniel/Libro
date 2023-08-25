using Libro.Business.Commands.IdentityCommands;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
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
        readonly IMediator _mediator;
        private new readonly IToastService _toastService;
        public AuthController(
            ApplicationDbContext context,
            IUnitOfWork unitOfWork,
            IToastService toastService = null,
            IMediator mediator = null) : base(toastService, unitOfWork)
        {
            _mediator = mediator;
            _toastService = toastService;
            _mediator = mediator;
        }

        [HttpPost("/Auth/SignIn")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInUserCommand command, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult("Invalid login attempt", ToastStatus.Error);
            }

            var result = await _mediator.Send(command);

            if (string.IsNullOrEmpty(result.Item2))
            {
                var claimIdentity = new ClaimsIdentity(result.Item1, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                return ResponseResult("Logged successfully", ToastStatus.Success, ReturnUrl ?? "/dashboard");
            }
            else
            {
                return ResponseResult(result.Item2, ToastStatus.Error);
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
