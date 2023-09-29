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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.User
{
    [Authorize(Roles = "Administrator")]
    public class IdentityController : LibroController
    {
        private new IMediator _mediator;
        private IValidator<AddUserDTO> _validatorCreate;
        private IValidator<UpdateUserDTO> _validatorUpdate;
        public new IToastService _toastService;
        public new ApplicationDbContext _context;
        public IdentityController(
            IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            UserManager<DataAccess.Entities.User> userManager,
            IValidator<AddUserDTO> validator,
            IValidator<UpdateUserDTO> validatorUpdate,
            ApplicationDbContext context) 
            : base(toastService, unitOfWork, context, mediator)
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
                return ResponseResult(validation.Errors.FirstOrDefault()?.ErrorMessage, ToastStatus.Error);

            var result = await _mediator.Send(new AddUserCommand(model));
            if(result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /Identity/Update
        [HttpPost("/Identity/Update")]
        public async Task<IActionResult> Update(UpdateUserDTO model)
        {
            var validation = await _validatorUpdate.ValidateAsync(model);
            if (!validation.IsValid)
                return ResponseResult(validation.Errors.FirstOrDefault()?.ErrorMessage, ToastStatus.Error);

            var result = await _mediator.Send(new UpdateUserCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success, "/administration/users");
        }

        //DELETE: /Identity/Delete/userId
        [HttpDelete("/Identity/Delete/{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
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
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(userId));
            return Ok(result);
        }

        //GET: /Identity/GetUserDetails//userId
        [HttpGet("/Identity/GetUserDetails/{userId}")]
        public async Task<IActionResult> GetUserDetails(Guid userId)
        {
            var result = await _mediator.Send(new GetUserDetailsByIdQuery(userId));
            return Ok(result);
        }

        //VIEW ROUTE: /administration/users
        [Route("administration/users")]
        public ActionResult Users()
        {
            return View();
        }

        [Route("user/edit/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id)
        {
            if (!await _unitOfWork.Users.isExists(x => x.Id == id.ToString()))
                return ResponseResult("Invalid user", ToastStatus.Error, "/administration/users");

            return View();
        }

        [Route("user/details/{id}")]
        public async Task<IActionResult> DetailsUser(Guid id)
        {
            if (!await _unitOfWork.Users.isExists(x => x.Id == id.ToString()))
                return ResponseResult("Invalid user", ToastStatus.Error, "/administration/users");

            return View();
        }
    }
}
