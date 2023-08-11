using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Mappers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Identity
{
    public class RoleController : LibroController
    {
        public readonly IMediator _mediator;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly Mapperly _mapperly;
        public RoleController(
            IMediator mediator, 
            Mapperly mapperly, 
            RoleManager<IdentityRole> roleManager) : base(mediator)
        {
            _mediator = mediator;
            _mapperly = mapperly;
            _roleManager = roleManager;
        }

        [HttpGet("roles/GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            List<UserTypes> roles = _roleManager.Roles
                .Select(q => _mapperly.Map(q))
                .ToList();

            return Ok(roles);
        }
    }
}
