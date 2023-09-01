using Libro.Business.Libra.DTOs.CityDTOs;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.City
{
    [Authorize]
    public class CityController : LibroController
    {
        private new IUnitOfWork _unitOfWork;
        public CityController(
            IMediator mediator,
            IToastService toastService,
            IUnitOfWork unitOfWork,
            ApplicationDbContext context) 
            : base(toastService, unitOfWork, context, mediator)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /Cities/GetCities
        [HttpGet("/Cities/GetCities")]
        public async Task<IActionResult> GetCities()
        {
            var result = (await _unitOfWork.Cities.FindAll<CityDTO>(
                skip: 0,
                take: int.MaxValue,
                select: x => new CityDTO
                {
                    Id = x.Id,
                    Name = x.CityName
                })).ToList();

            return Ok(result);
        }
    }
}
