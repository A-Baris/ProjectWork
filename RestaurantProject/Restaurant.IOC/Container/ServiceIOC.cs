using Microsoft.Extensions.DependencyInjection;
using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Repositories;
using Restaurant.BLL.Services;
using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IOC.Container
{
    public class ServiceIOC
    {
        public static void ServiceConfigure(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IDishCategoryService, DishCategoryService>();
            services.AddScoped<IWaiterService, WaiterService>();
            services.AddScoped<ITableOfRestaurantService, TableOfRestaurantService>();
            services.AddScoped<IDishCategoryService, DishCategoryService>();
            services.AddScoped<IKitchenService, KitchenService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IDrinkCategoryService, DrinkCategoryService>();
            services.AddScoped<IDrinkService, DrinkService>();
        }
    }
}
