using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class KitchenController : Controller
    {
        private readonly IKitchenService _kitchenService;
        private readonly IMapper _mapper;

        public KitchenController(IKitchenService kitchenService,IMapper mapper)
        {
            _kitchenService = kitchenService;
          _mapper = mapper;
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
        [Authorize(Roles ="admin")]
        public IActionResult Create(KitchenVM kitchenVM)
        {
            if(ModelState.IsValid)
            {
              var kitchen =_mapper.Map<Kitchen>(kitchenVM);
                _kitchenService.Create(kitchen);
                TempData["Message"] = "Successful";
                return RedirectToAction("index", "kitchen", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            return View(kitchenVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            var kitchen = await _kitchenService.GetbyIdAsync(id);
           if(kitchen != null)
            {
                var updated = _mapper.Map<KitchenVM>(kitchen);
                return View(updated);
            }
            TempData["ErrorMessage"] = "Id is not found";
            return RedirectToAction("index", "kitchen", new { area = "Manager" });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,KitchenVM updated)
        {
            var kitchen = await _kitchenService.GetbyIdAsync(id);
            if (kitchen != null)
            {
                if (ModelState.IsValid)
                {
                    _mapper.Map(updated, kitchen);
                    _kitchenService.Update(kitchen);

                    TempData["Message"] = "Updated";
                    return RedirectToAction("index", "kitchen", new { area = "Manager" });
                }
                TempData["ErrorMessage"] = "ModelState is invalid";
                return View(updated);
            }
            TempData["ErrorMessage"] = "Id is not found";
            return RedirectToAction("index", "kitchen", new { area = "Manager" });
        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _kitchenService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Restaurant.Entity.Enums.BaseStatus.Deleted;
                _kitchenService.Update(entity);
                TempData["Message"] = "Successful";
                return RedirectToAction("index", "kitchen", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "Id is not found";
            return RedirectToAction("index", "kitchen", new { area = "Manager" });
        }
       


    }
}
