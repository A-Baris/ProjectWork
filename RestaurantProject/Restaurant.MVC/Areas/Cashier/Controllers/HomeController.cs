using Microsoft.AspNetCore.Mvc;

namespace Restaurant.MVC.Areas.Cashier.Controllers
{
    [Area("Cashier")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateReservation()
        {
            return View();
        }
    }
}

