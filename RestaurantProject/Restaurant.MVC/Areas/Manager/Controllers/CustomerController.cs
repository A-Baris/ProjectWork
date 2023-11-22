using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        private readonly IMapper _mapper;
        private readonly IValidationService<CustomerVM> _validationService;

        public CustomerController(ICustomerService customerService,IMapper mapper, IValidationService<CustomerVM> validationService)
        {
            _customerService = customerService;
            _mapper = mapper;
            _validationService = validationService;
        }
      
        public IActionResult Index()
        {

            var customerList = _customerService.GetAll();
            return View(customerList);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerVM customerVM)
        {
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(customerVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(customerVM);
            }


            var customer = _mapper.Map<Customer>(customerVM);
            _customerService.Create(customer);
            TempData.SetSuccessMessage();

            return RedirectToAction("index", "customer", new { area = "manager" });

        }
        public async Task<IActionResult> Update(int id)
        {

            var customerEntity = await _customerService.GetbyIdAsync(id);
            if (customerEntity != null)
            {
                var updated = _mapper.Map<CustomerVM>(customerEntity);
                return View(updated);
            }
            TempData.NotFoundId();
            return View("index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(CustomerVM customerVM)
        {
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(customerVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(customerVM);
            }
            
                var customerUpdated = _mapper.Map<Customer>(customerVM);
                _customerService.Update(customerUpdated);
            TempData.SetSuccessMessage();
                return RedirectToAction("index", "customer", new { area = "manager" });
           
         
        }

        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _customerService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _customerService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "customer", new { area = "manager" });

            }
            TempData.NotFoundId();
            return View();
        }



    }
}
