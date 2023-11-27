using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;

using Restaurant.IOC.Container;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder
            .WithOrigins("https://localhost:7152")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


ServiceIOC.ServiceConfigure(builder.Services);

var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyCorsPolicy");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name:"ApiRoute",
        pattern:"api/{controller}/{action}/{id?}"
        );
});

// Configure method

app.Run();
