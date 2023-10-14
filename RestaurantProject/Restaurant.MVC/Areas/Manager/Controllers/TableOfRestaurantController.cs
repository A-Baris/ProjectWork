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
        private readonly IEmployeeService _employee;

        public TableOfRestaurantController(ITableOfRestaurantService tableOfRestaurant, IEmployeeService employee)
        {


            _tableOfRestaurant = tableOfRestaurant;
            _employee = employee;
        }
        public IActionResult Index()
        {
            ViewBag.EmployeeList = _employee.GetAll();
            var tableList = _tableOfRestaurant.GetAll();
            return View(tableList);
        }

        public IActionResult Create()
        {
            EmployeeList();
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
                    EmployeeId = restaurantVM.EmployeId
                };
                _tableOfRestaurant.Create(table);
                return RedirectToAction("index", "tableodrestaurant", new {area="Manager"});
            }
            EmployeeList();
            return View(restaurantVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            EmployeeList();
            

            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            var updated = new TableOfRestaurantVM()
            {
                Id = id,
                TableName = table.TableName,
                Capacity = table.TableCapacity,
                Status = table.Status,
                Location = table.TableLocation,
                EmployeId = table.EmployeeId
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
                tableUpdate.Status = updated.Status;
                tableUpdate.TableLocation = updated.Location;
                tableUpdate.EmployeeId = updated.EmployeId;

                _tableOfRestaurant.Update(tableUpdate);
                TempData["UpdateMessage"] = "Updated is achieved";
                return RedirectToAction("index", "tableodrestaurant", new { area = "Manager" });
            }
            EmployeeList();
            return View(updated);
            
        }
        public async Task<IActionResult> Remove(int id)
        {
            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            if(table!=null)
            {
                table.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _tableOfRestaurant.Update(table);
                return RedirectToAction("index", "tableodrestaurant", new { area = "Manager" });
            }
            return View();
        }
        void EmployeeList()
        {
            ViewBag.Employees = _employee.GetAll().Select(w => new SelectListItem
            {
                Text = w.Name + " " + w.Surname,
                Value = w.Id.ToString()
            });
        }

    }
}
