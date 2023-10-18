using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        public IActionResult Index()
        {
            var ingredientList = _ingredientService.GetAll();
            return View(ingredientList);
        }
        public IActionResult Create()
        {
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
                    Category = ingredientVM.Category,
                    Price = ingredientVM.Price,
                    Quantity = ingredientVM.Quantity,


                };
                _ingredientService.Create(ingredient);
                return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
            }
            return View(ingredientVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            var ingredient = await _ingredientService.GetbyIdAsync(id);
            if (ingredient != null)
            {
                var updateEntity = new IngredientUpdateVM
                {
                    Id = id,
                    Name = ingredient.Name,
                    Category = ingredient.Category,
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
                    ingredient.Category = updateVM.Category;
                    ingredient.Price = updateVM.Price;
                    ingredient.Quantity = updateVM.Quantity;
                    _ingredientService.Update(ingredient);
                    return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
                }
            }
            return View(updateVM);


        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _ingredientService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _ingredientService.Update(entity);
                return RedirectToAction("Index", "Ingredient", new { area = "Manager" });
            }
            return View();
        }
        
    }
}
