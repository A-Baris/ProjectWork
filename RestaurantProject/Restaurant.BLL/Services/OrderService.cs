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
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly ProjectContext _context;

        public OrderService(IRepository<Order> repository,ProjectContext context) : base(repository)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllDeletedStatus()
        {
            return _context.Set<Order>().Where(x=>x.BaseStatus == Entity.Enums.BaseStatus.Deleted).ToList();
        }
    }
}
