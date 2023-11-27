using Restaurant.Entity.Entities;
using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Repositories;
using Restaurant.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
     
        public CategoryService(IRepository<Category> repository) : base(repository)
        {
        }
    }
}
