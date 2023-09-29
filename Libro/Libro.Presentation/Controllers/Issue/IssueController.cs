using FluentValidation;
using Libro.Business.Commands.IssueCommands;
using Libro.Business.Libra.Commands.IssueCommands;
using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.DTOs.POSDTOs;
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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Libro.Presentation.Controllers.Issue
{
    [Authorize]
    public class IssueController : LibroController
    {
        private new IMediator _mediator;
        private IValidator<CreateIssueDTO> _createValidator;
        private IValidator<UpdateIssueDTO> _updateValidator;
        private new ApplicationDbContext _context;

        public IssueController(IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            UserManager<DataAccess.Entities.User> userManager,
            IValidator<CreateIssueDTO> createValidator,
            IValidator<UpdateIssueDTO> updateValidator,
            ApplicationDbContext context)
            : base(toastService, unitOfWork, context, mediator)
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
                return ResponseResult(validation.Errors.FirstOrDefault()?.ErrorMessage, ToastStatus.Error);

            var result = await _mediator.Send(new CreateIssueCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success, "/issue");
        }

        //PUT: /Issue/Update
        [HttpPost("/Issue/Update")]
        public async Task<IActionResult> Update(UpdateIssueDTO model)
        {
            var validation = await _updateValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return ResponseResult(validation.Errors.FirstOrDefault()?.ErrorMessage, ToastStatus.Error);

            var result = await _mediator.Send(new UpdateIssueCommand(model));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success, "/issue");
        }

        //PUT: /Issue/Delete
        [HttpDelete("/Issue/Delete/{issueId}")]
        public async Task<IActionResult> Delete(Guid issueId)
        {
            var result = await _mediator.Send(new DeleteIssueCommand(issueId));
            if (result != null)
                return ResponseResult(result, ToastStatus.Error);

            return ResponseResult("Success", ToastStatus.Success);
        }

        //POST: /Issue/GetIssues
        [HttpPost("/Issue/GetIssues")]
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

        //POST: /Issue/GetPOSsFromForIssues
        [HttpPost("/Issue/GetPOSsFromForIssues")]
        public async Task<IActionResult> GetPOSsFromForIssues(DataTablesParameters param = null)
        {
            var result = await _mediator.Send(new GetPOSsForIssuesQuery(param));
            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = result.Count(),
                recordsTotal = result.Count(),
                data = result
            };

            return Ok(jsonData);
        }

        //GET: /Issue/GetIssueById/{issueId}
        [HttpGet("/Issue/GetIssueById/{issueId}")]
        public async Task<IActionResult> GetIssueById(Guid issueId)
        {
            var result = await _mediator.Send(new GetIssueByIdQuery(issueId));
            return Ok(result);
        }

        //GET: /Issue/GetAllUsersForIssue
        [HttpGet("/Issue/GetAllUsersForIssue")]
        public async Task<IActionResult> GetAllUsersForIssue()
        {
            var result = await _mediator.Send(new GetAllUsersForIssueQuery());
            return Ok(result);
        }

        [Route("issue/update/{id}")]
        public async Task<IActionResult> UpdateIssue(Guid id)
        {
            if (!await _unitOfWork.POSs.isExists(x => x.Id == id))
                return ResponseResult("Invalid issue", ToastStatus.Error, "/issues/view");

            return View();
        }

        [Route("issue/details/{id}")]
        public async Task<IActionResult> DetailsIssue(Guid id)
        {
            if (!await _unitOfWork.Issues.isExists(x => x.Id == id))
                return ResponseResult("Invalid issue", ToastStatus.Error, "/issues/view");

            return View();
        }

        [Route("issues/add")]
        public ActionResult AddIssue(CreateIssueDTO model)
        {
            return View(model);
        }

        [Route("issues/view")]
        public ActionResult ViewIssues()
        {
            return View();
        }
    }
}
