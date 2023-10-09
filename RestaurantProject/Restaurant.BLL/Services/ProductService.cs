using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly ProjectContext _context;

        public ProductService(IRepository<Product> repository,ProjectContext context) : base(repository)
        {
            _context = context;
        }
       

        public IEnumerable<Product> GetSelectedProducts(string categoryName)
        {
            return _context.Set<Product>().Where(x=>x.Category.CategoryName == categoryName).ToList();
        }
    }
}
