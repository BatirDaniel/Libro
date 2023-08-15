using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Validators;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.User
{
    public class IdentityController : Controller
    {
        private readonly IMediator _mediator;
        public readonly ToastService _toastService;
        public IdentityController(IMediator mediator, ToastService toastService = null)
        {
            _mediator = mediator;
            _toastService = toastService;
        }

        public async Task<IActionResult> Create(AddUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                var tData = _toastService.GetToastData(ToastStatus.Error, "Invalid data");
                ViewData["ToastData"] = tData;
                return View("Users", command);
            }

            var validate = new AddUserCommandValidator();
            var result = validate.Validate(command);

            if (!result.IsValid)
            {
                var tData = _toastService.GetToastData(ToastStatus.Error, "There was a problem creating the user");
                ViewData["ToastData"] = tData;
                return View("Users", command);
            }

            var result1 = await _mediator.Send(command);
            if (result1 is null)
            {
                var tData = _toastService.GetToastData(ToastStatus.Success, "User created successfully");
                ViewData["ToastData"] = tData;
            }
            else
            {
                var tData = _toastService.GetToastData(ToastStatus.Error, $"{result1}");
                ViewData["ToastData"] = tData;
            }

            return View("Users");
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

        [Route("administration/users")]
        public IActionResult Users()
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewData["ToastData"] = _toastService.GetToastData(ToastStatus.Warning, "Please login first");
                return View("~/Views/Error-Pages/404.cshtml");
            }
            return View();
        }
    }
}
