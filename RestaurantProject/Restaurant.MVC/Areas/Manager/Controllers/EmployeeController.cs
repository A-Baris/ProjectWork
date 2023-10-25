using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            ViewBag.waiterList = _employeeService.GetAll();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                Employee employee= new Employee()
                {
                    Name = employeeVM.Name,
                    Surname = employeeVM.Surname,
                    Phone = employeeVM.Phone,
                    TcNo = employeeVM.TcNo,
                    Title= employeeVM.Title,
                    Notes = employeeVM.Notes,
                };
                _employeeService.Create(employee);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "Employee");  

            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            var employee = await _employeeService.GetbyIdAsync(id);
            var updated = new EmployeeVM
            {
                Id = id,
                Name = employee.Name,
                Surname = employee.Surname,
                Phone = employee.Phone,
                TcNo = employee.TcNo,
                Title= employee.Title,
                Notes = employee.Notes,
            };
            return View(updated);
        }
        [HttpPost]
        public async  Task<IActionResult> Update(EmployeeVM updated)
        {
            if (ModelState.IsValid)
            {
                var employeeUpdated = await _employeeService.GetbyIdAsync(updated.Id);
                employeeUpdated.Name = updated.Name;
                employeeUpdated.Surname = updated.Surname;
                employeeUpdated.Phone = updated.Phone;
                employeeUpdated.TcNo = updated.TcNo;
                employeeUpdated.Title = updated.Title;
                employeeUpdated.Notes = updated.Notes;

                _employeeService.Update(employeeUpdated);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index","Manager");

            }
            else
            {
                return View(updated);
            }
            
        }

        public async Task<IActionResult> Remove(int id)
        {
            var employee = await _employeeService.GetbyIdAsync(id);
            if(employee != null)
            {
                employee.BaseStatus=Entity.Enums.BaseStatus.Deleted;
                _employeeService.Update(employee);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "Manager");

            }
            return View();
        }


        
    }
}
