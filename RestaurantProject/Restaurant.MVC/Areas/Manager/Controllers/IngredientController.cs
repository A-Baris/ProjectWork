using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    
    public class IngredientController : AreaBaseController
    {
        private readonly IIngredientService _ingredientService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly IValidationService<IngredientVM> _validationService;

        public IngredientController(IIngredientService ingredientService, ICategoryService categoryService, ISupplierService supplierService, IMapper mapper, IValidationService<IngredientVM> validationService)
        {
            _ingredientService = ingredientService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _mapper = mapper;
            _validationService = validationService;
        }
       
        public IActionResult Index()
        {
            if (!CheckAuthorization(new[] { "admin", "manager","accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            SelectCategoryAndSupplier();
            var ingredientList = _ingredientService.GetAll();
            return View(ingredientList);
        }
        [Authorize(Roles ="employee")]
        public IActionResult StockTracking()
        {
          
            SelectCategoryAndSupplier();
            var ingredientList = _ingredientService.GetAll();
            return View(ingredientList);
        }
        public IActionResult Create()
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            SelectCategoryAndSupplier();
            return View();
        }
        [HttpPost]
        public IActionResult Create(IngredientVM ingredientVM)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(ingredientVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                SelectCategoryAndSupplier();
                return View(ingredientVM);
            }

            var ingredient = _mapper.Map<Ingredient>(ingredientVM);
            _ingredientService.Create(ingredient);
            TempData.SetSuccessMessage();
            return RedirectToAction("Index", "Ingredient", new { area = "Manager" });


        }
        public async Task<IActionResult> Update(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            SelectCategoryAndSupplier();
            var ingredient = await _ingredientService.GetbyIdAsync(id);
            if (ingredient != null)
            {
                var updateEntity = _mapper.Map<IngredientVM>(ingredient);
                return View(updateEntity);

            }
            TempData.NotFoundId();
            return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, IngredientVM updateVM)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(updateVM);
            if(errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                SelectCategoryAndSupplier();
                return View(updateVM);
            }
            var ingredient = await _ingredientService.GetbyIdAsync(id);
            if (ingredient != null)
            {
               
                    // Güncelleme yapılırken yeni toplam fiyat eski toplam fiyatla  toplanıp ve toplam adete bölünerek yeni güncel birim fiyatı ortaya çıkar.
                    updateVM.Price = ((ingredient.Quantity * ingredient.Price) + (updateVM.Price * updateVM.Quantity)) / (ingredient.Quantity + updateVM.Quantity);
                    //Güncellenen adet eski adet miktarına dahil edilerek stok kontrollü şekilde arttırılır
                    updateVM.Quantity = (ingredient.Quantity + updateVM.Quantity);
                    _mapper.Map(updateVM, ingredient);
                    _ingredientService.Update(ingredient);
                TempData.SetSuccessMessage();
                    return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
                

                
            }
            TempData.NotFoundId();
            return RedirectToAction("Index", "Ingredient", new { area = "Manager" });

        }



        public async Task<IActionResult> Remove(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var entity = await _ingredientService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _ingredientService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
            }
            TempData.NotFoundId();
            return View("index");
        }

        void SelectCategoryAndSupplier()
        {
            ViewBag.Category = _categoryService.GetAll().Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            });

            ViewBag.Supplier = _supplierService.GetAll().Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.Id.ToString(),
            });

        }
    }
}
