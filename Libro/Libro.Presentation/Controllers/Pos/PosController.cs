using FluentValidation;
using Libro.Business.Commands.PosCommands;
using Libro.Business.Libra.Commands.PosCommands;
using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Libra.Queries.PosQueries;
using Libro.Business.Libra.Queries.POSQueries;
using Libro.DataAccess.Contracts;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Pos
{
    [Authorize]
    public class PosController : LibroController
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreatePOSDTO> _createValidator;
        private readonly IValidator<UpdatePOSDTO> _updateValidator;
        public PosController(IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            UserManager<DataAccess.Entities.User> userManager,
            IValidator<CreatePOSDTO> createValidator,
            IValidator<UpdatePOSDTO> updateValidator)
            : base(toastService, unitOfWork)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        //POST: /POS/Create
        [ValidateAntiForgeryToken]
        [HttpPost("/POS/Create")]
        public async Task<IActionResult> Create(CreatePOSDTO model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return View("Pos", model);

            var result = await _mediator.Send(new CreatePOSCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //PUT: /POS/Update
        [HttpPut("/POS/Update")]
        public async Task<IActionResult> Update(UpdatePOSDTO model)
        {
            var validation = await _updateValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return View("Pos", model);

            var result = await _mediator.Send(new UpdatePOSCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //PUT: /POS/Update
        [HttpDelete("/POS/Delete/{posId}")]
        public async Task<IActionResult> Delete(string posId)
        {
            var result = await _mediator.Send(new DeletePOSCommand(posId));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /POS/GetPOSs
        [HttpPost("/POS/GetPOSs")]
        public async Task<IActionResult> GetIssues(DataTablesParameters param = null)
        {
            var result = await _mediator.Send(new GetPOSsQuery(param));

            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = result.Count(),
                recordsTotal = result.Count(),
                data = result
            };

            return Ok(jsonData);
        }

        //POS: /POS/GetPOSById/{posId}
        [HttpGet("/POS/GetPOSById/{posId}")]
        public async Task<IActionResult> GetPOSById(string posId)
        {
            var result = await _mediator.Send(new GetPOSByIdQuery(posId));
            return Ok(result);
        }

        [Route("pos")]
        public IActionResult Pos()
        {
            return View();
        }
    }
}
