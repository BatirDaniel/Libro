using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Mappers;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Identity
{
    [Authorize]
    public class RoleController : LibroController
    {
        private RoleManager<IdentityRole> _roleManager;
        private Mapperly _mapperly;

        public RoleController(
            IMediator mediator,
            Mapperly mapperly,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IToastService toastService,
            ApplicationDbContext context) 
            : base (toastService, unitOfWork, context, mediator)
        {
            _mapperly = mapperly;
            _roleManager = roleManager;
        }

        //GET : /Role/GetAllRoles
        [HttpGet("/roles/GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            List<Role> roles = _roleManager.Roles
                .Select(q => _mapperly.Map(q))
                .ToList();

            return Ok(roles);
        }
    }
}
