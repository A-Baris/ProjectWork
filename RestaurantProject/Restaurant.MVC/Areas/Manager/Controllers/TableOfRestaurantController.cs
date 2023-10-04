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

        public TableOfRestaurantController(ITableOfRestaurantService tableOfRestaurant, IWaiterService waiter)
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
                return RedirectToAction("tableofrestaurant", "Manager", "Index");
            }
            return View(restaurantVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Waiters = _waiter.GetAll().Select(w => new SelectListItem
            {
                Text = w.Name + " " + w.Surname,
                Value = w.Id.ToString()
            });

            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            var updated = new TableOfRestaurantVM()
            {
                Id = id,
                TableName = table.TableName,
                Capacity = table.TableCapacity,
                Location = table.TableLocation,
                WaiterId = table.WaiterId
            };
            return View(updated);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TableOfRestaurantVM updated)
        {
            if (ModelState.IsValid)
            {
                var tableUpdate = await _tableOfRestaurant.GetbyIdAsync(updated.Id);
                tableUpdate.TableName = updated.TableName;
                tableUpdate.TableCapacity = updated.Capacity;
                tableUpdate.TableLocation = updated.Location;
                tableUpdate.WaiterId = updated.WaiterId;

                _tableOfRestaurant.Update(tableUpdate);
                TempData["UpdateMessage"] = "Updated is achieved";
                return RedirectToAction("tableofrestaurant", "Manager", "Index");
            }
            else
            {
                return View(updated);
            }
        }
        public async Task<IActionResult> Remove(int id)
        {
            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            if(table!=null)
            {
                table.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _tableOfRestaurant.Update(table);
                return RedirectToAction("tableofrestaurant", "Manager", "Index");
            }
            return View();
        }

    }
}
