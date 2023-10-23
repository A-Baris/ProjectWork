using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using System.Text.RegularExpressions;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class AccountingController : Controller
    {
        private readonly IOrderItemService _orderItemService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly ProjectContext _context;

        public AccountingController(IOrderItemService orderItemService,ITableOfRestaurantService tableOfRestaurantService,ProjectContext context)
        {
            _orderItemService = orderItemService;
           _tableOfRestaurantService = tableOfRestaurantService;
            _context = context;
        }
        public IActionResult Index()
        {
         return View();
        }

        public IActionResult Report(int targetYear, int targetMonth)
        {
//            SELECT DATEPART(WEEKDAY, CreatedDate),SUM(TotalPrice)
//FROM OrderItems where DATEPART(MONTH, CreatedDate) = DATEPART(MONTH, '2023-10-22') group by DATEPART(WEEKDAY, CreatedDate)


            var orderItemsForMonth = _context.OrderItems
                .Where(item => item.CreatedDate.Year == targetYear && item.CreatedDate.Month == targetMonth)
                .ToList(); 

            var weeklyTotalPrices = orderItemsForMonth
                .GroupBy(item => item.CreatedDate.Date)
                .Select(group => new
                {
                    DayOfYear = group.Key,
                    TotalPriceSum = group.Sum(item => item.TotalPrice)
                })
                .ToList();

            ViewBag.Weekly = weeklyTotalPrices.ToList();


            return View(weeklyTotalPrices);
        }
    }
}
