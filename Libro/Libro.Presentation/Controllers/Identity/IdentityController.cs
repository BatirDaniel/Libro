using Libro.Business.Commands.IdentityCommands;
using Libro.Business.DTOs;
using Libro.Business.Queries.IdentityQueries;
using Libro.Business.Responses.IdentityResponses;
using Libro.Business.TableParameters;
using Libro.Business.Validators;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Configuration;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;

namespace Libro.Presentation.Controllers.User
{
    [System.Web.Mvc.Authorize(Roles = "Administrator")]
    public class IdentityController : Controller
    {
        public ApplicationDbContext _context;
        private readonly IMediator _mediator;
        public readonly IToastService _toastService;
        public IUnitOfWork _unitOfWork;
        public UserManager<Libro.DataAccess.Entities.User> _userManager;
        public IdentityController(IMediator mediator, ToastService toastService = null, IUnitOfWork unitOfWork = null, UserManager<DataAccess.Entities.User> userManager = null, ApplicationDbContext context = null)
        {
            _mediator = mediator;
            _toastService = toastService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _context = context;
        }

        //POST: /Identity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                //var tData = _toastService.GetToastData(ToastStatus.Error, "Invalid data");
                //ViewData["ToastData"] = tData;
                return View("Users", command);
            }

            var validate = new AddUserCommandValidator();
            var result = validate.Validate(command);

            if (!result.IsValid)
            {
                //var tData = _toastService.GetToastData(ToastStatus.Error, "There was a problem creating the user");
                //ViewData["ToastData"] = tData;
                return View("Users", command);
            }

            var result1 = await _mediator.Send(command);
            if (result1 is null)
            {
                //var tData = _toastService.GetToastData(ToastStatus.Success, "User created successfully");
                //ViewData["ToastData"] = tData;
            }
            else
            {
                //var tData = _toastService.GetToastData(ToastStatus.Error, $"{result1}");
                //ViewData["ToastData"] = tData;
            }

            return View("Users");
        }

        //POST: /Identity/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AddUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result == null ? Ok("Success") : BadRequest(result);
        }

        //DELETE: /Identity/Remove
        [HttpDelete]
        public async Task<IActionResult> Remove(string userid)
        {
            var command = new RemoveUserCommand(userid);
            var result = await _mediator.Send(command);

            return result == null ? Ok("Succes") : BadRequest(result);
        }

        //POST: /Identity/GetAutocompleteUsers
        [HttpPost("/Identity/GetUsers")]
        public async Task<IActionResult> GetUsers(DataTablesParameters param = null)
        {
            string orderColumn = param.Columns[param.Order[0].Column].Name;
            string sValue = param.Search.Value ?? "";

            int recordsTotal = 0;

            Expression<Func<Libro.DataAccess.Entities.User, bool>> expression = q =>
                q.UserName != "admin@libro" && (q.Name.Contains(sValue)
                || q.Name.Contains(sValue)
                || q.UserName.Contains(sValue)
                || q.Email.Contains(sValue)
                || q.Telephone.Contains(sValue));

            var usersQuery = _context.Users
                .Where(expression)
                .OrderByExtension(orderColumn, param.Order[0].Dir)
                .Skip(param.Start)
                .Take(param.Length)
                .Select(x => new UserResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Username = x.UserName,
                    Telephone = x.Telephone,
                    Role = _userManager.GetRolesAsync(x).Result.LastOrDefault(),
                });

            var users = usersQuery.ToList();

            recordsTotal = await usersQuery.CountAsync();

            var jsonData = new
            {
                draw = param.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = usersQuery
            };

            return Ok(jsonData);
        }

        [Route("administration/users")]
        public IActionResult Users()
        {
            return View();
        }

    }
    public static class LinqHelper
    {
        public static IQueryable<T> OrderByExtension<T>(this IQueryable<T> source, string ordering, string dir, params object[] values)
        {
            var type = typeof(T);
            var property = type.GetProperty(ordering, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), dir == "asc" ? "OrderBy" : "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
