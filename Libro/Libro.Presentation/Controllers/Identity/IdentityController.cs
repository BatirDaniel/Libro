using FluentValidation;
using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Libra.Commands.IdentityCommands;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Queries.IdentityQueries;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Id.Insert;

namespace Libro.Presentation.Controllers.User
{
    [System.Web.Mvc.Authorize(Roles = "Administrator")]
    public class IdentityController : LibroController
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddUserCommand> _validatorCreate;
        private readonly IValidator<UpdateUserCommand> _validatorUpdate;
        public new readonly IToastService _toastService;
        public IdentityController(
            IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            UserManager<DataAccess.Entities.User> userManager,
            ApplicationDbContext context,
            IValidator<AddUserCommand> validator,
            IValidator<UpdateUserCommand> validatorUpdate) : base(toastService, unitOfWork, context)
        {
            _mediator = mediator;
            _toastService = toastService;
            _validatorCreate = validator;
            _validatorUpdate = validatorUpdate;
        }

        //POST: /Identity/Create
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost("/Identity/Create")]
        public async Task<IActionResult> Create(AddUserCommand command)
        {
            var validation = await _validatorCreate.ValidateAsync(command);
            if (!validation.IsValid)
                return View("Users", command);

            var result = await _mediator.Send(command);
            if(result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /Identity/Update
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost("/Identity/Update")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            var validator = await _validatorUpdate.ValidateAsync(command);
            if (!validator.IsValid)
                return View("Users", command);

            var result = await _mediator.Send(command);
            if (result != null)
                ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //DELETE: /Identity/Delete/userId
        [AllowAnonymous]
        [HttpDelete("/Identity/Delete/{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _mediator.Send(new RemoveUserCommand(userId));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /Identity/GetUsers
        [HttpPost("/Identity/GetUsers")]
        public async Task<IActionResult> GetUsers(DataTablesParameters param = null)
        {
            var result = await _mediator.Send(new GetUsersQuery(param));

            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = result.Count(),
                recordsTotal = result.Count(),
                data = result
            };

            return Ok(jsonData);
        }

        //GET: /Identity/GetUserById/userId
        [HttpGet("/Identity/GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(string? userId)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(userId));
            return Ok(result);
        }

        //POST: /Identity/ActivateOrDezactivateUser
        [AllowAnonymous]
        [HttpPost("/Identity/UpdateStatusUser/{userId}")]
        public async Task<IActionResult> UpdateStatusUser(string userId)
        {
            var result = await _mediator.Send(new UpdateUserStatusCommand(userId));

            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //VIEW ROUTE: /administration/users
        [Route("administration/users")]
        public IActionResult Users()
        {
            return View();
        }
    }
}
