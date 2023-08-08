using Autofac.Core;
using FluentValidation;
using FluentValidation.AspNetCore;
using Libro.Business.Commands.IdentityCommands;
using Libro.Business.Commands.IssueCommands;
using Libro.Business.Commands.PosCommands;
using Libro.Business.Managers;
using Libro.Business.Services;
using Libro.Business.Validators;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.DataAccess.Repository;
using Libro.Infrastructure.Mappers;
using Libro.Infrastructure.Persistence.SystemConfiguration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Caching
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

// Mvc
builder.Services.AddRouting();
builder.Services.AddMvc();

//Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");
});


builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Coockie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "";
        options.Cookie.Name = builder.Configuration["AppSettings:Cookie_Name"];
        options.ExpireTimeSpan = TimeSpan.FromDays(int.Parse(builder.Configuration["AppSettings:ExpireTimeSpan"]));
        options.SlidingExpiration = bool.Parse(builder.Configuration["AppSettings:SlidingExpiration"]);
    });

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

//Services

builder.Services.AddTransient<IDBDesigner, DBDesigner>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ClaimsPrincipal>(s =>
    s.GetService<IHttpContextAccessor>().HttpContext?.User ?? null);

//builder.Services.AddSingleton<SystemConfigurationService>();

builder.Services.AddScoped<IValidator<AddUserCommand>, AddUserCommandValidator>();
builder.Services.AddScoped<IValidator<CreatePosCommand>, AddPosCommandValidator>();
builder.Services.AddScoped<IValidator<CreateIssueCommand>, AddIssueCommandValidator>();

builder.Services.AddScoped<IdentityService>();

builder.Services.ConfigureWritable<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IdentityManager>();
builder.Services.AddScoped<PosManager>();
builder.Services.AddScoped<IssueManager>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var systemConfigurationService = scope.ServiceProvider.GetRequiredService<SystemConfigurationService>();

    SeedData.Seed(userManager, roleManager, dbContext, systemConfigurationService);
}

app.UseRouting();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Pages/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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