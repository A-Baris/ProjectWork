using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class DishCategoryController : Controller
    {
        private readonly IDishCategoryService _categoryService;

        public DishCategoryController(IDishCategoryService categoryService)
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
        public IActionResult Create(DishCategoryVm dishCategoryVm)
        {
            if(ModelState.IsValid)
            {
                DishCategory category = new DishCategory()
                {
                    CategoryName = dishCategoryVm.CategoryName
                };
                _categoryService.Create(category);
                return RedirectToAction("DishCategory", "Manager", "Index");
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var category= await _categoryService.GetbyIdAsync(id);
            var updated = new DishCategory()
            {
                Id = id,
                CategoryName = category.CategoryName
            };
            return View(updated);
        }
        [HttpPost]
        public async Task <IActionResult> Update(DishCategoryVm updated)
        {
            if(ModelState.IsValid)
            {
                var category = await _categoryService.GetbyIdAsync(updated.Id);
                category.Id= updated.Id;
                category.CategoryName = updated.CategoryName;
                _categoryService.Update(category);
                return RedirectToAction("DishCategory", "Manager", "Index");

            }
            return View();
        }
        public async Task <IActionResult> Remove(int id)
        {
            var entity = await _categoryService.GetbyIdAsync(id);
            if(entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _categoryService.Update(entity);
                return RedirectToAction("DishCategory", "Manager", "Index");
            }
            return View();
        }
    }
}
