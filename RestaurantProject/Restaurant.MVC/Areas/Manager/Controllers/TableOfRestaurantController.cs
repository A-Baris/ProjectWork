using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class TableOfRestaurantController : Controller
    {

        private readonly ITableOfRestaurantService _tableOfRestaurant;
        private readonly IWaiterService _waiter;

        public TableOfRestaurantController(ITableOfRestaurantService tableOfRestaurant,IWaiterService waiter )
        {

            
            _tableOfRestaurant = tableOfRestaurant;
            _waiter = waiter;
        }
        public IActionResult Index()
        {
            ViewBag.WaiterList = _waiter.GetAll();
            var tableList = _tableOfRestaurant.GetAll();
            return View(tableList);
        }

        public IActionResult Create()
        {
            ViewBag.Waiters = _waiter.GetAll().Select(w => new SelectListItem
            {
                Text = w.Name + " " + w.Surname,
                Value = w.Id.ToString()
            });
            return View();
        }
        [HttpPost]
        public IActionResult Create(TableOfRestaurantVM restaurantVM)
        {
            if (ModelState.IsValid)
            {
                TableOfRestaurant table = new TableOfRestaurant()
                {
                    TableCapacity = restaurantVM.Capacity,
                    TableLocation = restaurantVM.Location,
                    TableName = restaurantVM.TableName,
                    WaiterId = restaurantVM.WaiterId
                };
                _tableOfRestaurant.Create(table);
                return RedirectToAction("Index", "Manager");
            }
            return View(restaurantVM);
        }
    }
}
