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
using Restaurant.MVC.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

//  DbContext
builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity DbContext
builder.Services.AddDbContext<UserRoleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection2")));

// Add Identity services
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<UserRoleContext>()
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
    x.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});

//Password settings
builder.Services.Configure<IdentityOptions>(x =>
{
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.Run();
