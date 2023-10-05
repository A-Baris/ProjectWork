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
        private readonly IKitchenService _kitchenService;
        private readonly IIngredientCategoryService _categoryService;

        public IngredientController(IIngredientService ingredientService,IKitchenService kitchenService,IIngredientCategoryService categoryService)
        {
            _ingredientService = ingredientService;
            _kitchenService = kitchenService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            ViewBag.CategoryList = _categoryService.GetAll();
            ViewBag.KitchenList= _kitchenService.GetAll();
            var ingredientList=_ingredientService.GetAll();
            return View(ingredientList);
        }
        public IActionResult Create()
        {
            CategoryAndKitchenSelect();
            return View();
        }
        [HttpPost]
        public IActionResult Create(IngredientVM vM)
        {
            if(ModelState.IsValid)
            {
                Ingredient ingredient = new Ingredient()
                {
                    IngredientName = vM.IngredientName,
                    UnitPrice = vM.UnitPrice,
                    Quantity = vM.Quantity,
                    IngredientCatgoryId = vM.IngredientCatgoryId,
                    KitchenId = vM.KitchenId,
                };
                _ingredientService.Create(ingredient);
                return RedirectToAction("Ingredient", "manager", "index");
            }
            CategoryAndKitchenSelect();
            return View(vM);
        }
        public async Task<IActionResult> Update(int id)
        {
            CategoryAndKitchenSelect();
            var ingredientEntity= await _ingredientService.GetbyIdAsync(id);
            if (ingredientEntity != null)
            {
                var updated = new IngredientVM()
                {
                    IngredientName = ingredientEntity.IngredientName,
                    UnitPrice = ingredientEntity.UnitPrice,
                    Quantity = ingredientEntity.Quantity,
                    IngredientCatgoryId = ingredientEntity.IngredientCatgoryId,
                    KitchenId = ingredientEntity.KitchenId
                };
                return View(updated);
            }
            return View();
           

        }
        [HttpPost]
        public async Task<IActionResult> Update(IngredientVM vM)
        {
            CategoryAndKitchenSelect();
            if(ModelState.IsValid)
            {
                var entity = await _ingredientService.GetbyIdAsync(vM.Id);
                if (entity != null)
                {
                    entity.IngredientName= vM.IngredientName;
                    entity.UnitPrice= vM.UnitPrice;
                    entity.Quantity= vM.Quantity;
                    entity.IngredientCatgoryId=vM.IngredientCatgoryId;
                    entity.KitchenId= vM.KitchenId;
                    _ingredientService.Update(entity);
                    return RedirectToAction("Ingredient", "manager", "index");

                }
            }
            CategoryAndKitchenSelect();
            return View(vM);
        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _ingredientService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _ingredientService.Update(entity);
                return RedirectToAction("Ingredient", "manager", "index");
            }
            return View();
        }





        void CategoryAndKitchenSelect()
        {
            ViewBag.CategorySelect = _categoryService.GetAll().Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()
            });
            ViewBag.KitchenSelect = _kitchenService.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitchenName,
                Value = k.Id.ToString()
            });

        }
    }
}
