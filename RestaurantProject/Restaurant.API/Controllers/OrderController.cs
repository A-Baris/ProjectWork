using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;

namespace Restaurant.API.Controllers
{
   
    public class OrderController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IOrderService _orderService;

        public OrderController(ProjectContext context,IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult GetBestOrders()
        {
            var query = (from order in _context.Orders
                         join product in _context.Products on order.ProductId equals product.Id
                         group order.Quantity by product.ProductName into grouped
                         orderby grouped.Sum() descending
                         select new
                         {
                             ProductName = grouped.Key,
                             ToplamSiparis = grouped.Sum()
                         }).Take(10);

            return Ok(query);

        }
        [HttpGet]
        public IActionResult GetWeeklyRevenue()
        {
            var startDate = DateTime.Now.Date.AddDays(-7);
            var endDate = DateTime.Now.Date;
            var orders = _orderService.GetAllDeletedStatus();

            var weeklyRevenue =orders
                .Where(order => order.CreatedDate >= startDate && order.CreatedDate < endDate)
                .GroupBy(order => order.CreatedDate.DayOfWeek)
                .Select(group => new
                {
                    GunAdi = group.Key.ToString(),
                    ToplamCiro = group.Sum(order => order.TotalPrice)
                })
                .OrderBy(result => result.GunAdi);
            var dataRevenue = weeklyRevenue.ToList();
            return Ok(dataRevenue);
        }
    }
}
