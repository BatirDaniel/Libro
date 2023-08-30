using Autofac.Core;
using FluentValidation;
using FluentValidation.AspNetCore;
using Libro.Business.AssemblyHelper;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.DTOs.Validators.IdentityValidators;
using Libro.Business.Libra.DTOs.Validators.IssuesValidators;
using Libro.Business.Libra.DTOs.Validators.POSsValidators;
using Libro.Business.Managers;
using Libro.Business.Services;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.DataAccess.Repository;
using Libro.Infrastructure.Helpers.AuthorizationHelper;
using Libro.Infrastructure.Mappers;
using Libro.Infrastructure.Persistence.SystemConfiguration;
using Libro.Infrastructure.Persistence.SystemConfiguration.AppSettings;
using Libro.Infrastructure.Services.ToastService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddRouting();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(AuthorizeFilter));
});

builder.Services.AddOptions();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
});

builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? "");
}, ServiceLifetime.Scoped);

builder.Services.RegisterHandlers();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.Configure<IdentityOptions>(q =>
{
    q.Password.RequireDigit = false;
    q.Password.RequiredLength = 8;
    q.Password.RequireNonAlphanumeric = false;
    q.Password.RequireLowercase = false;
    q.Password.RequireUppercase = false;
});

builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/auth/Login");
        options.AccessDeniedPath = new PathString("/Errors/403");
        options.LogoutPath = "";
        options.Cookie.Name = configuration["AppSettings:Cookie_Name"];
        options.ExpireTimeSpan = TimeSpan.FromSeconds(int.Parse(configuration["AppSettings:ExpireTimeSpan"]));
        options.SlidingExpiration = bool.Parse(configuration["AppSettings:SlidingExpiration"]);
    });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; //OR other desired strategy
    options.JsonSerializerOptions.WriteIndented = true; //FOR better formatting
});

builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDBDesigner, DBDesigner>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IToastService, ToastService>();
builder.Services.AddTransient<ClaimsPrincipal>(s =>
    s.GetService<IHttpContextAccessor>().HttpContext?.User ?? null);

builder.Services.AddSingleton<SystemConfigurationService>();
builder.Services.AddSingleton<Mapperly>();

builder.Services.AddScoped<IValidator<AddUserDTO>, AddUserDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDTO>, UpdateUserDTOValidator>();
builder.Services.AddScoped<IValidator<CreateIssueDTO>, CreateIssueDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateIssueDTO>, UpdateIssueDTOValidator>();
builder.Services.AddScoped<IValidator<CreatePOSDTO>, CreatePOSDTOValidator>();
builder.Services.AddScoped<IValidator<UpdatePOSDTO>, UpdatePOSDTOValidator>();

builder.Services.AddScoped<IdentityService>();

builder.Services.ConfigureWritable<AppSettings>(configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IdentityManager>();
builder.Services.AddScoped<PosManager>();
builder.Services.AddScoped<IssueManager>();

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var systemConfigurationService = scope.ServiceProvider.GetRequiredService<SystemConfigurationService>();

    SeedData.Seed(userManager, roleManager, dbContext, systemConfigurationService).Wait();
}

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Errors/404");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources"
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Libro}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.Run();