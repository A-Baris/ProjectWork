using Restaurant.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.MVC.Models;
using System.Diagnostics;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDishCategoryService _dishCategory;

        public HomeController(ILogger<HomeController> logger,IDishCategoryService dishCategory  )
        {
            _logger = logger;
            _dishCategory = dishCategory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var dishCategoryList = _dishCategory.GetAll();

            return View(dishCategoryList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
