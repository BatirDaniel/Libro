using Libro.Infrastructure.Services.ToastHelper;
using Libro.Infrastructure.Services.ToastService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Mvc;

namespace Libro.Infrastructure.Helpers.AuthorizationHelper
{
    public class AuthorizeFilter : Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                var toastService = context.HttpContext.RequestServices.GetRequiredService<IToastService>();

                var toastData = toastService.GetToastData(ToastStatus.Error, "Please login first");

                context.HttpContext.Session.SetString("message", toastData["message"]);
                context.HttpContext.Session.SetString("svg", toastData["svg"]);
            }
        }
    }
}
