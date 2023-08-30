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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Issue
{
    [Authorize]
    public class IssueController : LibroController
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateIssueDTO> _createValidator;
        private readonly IValidator<UpdateIssueDTO> _updateValidator;
        private readonly ApplicationDbContext _context;

        public IssueController(IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            UserManager<DataAccess.Entities.User> userManager,
            IValidator<CreateIssueDTO> createValidator,
            IValidator<UpdateIssueDTO> updateValidator,
            ApplicationDbContext context)
            : base(toastService, unitOfWork, context)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        //POST: /Issue/Create
        [ValidateAntiForgeryToken]
        [HttpPost("/Issue/Create")]
        public async Task<IActionResult> Create(CreateIssueDTO model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return View("Issue", model);

            var result = await _mediator.Send(new CreateIssueCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Succes", ToastStatus.Success);
        }

        //PUT: /Issue/Update
        [HttpPost("/Issue/Update")]
        public async Task<IActionResult> Update(UpdateIssueDTO model)
        {
            var validation = await _updateValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return View("Issue", model);

            var result = await _mediator.Send(new UpdateIssueCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Succes", ToastStatus.Success);
        }

        //PUT: /Issue/Delete
        [HttpDelete("/Issue/Delete/{issueId}")]
        public async Task<IActionResult> Delete(string? issueId)
        {
            var result = await _mediator.Send(new DeleteIssueCommand(issueId));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Succes", ToastStatus.Success);
        }

        //POST: /Issue/GetIssues
        [HttpPost("/Issue/GetIssues")]
        public async Task<IActionResult> GetIssues(DataTablesParameters param = null)
        {
            var result = await _mediator.Send(new GetIssuesQuery(param));
            var totalRecords = _context.Issues.ToList().Count();

            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = result.Count(),
                recordsTotal = totalRecords,
                data = result
            };

            return Ok(jsonData);
        }

        //GET: /Issue/GetIssueById/{issueId}
        [HttpGet("/Issue/GetIssueById/{issueId}")]
        public async Task<IActionResult> GetIssueById(string? issueId)
        {
            var result = await _mediator.Send(new GetIssueByIdQuery(issueId));
            return Ok(result);
        }

        [Route("issue")]
        public IActionResult Issue()
        {
            return View();
        }
    }
}
