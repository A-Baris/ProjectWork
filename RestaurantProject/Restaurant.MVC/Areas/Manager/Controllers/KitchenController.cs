using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class KitchenController : Controller
    {
        private readonly IKitchenService _kitchenService;

        public KitchenController(IKitchenService kitchenService)
        {
            _kitchenService = kitchenService;
        }
        public IActionResult Index()
        {
            var kitchenList=_kitchenService.GetAll();   
            return View(kitchenList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(KitchenVM kitchenVM)
        {
            if(ModelState.IsValid)
            {
                Kitchen kitchen = new Kitchen()
                {
                    KitchenName = kitchenVM.KitchenName,
                    Description = kitchenVM.Description,
                };
                _kitchenService.Create(kitchen);
                return RedirectToAction("Kitchen", "Manager", "Index");
            }
            return View(kitchenVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            var kitchen = await _kitchenService.GetbyIdAsync(id);
            var updated = new KitchenVM()
            {
                Id = id,
                KitchenName = kitchen.KitchenName,
                Description = kitchen.Description
            };

            return View(updated);
        }
        [HttpPost]
        public async Task<IActionResult> Update(KitchenVM updated)
        {
            if(ModelState.IsValid)
            {
                var kitchenEntity =  await _kitchenService.GetbyIdAsync(updated.Id);
                kitchenEntity.KitchenName = updated.KitchenName;
                kitchenEntity.Description= updated.Description;
                _kitchenService.Update(kitchenEntity);
                return RedirectToAction("Kitchen", "Manager", "Index");
            }
            
            return View();
        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _kitchenService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Restaurant.Entity.Enums.BaseStatus.Deleted;
                _kitchenService.Update(entity);
                return RedirectToAction("Kitchen", "Manager", "Index");
            }
            return View();
        }


    }
}
