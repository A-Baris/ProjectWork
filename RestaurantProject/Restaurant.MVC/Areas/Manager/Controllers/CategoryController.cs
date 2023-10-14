using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
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
        public IActionResult Create(CategoryVm dishCategoryVm)
        {
            if(ModelState.IsValid)
            {
                Category category = new Category()
                {
                    CategoryName = dishCategoryVm.CategoryName
                };
                _categoryService.Create(category);
                return RedirectToAction("index", "category", new { area = "Manager" });
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var category= await _categoryService.GetbyIdAsync(id);
            var updated = new Category()
            {
                Id = id,
                CategoryName = category.CategoryName
            };
            return View(updated);
        }
        [HttpPost]
        public async Task <IActionResult> Update(CategoryVm updated)
        {
            if(ModelState.IsValid)
            {
                var category = await _categoryService.GetbyIdAsync(updated.Id);
                category.Id= updated.Id;
                category.CategoryName = updated.CategoryName;
                _categoryService.Update(category);
                return RedirectToAction("index", "category", new { area = "Manager" });

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
                return RedirectToAction("index", "category", new { area = "Manager" });
            }
            return View();
        }
    }
}
