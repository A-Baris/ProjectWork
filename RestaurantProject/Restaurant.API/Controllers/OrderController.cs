using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;

namespace Restaurant.API.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IOrderService _orderService;

        public OrderController(ProjectContext context,IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }
        [HttpGet("GetBestOrders")]
    
        public IActionResult GetBestOrders(string menuName)
        {
            var query = (from o in _context.Orders
                         join p in _context.Products on o.ProductId equals p.Id
                         join m in _context.Menus on p.MenuId equals m.Id
                         where m.MenuName == menuName
                         group new { p.ProductName, o.Quantity } by new { p.Id, p.ProductName } into g
                         orderby g.Sum(x => x.Quantity) descending
                         select new
                         {
                             ProductName = g.Key.ProductName,
                             ToplamSatis = g.Sum(x => x.Quantity)
                         }).Take(10);
            var products = query.ToList();
            return Ok(products);

        }
       
        [HttpGet("GetWeeklyRevenue")]
        public IActionResult GetWeeklyRevenue()
        {
            var startDate = DateTime.Now.Date.AddDays(-7);
            var endDate = DateTime.Now.Date;
            var orders = _orderService.GetAllDeletedStatus();

            var weeklyRevenue = orders
                .Where(order => order.CreatedDate >= startDate && order.CreatedDate < endDate)
                .GroupBy(order => order.CreatedDate.DayOfWeek)
                .OrderBy(group => group.Key)
                .Select(group => new
                {
                    GunAdi = group.Key.ToString(),
                    ToplamCiro = group.Sum(order => order.TotalPrice)
                });
              
            var dataRevenue = weeklyRevenue.ToList();
            return Ok(dataRevenue);
        }
    }
}
