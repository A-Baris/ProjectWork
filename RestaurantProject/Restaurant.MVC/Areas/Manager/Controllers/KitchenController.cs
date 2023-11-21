using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class KitchenController : Controller
    {
        private readonly IKitchenService _kitchenService;
        private readonly IMapper _mapper;
        private readonly IValidator<KitchenVM> _validator;
        private readonly IValidationService<KitchenVM> _validationService;

        public KitchenController(IKitchenService kitchenService,IMapper mapper,IValidator<KitchenVM> validator,IValidationService<KitchenVM> validationService)
        {
            _kitchenService = kitchenService;
          _mapper = mapper;
            _validator = validator;
            _validationService = validationService;
        }
        public IActionResult Index()
        {
            var kitchenList=_kitchenService.GetAll();   
            return View(kitchenList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        public IActionResult Create(KitchenVM kitchenVM)
        {
            ModelState.Clear();
            //var result = _validator.Validate(kitchenVM);
            var errors = _validationService.GetValidationErrors(kitchenVM);
           
            if(errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(kitchenVM);
            }
            
                var kitchen = _mapper.Map<Kitchen>(kitchenVM);
                _kitchenService.Create(kitchen);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "kitchen", new { area = "Manager" });
            

          
            
        }
        public async Task<IActionResult> Update(int id)
        {
            var kitchen = await _kitchenService.GetbyIdAsync(id);
           if(kitchen != null)
            {
                var updated = _mapper.Map<KitchenVM>(kitchen);
                return View(updated);
            }
            TempData.NotFoundId();
            return RedirectToAction("index", "kitchen", new { area = "Manager" });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,KitchenVM updated)
        {
            ModelState.Clear();
            var result = _validator.Validate(updated);
            var kitchen = await _kitchenService.GetbyIdAsync(id);
            if (kitchen != null)
            {
                if (result.IsValid)
                {
                    _mapper.Map(updated, kitchen);
                    _kitchenService.Update(kitchen);

                    TempData.SetSuccessMessage();
                    return RedirectToAction("index", "kitchen", new { area = "Manager" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                TempData.SetErrorMessage();
                return View(updated);
            }
            TempData.NotFoundId();
            return RedirectToAction("index", "kitchen", new { area = "Manager" });
        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _kitchenService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Restaurant.Entity.Enums.BaseStatus.Deleted;
                _kitchenService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "kitchen", new { area = "Manager" });
            }
            TempData.NotFoundId();
            return RedirectToAction("index", "kitchen", new { area = "Manager" });
        }
       


    }
}
