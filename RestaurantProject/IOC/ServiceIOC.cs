﻿using Microsoft.Extensions.DependencyInjection;
using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Repositories;
using Restaurant.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC
{
    public class ServiceIOC
    {
        public static void ServiceConfigure(IServiceCollection services)
        {
           services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
           services.AddScoped<IDishCategoryService, DishCategoryService>();
        }
    }
}