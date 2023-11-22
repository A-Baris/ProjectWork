using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Data;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IValidationService<EmployeeVM> _validationService;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, RoleManager<AppRole> roleManager, IValidationService<EmployeeVM> validationService)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _roleManager = roleManager;
            _validationService = validationService;
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
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(employeeVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(employeeVM);


            }


            var employee = _mapper.Map<Employee>(employeeVM);
            _employeeService.Create(employee);
            TempData.SetSuccessMessage();
            return RedirectToAction("Index", "employee", new { area = "manager" });



        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            var employee = await _employeeService.GetbyIdAsync(id);
            if (employee != null)
            {
                var updated = _mapper.Map<EmployeeVM>(employee);
                return View(updated);

            }
            TempData.NotFoundId();
            return View("index");

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, EmployeeVM updated)
        {
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(updated);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState,errors);
                TempData.SetErrorMessage();
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(updated);

            }

               var employee = await _employeeService.GetbyIdAsync(id);
            if (employee != null)
            {


                
                    _mapper.Map(updated, employee);
                    _employeeService.Update(employee);
                   TempData.SetSuccessMessage();
                    return RedirectToAction("Index", "employee", new { area = "manager" });

          
               
            }
            TempData.NotFoundId();
            return View("index");




        }

        public async Task<IActionResult> Remove(int id)
        {
            var employee = await _employeeService.GetbyIdAsync(id);
            if (employee != null)
            {
                employee.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _employeeService.Update(employee);
                TempData.SetSuccessMessage();
                return RedirectToAction("Index", "employee", new { area = "manager" });

            }
            TempData.NotFoundId();
            return View("index");
        }



    }
}
