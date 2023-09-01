using Libro.Business.Libra.DTOs.ConnectionTypesDTOs;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.ConnectionType
{
    [Authorize]
    public class ConnectionTypeController : LibroController
    {
        private new IUnitOfWork _unitOfWork;
        public ConnectionTypeController(
            IToastService toastService,
            IUnitOfWork unitOfWork,
            ApplicationDbContext context,
            IMediator mediator)
            : base(toastService, unitOfWork, context, mediator)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /ConnectionTypes/GetConnectionTypes
        [HttpGet("/ConnectionTypes/GetConnectionTypes")]
        public async Task<IActionResult> GetConnectionTypes()
        {
            var result = (await _unitOfWork.ConnectionTypes.FindAll<ConnectionTypeDTO>(
                skip: 0,
                take: int.MaxValue,
                select: x => new ConnectionTypeDTO
                {
                    Id = x.Id,
                    Type = x.ConnectionType
                })).ToList();

            return Ok(result);
        }
    }
}
