using Libro.Business.Libra.DTOs.PriorityDTOs;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Issue
{
    public class PriorityController : LibroController
    {
        private new IUnitOfWork _unitOfWork;
        public PriorityController(
            IToastService toastService,
            IUnitOfWork unitOfWork,
            ApplicationDbContext context,
            IMediator mediator) 
            : base(toastService, unitOfWork, context, mediator)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /Issue/GetAllPriorities
        [HttpGet("/Issue/GetAllPriorities")]
        public async Task<IActionResult> GetAllPriorities()
        {
            var result = (await _unitOfWork.Priorities.FindAll<PriorityDTO>(
                skip: 0,
                take: int.MaxValue,
                select: x => new PriorityDTO(
                    x.Id,
                    x.Name))).ToList();

            return Ok(result);
        }
    }
}
