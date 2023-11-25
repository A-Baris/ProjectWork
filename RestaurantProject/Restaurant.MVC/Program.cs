using Restaurant.Entity.Entities;
using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Repositories;
using Restaurant.BLL.Services;
using Restaurant.DAL.Context;
using Restaurant.IOC.Container;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

using FluentValidation.AspNetCore;
using Restaurant.MVC.Validators;
using FluentValidation;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Models.Context;
using Restaurant.MVC.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews().AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddTransient(typeof(IValidationService<>), typeof(ValidationService<>));
//  DbContext
builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Identity DbContext
builder.Services.AddDbContext<UserRoleIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection2")));

//Google
builder.Services.AddAuthentication().AddGoogle(options =>
{
    var googleSection = builder.Configuration.GetSection("Google");
    options.ClientId = googleSection["ClientID"];
    options.ClientSecret = googleSection["ClientSecret"];
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "order_customer";
    options.IdleTimeout = TimeSpan.FromHours(24);
});

// Identity services
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<UserRoleIdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/Home/Login";
    x.AccessDeniedPath = "/Home/Login";
    x.Cookie = new CookieBuilder
    {
        Name = "MyRestaurantCookie"
    };
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromHours(12);
});

//Password settings
builder.Services.Configure<IdentityOptions>(x =>
{
    x.User.AllowedUserNameCharacters =
    "abcçdefgðhýijklmnoöprsþtuüvyzwxqABCDEFGÐHIÝJKLMNOÖPRSÞTUÜVYZWXQ0123456789-,_";


    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireDigit = false;
    x.Password.RequiredLength = 6;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
});

ServiceIOC.ServiceConfigure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});


app.Run();
