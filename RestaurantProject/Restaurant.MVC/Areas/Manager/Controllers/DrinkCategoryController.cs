using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class DrinkCategoryController : Controller
    {
        private readonly IDrinkCategoryService _drinkCategoryService;

        public DrinkCategoryController(IDrinkCategoryService drinkCategoryService)
        {
            _drinkCategoryService = drinkCategoryService;
        }
        public IActionResult Index()
        {
            var drinkCategoryList = _drinkCategoryService.GetAll();
            return View(drinkCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DrinkCategoryVM categoryVM)
        {
            if(ModelState.IsValid)
            {
                DrinkCategory category = new DrinkCategory()
                {
                    CategoryName = categoryVM.CategoryName,
                    Description = categoryVM.Description,
                };
                _drinkCategoryService.Create(category);
                return RedirectToAction("drinkcategory", "manager", "index");
            }
            return View(categoryVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await _drinkCategoryService.GetbyIdAsync(id);
            if (category != null)
            {
                var updated = new DrinkCategoryVM()
                {
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };
                return View(updated);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(DrinkCategoryVM category)
        {
            
            if (ModelState.IsValid)
            {
                var entity = await _drinkCategoryService.GetbyIdAsync(category.Id);
                entity.CategoryName= category.CategoryName;
                entity.Description= category.Description;
                _drinkCategoryService.Update(entity);
                return RedirectToAction("drinkcategory", "manager", "index");
            }
            return View();
        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity= await _drinkCategoryService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus=Restaurant.Entity.Enums.BaseStatus.Deleted;
                _drinkCategoryService.Update(entity);
                return RedirectToAction("drinkcategory", "manager", "index");
            }
            return View();
        }
    }
}

