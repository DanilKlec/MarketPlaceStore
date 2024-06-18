using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewsStore.Application.Services;
using NewsStore.Core.Abstractions;
using NewsStore.DataBase;
using NewsStore.DataBase.Reposotories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    x =>
    {
        x.LoginPath = "/Home/Login";
        x.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
    });

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddScoped<ITidingsService, TidingsService>();
builder.Services.AddScoped<ITidingsRepository, TidingsRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddDbContext<TidingsStoreDbContext>(option =>
{
    option.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=NewsDB;Trusted_Connection=True;");
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=MainBase}"
    );



app.Run();
