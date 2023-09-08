using FluentValidation;
using Libro.Business.Commands.PosCommands;
using Libro.Business.Libra.Commands.PosCommands;
using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Libra.Queries.PosQueries;
using Libro.Business.Libra.Queries.POSQueries;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
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
        private new IMediator _mediator;
        private IValidator<CreatePOSDTO> _createValidator;
        private IValidator<UpdatePOSDTO> _updateValidator;
        private new ApplicationDbContext _context;
        public PosController(IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            UserManager<DataAccess.Entities.User> userManager,
            IValidator<CreatePOSDTO> createValidator,
            IValidator<UpdatePOSDTO> updateValidator,
            ApplicationDbContext context)
            : base(toastService, unitOfWork, context, mediator)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        //POST: /POS/Create
        [ValidateAntiForgeryToken]
        [HttpPost("/POS/Create")]
        public async Task<IActionResult> Create(CreatePOSDTO modelH)
        {
            var validation = await _createValidator.ValidateAsync(modelH);
            if (!validation.IsValid)
                return ResponseResult(validation.Errors.FirstOrDefault()?.ErrorMessage, ToastStatus.Error);

            var result = await _mediator.Send(new CreatePOSCommand(modelH));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /POS/Update
        [HttpPost("/POS/Update")]
        public async Task<IActionResult> Update(UpdatePOSDTO modelH)
        {
            var validation = await _updateValidator.ValidateAsync(modelH);
            if (!validation.IsValid)
                return ResponseResult(validation.Errors.FirstOrDefault()?.ErrorMessage, ToastStatus.Error);

            var result = await _mediator.Send(new UpdatePOSCommand(modelH));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success, "/pos");
        }

        //DELETE: /POS/Update
        [HttpDelete("/POS/Delete/{posId}")]
        public async Task<IActionResult> Delete(Guid posId)
        {
            var result = await _mediator.Send(new DeletePOSCommand(posId));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /POS/GetPOSs
        [HttpPost("/POS/GetPOSs")]
        public async Task<IActionResult> GetPOSs(DataTablesParameters param = null)
        {
            var result = await _mediator.Send(new GetPOSsQuery(param));
            var totalRecords = _context.POSs.ToList().Count();

            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = result
            };

            return Ok(jsonData);
        }

        //GET: pos details
        [HttpGet("/POS/GetPOSDetails/{posId}")]
        public async Task<IActionResult> GetPOSDetails(Guid posId)
        {
            var result = await _mediator.Send(new GetPOSDetailsQuery(posId));
            return Ok(result);
        }

        //POST: issues of the pos 
        [HttpPost("/POS/GetIssuesOfThePOS/{posId}")]
        public async Task<IActionResult> GetIssuesOfThePOS(DataTablesParameters param = null, string posId = null)
        {
            var result = await _mediator.Send(new GetIssuesOfPOSQuery(param, posId.ToString()));

            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = result.Count(),
                recordsTotal = result.Count(),
                data = result
            };

            return Ok(jsonData);
        }

        //GET: /POS/GetPOSById/{posId}
        [HttpGet("/POS/GetPOSById/{posId}")]
        public async Task<IActionResult> GetPOSById(Guid posId)
        {
            var result = await _mediator.Send(new GetPOSByIdQuery(posId));
            return Ok(result);
        }

        [Route("pos")]
        public IActionResult Pos()
        {
            return View();
        }

        [Route("pos/edit/{posId}")]
        public async Task<IActionResult> UpdatePos(Guid posId)
        {
            if (!await _unitOfWork.POSs.isExists(x => x.Id == posId))
                return ResponseResult("Invalid POS", ToastStatus.Error, "/pos");

            return View();
        }

        [Route("pos/details/{posId}")]
        public async Task<IActionResult> DetailsPos(Guid posId)
        {
            if (!await _unitOfWork.POSs.isExists(x => x.Id == posId))
                return ResponseResult("Invalid POS", ToastStatus.Error, "/pos");
                
            return View(await _mediator.Send(new GetPOSDetailsQuery(posId)));
        }
    }
}
