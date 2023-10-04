using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IDishCategoryService _dishCategoryService;
        private readonly IKitchenService _kitchenService;

        public DishController(IDishService dishService,IDishCategoryService dishCategoryService,IKitchenService kitchenService)
        {
            _dishService = dishService;
            _dishCategoryService = dishCategoryService;
            _kitchenService = kitchenService;
        }
        public IActionResult Index()
        {
            ViewBag.DishCategoryList = _dishCategoryService.GetAll();
            ViewBag.KitchenList= _kitchenService.GetAll();
            var dishList=_dishService.GetAll();
            return View(dishList);
        }
        public IActionResult Create()
        {
            CategoryAndKitchenList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(DishVM dishVM)
        {
            if(ModelState.IsValid)
            {
                Dish dish = new Dish()
                {
                    DishName = dishVM.DishName,
                    Price = dishVM.Price,
                    Quantity = dishVM.Quantity,
                    Description = dishVM.Description,
                    DishCategoryId = dishVM.DishCategoryId,
                    KitchenId = dishVM.KitchenId,
                };
                _dishService.Create(dish);
                return RedirectToAction("Dish","Manager","Index");
            }
            CategoryAndKitchenList();
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            CategoryAndKitchenList();
            var updated = await _dishService.GetbyIdAsync(id);
            var dishUpdate = new DishVM()
            {
                DishName = updated.DishName,
                Price = updated.Price,
                Quantity = updated.Quantity,
                Description = updated.Description,
                DishCategoryId = updated.DishCategoryId,
                KitchenId = updated.KitchenId,
            };
            
            return View(dishUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Update(DishVM dishUpdate)
        {
            if (ModelState.IsValid)
            {
                var entity = await _dishService.GetbyIdAsync(dishUpdate.Id);
                entity.DishName = dishUpdate.DishName;
                entity.Price = dishUpdate.Price;
                entity.Quantity = dishUpdate.Quantity;
                entity.Description = dishUpdate.Description;
                entity.DishCategoryId = dishUpdate.DishCategoryId;
                entity.KitchenId = dishUpdate.KitchenId;
                _dishService.Update(entity);
                return RedirectToAction("Dish", "Manager", "Index");

            }
            CategoryAndKitchenList();
            return View(dishUpdate);
        }
        public async Task<IActionResult> Remove(int id)

        {
            var dishEntity =  await _dishService.GetbyIdAsync(id);
            if(dishEntity!=null)
            {
                dishEntity.BaseStatus= Entity.Enums.BaseStatus.Deleted;
                _dishService.Update(dishEntity);
                return RedirectToAction("Dish", "Manager", "Index");
            }
            return View();
        }

        void CategoryAndKitchenList() //liste metodu
        {
            ViewBag.DishCategoryList = _dishCategoryService.GetAll().Select(d => new SelectListItem
            {
                Text = d.CategoryName,
                Value = d.Id.ToString(),

            });
            ViewBag.KitchenList = _kitchenService.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitchenName,
                Value = k.Id.ToString(),
            });
        }

    }
}
