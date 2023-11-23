using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "admin,manager")]
    public class CategoryController : AreaBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidationService<CategoryVm> _validationService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IValidationService<CategoryVm> validationService, IMapper mapper)
        {
            _categoryService = categoryService;
            _validationService = validationService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var categoryList = _categoryService.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryVm categoryVm)
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(categoryVm);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(categoryVm);
            }
            var category = _mapper.Map<Category>(categoryVm);
            _categoryService.Create(category);
            TempData.SetSuccessMessage();
            return RedirectToAction("index", "category", new { area = "Manager" });


        }
        public async Task<IActionResult> Update(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var category = await _categoryService.GetbyIdAsync(id);
            if (category != null)
            {
                var updated = _mapper.Map<CategoryVm>(category);

                return View(updated);
            }
            TempData.NotFoundId();
            return RedirectToAction("index", "category", new { area = "Manager" });

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryVm updated)
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(updated);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(updated);
            }
           
                var category = await _categoryService.GetbyIdAsync(id);
            if(category!=null)
            {
                category = _mapper.Map(updated, category);
                _categoryService.Update(category);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "category", new { area = "Manager" });
            }
            TempData.NotFoundId();
            return RedirectToAction("index", "category", new { area = "Manager" });

        }
        public async Task<IActionResult> Remove(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var entity = await _categoryService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _categoryService.Update(entity);
                TempData["Message"] = "İşlem başarılı";
                return RedirectToAction("index", "category", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "Id bulunamadı";
            return View("index");
        }
    }
}
