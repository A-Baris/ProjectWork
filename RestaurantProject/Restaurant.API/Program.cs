using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using Restaurant.IOC.Container;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

ServiceIOC.ServiceConfigure(builder.Services);

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name:"ApiRoute",
        pattern:"api/{controller}/{action}/{id?}"
        );
});
app.Run();
