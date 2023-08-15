using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Mappers;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers.Identity
{
    public class RoleController : Controller
    {
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly Mapperly _mapperly;

        public RoleController(Mapperly mapperly, RoleManager<IdentityRole> roleManager) 
        {
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
