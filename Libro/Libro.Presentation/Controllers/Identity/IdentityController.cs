using FluentValidation;
using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Libra.Commands.IdentityCommands;
using Libro.Business.Libra.DTOs.IdentityDTOs;
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

namespace Libro.Presentation.Controllers.User
{
    [System.Web.Mvc.Authorize(Roles = "Administrator")]
    public class IdentityController : LibroController
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddUserDTO> _validatorCreate;
        private readonly IValidator<UpdateUserDTO> _validatorUpdate;
        public readonly IToastService _toastService;
        public IdentityController(
            IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            UserManager<DataAccess.Entities.User> userManager,
            IValidator<AddUserDTO> validator,
            IValidator<UpdateUserDTO> validatorUpdate) : base(toastService, unitOfWork)
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
        public async Task<IActionResult> Create(AddUserDTO model)
        {
            var validation = await _validatorCreate.ValidateAsync(model);
            if (!validation.IsValid)
                return View("Users", model);

            var result = await _mediator.Send(new AddUserCommand(model));
            if(result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /Identity/Update
        [AllowAnonymous]
        [HttpPost("/Identity/Update")]
        public async Task<IActionResult> Update(UpdateUserDTO model)
        {
            var validator = await _validatorUpdate.ValidateAsync(model);
            if (!validator.IsValid)
                return View("Users", model);

            var result = await _mediator.Send(new UpdateUserCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //DELETE: /Identity/Delete/userId
        [AllowAnonymous]
        [HttpDelete("/Identity/Delete/{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand(userId));
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

        [Route("details/{userId}")]
        public IActionResult Details(string? userId)
        {
            if(GetUserById(userId) != null)
                return View();

            return ResponseResult("Invalid user provided", ToastStatus.Error);
        }
    }
}
