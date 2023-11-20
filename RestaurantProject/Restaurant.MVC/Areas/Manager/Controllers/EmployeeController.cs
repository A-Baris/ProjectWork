using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Data;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;

        public EmployeeController(IEmployeeService employeeService,IMapper mapper,RoleManager<AppRole> roleManager)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
           var employees = _employeeService.GetAll();
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                 
                var employee = _mapper.Map<Employee>(employeeVM);
                _employeeService.Create(employee);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "employee", new {area="manager"});

            }
            TempData["ErrorMessage"] = "ModelState is valid";
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            return View(employeeVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            var employee = await _employeeService.GetbyIdAsync(id);
          if(employee!=null)
            {
                var updated = _mapper.Map<EmployeeVM>(employee);
                return View(updated);

            }
            TempData["ErrorMessage"] = $"{id} is not found";
          return View("index");
           
        }
        [HttpPost]
        public async  Task<IActionResult> Update(int id,EmployeeVM updated)
        {
            var employee = await _employeeService.GetbyIdAsync(id);
            if (employee != null)
            {


                if (ModelState.IsValid)
                {

                    _mapper.Map(updated, employee);
                    _employeeService.Update(employee);
                    TempData["Message"] = "Successful";
                    return RedirectToAction("Index", "employee", new { area = "manager" });

                }
                TempData["ErrorMessage"] = "ModelState is valid";
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(updated);
            }
            TempData["ErrorMessage"] = "Employee is not found";
            return View("index");




        }

        public async Task<IActionResult> Remove(int id)
        {
            var employee = await _employeeService.GetbyIdAsync(id);
            if(employee != null)
            {
                employee.BaseStatus=Entity.Enums.BaseStatus.Deleted;
                _employeeService.Update(employee);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "employee", new { area = "manager" });

            }
            return View();
        }


        
    }
}
