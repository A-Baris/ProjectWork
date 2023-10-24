using Microsoft.Extensions.DependencyInjection;
using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Mapping;
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
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITableOfRestaurantService, TableOfRestaurantService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IKitchenService, KitchenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISupplierService, SupplierService>();           
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IAccountingTransactionService, AccountingTransactionService>();

            //automap servise ekledik
            services.AddAutoMapper(typeof(MapProfile));
        }
    }
}
