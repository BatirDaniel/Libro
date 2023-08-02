using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Persistence.SystemConfiguration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");
});

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();

builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.Cookie.Name = builder.Configuration["AppSettings:Cookie_Name"];
        option.ExpireTimeSpan = TimeSpan.FromDays(int.Parse(builder.Configuration["AppSettings:ExpireTimeSpan"]));
        option.SlidingExpiration = bool.Parse(builder.Configuration["AppSettings:SlidingExpiration"]);
    });

var app = builder.Build();

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


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Libro}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

//app.MapControllerRoute(
//        name: "auth-login",
//        pattern: "auth/login",
//        defaults: new { controller = "Auth", action = "Login" });

//app.MapControllerRoute(
//    name: "auth-register",
//    pattern: "auth/register",
//    defaults: new { controller = "Auth", action = "Register" });

app.Run();