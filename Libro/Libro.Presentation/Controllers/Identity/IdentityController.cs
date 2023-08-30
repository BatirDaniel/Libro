using FluentValidation;
using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Libra.Queries.IdentityQueries;
using Libro.Business.Queries.IdentityQueries;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
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
        public readonly ApplicationDbContext _context;
        public IdentityController(
            IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            UserManager<DataAccess.Entities.User> userManager,
            IValidator<AddUserDTO> validator,
            IValidator<UpdateUserDTO> validatorUpdate,
            ApplicationDbContext context) 
            : base(toastService, unitOfWork, context)
        {
            _mediator = mediator;
            _toastService = toastService;
            _validatorCreate = validator;
            _validatorUpdate = validatorUpdate;
            _context = context;
        }

        //POST: /Identity/Create
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
        [HttpPost("/Identity/Update")]
        public async Task<IActionResult> Update(UpdateUserDTO model)
        {
            var validator = await _validatorUpdate.ValidateAsync(model);
            if (!validator.IsValid)
                return View("Users", model);

            var result = await _mediator.Send(new UpdateUserCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success, "/administration/users");
        }

        //DELETE: /Identity/Delete/userId
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
            var totalRecords = _context.Users.ToList().Count();

            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = result.Count(),
                recordsTotal = totalRecords-1,
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

        //GET: /Identity/GetUserDetails//userId
        [HttpGet("/Identity/GetUserDetails/{userId}")]
        public async Task<IActionResult> GetUserDetails(string? userId)
        {
            var result = await _mediator.Send(new GetUserDetailsByIdQuery(userId));
            return Ok(result);
        }

        //VIEW ROUTE: /administration/users
        [Route("administration/users")]
        public IActionResult Users()
        {
            return View();
        }

        [Route("edit/{userId}")]
        public IActionResult UpdateUser(string? userId)
        {
            if(GetUserById(userId) != null)
                return View();

            return RedirectToAction("Users");
        }

        [Route("details/{userId}")]
        public IActionResult DetailsUser(string? userId)
        {
            if (GetUserById(userId) != null)
                return View();

            return RedirectToAction("Users");
        }
    }
}
