using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class IngredientCategoryController : Controller
    {
        private readonly IIngredientCategoryService _categoryService;

        public IngredientCategoryController(IIngredientCategoryService categoryService)
        {
           _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categoryList= _categoryService.GetAll();
            return View(categoryList);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(IngredientCategoryVM categoryVM)
        {
            if(ModelState.IsValid)
            {
                IngredientCategory category = new IngredientCategory()
                {
                    CategoryName = categoryVM.CategoryName,
                    Description = categoryVM.Description
                };
                _categoryService.Create(category);
                return RedirectToAction("IngredientCategory", "manager", "Index");
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var categoryEntity=await _categoryService.GetbyIdAsync(id);
            if(categoryEntity != null)
            {
                var updated = new IngredientCategoryVM()
                {
                    Id = id,
                    CategoryName = categoryEntity.CategoryName,
                    Description = categoryEntity.Description
                }; 
                return View(updated);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(IngredientCategoryVM categoryVM)
        {
            if(ModelState.IsValid)
            {
                var entity = await _categoryService.GetbyIdAsync(categoryVM.Id);
                if(entity!=null)
                {
                    entity.Id = categoryVM.Id;
                    entity.CategoryName = categoryVM.CategoryName;
                    entity.Description = categoryVM.Description;
                }
                _categoryService.Update(entity);
                return RedirectToAction("IngredientCategory", "manager", "Index");

            }
            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _categoryService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _categoryService.Update(entity);
                return RedirectToAction("IngredientCategory", "manager", "Index");
            }
            return View();
        }
    
}
}
