using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientService ingredientService,ICategoryService categoryService,ISupplierService supplierService,IMapper mapper )
        {
            _ingredientService = ingredientService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            SelectCategoryAndSupplier();
            var ingredientList = _ingredientService.GetAll();
            return View(ingredientList);
        }
        public IActionResult Create()
        {
            SelectCategoryAndSupplier();
            return View();
        }
        [HttpPost]
        public IActionResult Create(IngredientVM ingredientVM)
        {
            if (ModelState.IsValid)
            {
                var ingredient = _mapper.Map<Ingredient>(ingredientVM );
                _ingredientService.Create(ingredient);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            SelectCategoryAndSupplier();
            return View(ingredientVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            SelectCategoryAndSupplier();
            var ingredient = await _ingredientService.GetbyIdAsync(id);
            if (ingredient != null)
            {
                var updateEntity = _mapper.Map<IngredientVM>(ingredient);
                return View(updateEntity);

            }
            TempData["ErrorMessage"] = "Ingredient is not found";
            return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, IngredientVM updateVM)
        {
            var ingredient = await _ingredientService.GetbyIdAsync(id);
            if (ingredient != null)
            {
                if (ModelState.IsValid)
                {
                    // Güncelleme yapılırken yeni toplam fiyat eski toplam fiyatla  toplanıp ve toplam adete bölünerek yeni güncel birim fiyatı ortaya çıkar.
                    updateVM.Price = ((ingredient.Quantity * ingredient.Price) + (updateVM.Price * updateVM.Quantity)) / (ingredient.Quantity + updateVM.Quantity); 
                    //Güncellenen adet eski adet miktarına dahil edilerek stok kontrollü şekilde arttırılır
                    updateVM.Quantity = (ingredient.Quantity + updateVM.Quantity);
                    _mapper.Map(updateVM, ingredient);
                    _ingredientService.Update(ingredient);
                    TempData["Message"] = "Updated";
                    return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
                }

                TempData["ErrorMessage"] = "ModelState is invalid";
                SelectCategoryAndSupplier();
                return View(updateVM);
            }
            TempData["ErrorMessage"] = "Id is not found";
            return RedirectToAction("Index", "Ingredient", new { area = "Manager" });

        }


    
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _ingredientService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _ingredientService.Update(entity);
                TempData["Message"] = "Deleted";
                return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
            }
            return View();
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
