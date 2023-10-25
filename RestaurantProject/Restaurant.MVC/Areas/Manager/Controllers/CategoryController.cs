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
                TempData["Message"] = "Is created successfully";
                return RedirectToAction("index", "category", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            return View(dishCategoryVm);
        }
        public async Task<IActionResult> Update(int id)
        {
            var category= await _categoryService.GetbyIdAsync(id);
            if (category != null)
            {
                var updated = new Category()
                {
                    Id = id,
                    CategoryName = category.CategoryName
                };
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "Category", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "Successful";
            return View("index");
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
                TempData["Message"] = "Successful";
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
                TempData["Message"] = "Successful";
                return RedirectToAction("index", "category", new { area = "Manager" });
            }
            return View();
        }
    }
}
