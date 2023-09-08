using Libro.Business.Libra.DTOs.IssueTypes;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Issue
{
    public class IssueTypesController : LibroController
    {
        private new IUnitOfWork _unitOfWork;
        public IssueTypesController(
            IToastService toastService,
            IUnitOfWork unitOfWork,
            ApplicationDbContext context,
            IMediator mediator) 
            : base(toastService, unitOfWork, context, mediator)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /IssuesTypes/GetAllIssueTypes
        [HttpGet("/IssuesTypes/GetAllIssueTypes")]
        public async Task<IActionResult> GetAllIssueTypes()
        {
            var result = (await _unitOfWork.IssueTypes.FindAll<IssueTypesDTO>(
                skip: 0,
                take: int.MaxValue,
                select: x => new IssueTypesDTO
                (
                    x.Id,
                    x.Name
                ))).ToList();

            return Ok(result);
        }
    }
}
