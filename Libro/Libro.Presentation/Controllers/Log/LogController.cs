using FluentValidation;
using Libro.Business.Commands.IssueCommands;
using Libro.Business.Libra.Commands.IssueCommands;
using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Libra.Queries.IssueQueries;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Log
{
    public class LogController : LibroController
    {
        private new IMediator _mediator;
        //private IValidator<> _validator;
        private new ApplicationDbContext _context;

        public LogController(
            IToastService toastService,
            IUnitOfWork unitOfWork,
            ApplicationDbContext context,
            IMediator mediator)
            : base(toastService, unitOfWork, context, mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        //POST: /Log/Create
        [ValidateAntiForgeryToken]
        [HttpPost("/Log/Create")]
        public async Task<IActionResult> Create(CreateIssueDTO model)
        {
            //var validation = await _createValidator.ValidateAsync(model);
            //if (!validation.IsValid)
            //    return ResponseResult(validation.Errors.FirstOrDefault()?.ErrorMessage, ToastStatus.Error);

            var result = await _mediator.Send(new CreateIssueCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success, "/issue");
        }

        //PUT: /Issue/Update
        [HttpPost("/Log/Update")]
        public async Task<IActionResult> Update(UpdateIssueDTO model)
        {
            //var validation = await _updateValidator.ValidateAsync(model);
            //if (!validation.IsValid)
            //    return ResponseResult(validation.Errors.FirstOrDefault()?.ErrorMessage, ToastStatus.Error);

            var result = await _mediator.Send(new UpdateIssueCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success, "/issue");
        }

        //PUT: /Issue/Delete
        [HttpDelete("/Log/Delete/{issueId}")]
        public async Task<IActionResult> Delete(Guid issueId)
        {
            var result = await _mediator.Send(new DeleteIssueCommand(issueId));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /Issue/GetIssues
        [HttpPost("/Log/GetLogs")]
        public async Task<IActionResult> GetIssues(DataTablesParameters param = null)
        {
            var result = await _mediator.Send(new GetIssuesQuery(param));
            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = result.Count(),
                recordsTotal = result.Count(),
                data = result
            };

            return Ok(jsonData);
        }

        [Route("/logs")]
        public IActionResult Logs()
        {
            return View();
        }

        [Route("log/update")]
        public IActionResult Update()
        {
            return View();
        }

        [Route("log/details")]
        public IActionResult DetailsLog()
        {
            return View();
        }
    }
}
