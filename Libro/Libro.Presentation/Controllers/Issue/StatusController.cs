using Libro.Business.Libra.DTOs;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Issue
{
    public class StatusController : LibroController
    {
        private new IUnitOfWork _unitOfWork;
        public StatusController(
            IToastService toastService, 
            IUnitOfWork unitOfWork,
            ApplicationDbContext context,
            IMediator mediator) 
            : base(toastService, unitOfWork, context, mediator)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /Status/GetAllStatuses
        public async Task<IActionResult> GetAllStatuses()
        {
            var result = (await _unitOfWork.Statuses.FindAll<StatusDTO>(
                skip: 0,
                take: int.MaxValue,
                select: x => new StatusDTO(
                    x.Id,
                    x.Status_Name))).ToList();

            return Ok(result);
        }
    }
}
