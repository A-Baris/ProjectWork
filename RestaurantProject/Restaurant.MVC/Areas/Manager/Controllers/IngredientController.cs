using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;

        public IngredientController(IIngredientService ingredientService,ICategoryService categoryService,ISupplierService supplierService )
        {
            _ingredientService = ingredientService;
            _categoryService = categoryService;
            _supplierService = supplierService;
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
        public IActionResult Create(IngredientCreateVM ingredientVM)
        {
            if (ModelState.IsValid)
            {
                Ingredient ingredient = new Ingredient()
                {
                    Name = ingredientVM.Name,
                    CategoryId = ingredientVM.CategoryId,
                    SupplierId = ingredientVM.SupplierId,
                    Price = ingredientVM.Price,
                    Quantity = ingredientVM.Quantity,


                };
                _ingredientService.Create(ingredient);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
            }
            SelectCategoryAndSupplier();
            return View(ingredientVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            SelectCategoryAndSupplier();
            var ingredient = await _ingredientService.GetbyIdAsync(id);
            if (ingredient != null)
            {
                var updateEntity = new IngredientUpdateVM
                {
                    Id = id,
                    Name = ingredient.Name,
                 
                    Price = ingredient.Price,
                    Quantity = ingredient.Quantity,

                };
                return View(updateEntity);
            }
            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> Update(IngredientUpdateVM updateVM)
        {
            if (ModelState.IsValid)
            {
                var ingredient = await _ingredientService.GetbyIdAsync(updateVM.Id);
                if (ingredient != null)
                {
                    ingredient.Name = updateVM.Name;
                    ingredient.CategoryId = updateVM.CategoryId;
                    ingredient.SupplierId = updateVM.SupplierId;
                    ingredient.Price = updateVM.Price;
                    ingredient.Quantity = updateVM.Quantity;
                    _ingredientService.Update(ingredient);
                    TempData["Message"] = "Successful";
                    return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
                }
            }
            SelectCategoryAndSupplier();
            return View(updateVM);


        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _ingredientService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _ingredientService.Update(entity);
                TempData["Message"] = "Successful";
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
