using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class DrinkController : Controller
    {
        private readonly IDrinkService _drinkService;
        private readonly IDrinkCategoryService _categoryService;
        private readonly IKitchenService _kitchenService;

        public DrinkController(IDrinkService drinkService,IDrinkCategoryService categoryService,IKitchenService kitchenService)
        {
            _drinkService = drinkService;
            _categoryService = categoryService;
            _kitchenService = kitchenService;
        }
        public IActionResult Index()
        {
            ViewBag.CategoryList = _categoryService.GetAll();
            ViewBag.KitchenList = _kitchenService.GetAll();
            var drinkList = _drinkService.GetAll();
            return View(drinkList);
        }
        public IActionResult Create()
        {
            CategoryAndKitchenSelect();
       
            return View();

        }
        [HttpPost]
        public IActionResult Create(DrinkVM drinkVM)
        {
            if(ModelState.IsValid)
            {
                Drink drink = new Drink()
                {
                    DrinkName = drinkVM.DrinkName,
                    Price = drinkVM.Price,
                    Quantity = drinkVM.Quantity,
                    DrinkCategoryId = drinkVM.DrinkCategoryId,
                    KitchenId = drinkVM.KitchenId,
                };
                _drinkService.Create(drink);
                return RedirectToAction("drink", "manager", "index");
                
            }
            CategoryAndKitchenSelect();
            return View(drinkVM);


        }
        public async  Task<IActionResult> Update(int id)
        {
            CategoryAndKitchenSelect();
            var drinkEntity= await _drinkService.GetbyIdAsync(id);
            if (drinkEntity != null)
            {
                var updated = new DrinkVM()
                {
                    Id=drinkEntity.Id,
                    DrinkName = drinkEntity.DrinkName,
                    Price = drinkEntity.Price,
                    Quantity = drinkEntity.Quantity,
                    DrinkCategoryId = drinkEntity.DrinkCategoryId,
                    KitchenId = drinkEntity.KitchenId,

                };
                return View(updated);
            }
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> Update(DrinkVM drinkVM)
        {
           
            if (ModelState.IsValid)
            {
                var drinkEntity = await _drinkService.GetbyIdAsync(drinkVM.Id);
                if (drinkEntity != null)
                {
                    drinkEntity.Id = drinkVM.Id;
                    drinkEntity.DrinkName = drinkVM.DrinkName;
                    drinkEntity.Price= drinkVM.Price;
                    drinkEntity.Quantity= drinkVM.Quantity;
                    drinkEntity.DrinkCategoryId= drinkVM.DrinkCategoryId;
                    drinkEntity.KitchenId= drinkVM.KitchenId;
                    _drinkService.Update(drinkEntity);
    }
                return RedirectToAction("drink", "manager", "index");

            }
            CategoryAndKitchenSelect();
            return View(drinkVM);
        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _drinkService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _drinkService.Update(entity);
                return RedirectToAction("drink", "manager", "index");
            }
            return View();

        }






        void CategoryAndKitchenSelect()
        {
            ViewBag.CategorySelect = _categoryService.GetAll().Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString(),
            });
            ViewBag.KitchenSelect = _kitchenService.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitchenName,
                Value = k.Id.ToString(),
            });
        }
    }
}
