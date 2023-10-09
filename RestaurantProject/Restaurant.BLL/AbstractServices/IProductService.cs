using Restaurant.BLL.AbstractRepositories;
using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.AbstractServices
{
    public interface IProductService:IRepository<Product>
    {
        IEnumerable<Product> GetSelectedProducts(string categoryName);
    }
}
