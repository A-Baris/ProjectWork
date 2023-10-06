using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class MenuController : Controller
    {
        //dish ve drink controllerda menu işlemleri unutma düzeltme gerçekleştir
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult Index()
        {
           var menuList = _menuService.GetAll();
            return View(menuList);
        }

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(MenuVM menuVM)
        {
            if(ModelState.IsValid)
            {
                Menu menu = new Menu()
                {
                    MenuName = menuVM.MenuName,
                };
                _menuService.Create(menu);
               return RedirectToAction("Menu","Manager","Index");
            }
            return View();

        }
        public async Task<IActionResult> Update(int id)
        {
            var menuEntity= await _menuService.GetbyIdAsync(id);
            if(menuEntity!=null)
            {
                var updated = new MenuVM()
                {
                    Id=menuEntity.Id,
                    MenuName = menuEntity.MenuName,
                };
                return View(updated);
            }
            return View();
        }
        [HttpPost]
      public async Task<IActionResult> Update(MenuVM menuVM)
        {
            if(ModelState.IsValid)
            {
                var entity = await _menuService.GetbyIdAsync(menuVM.Id);
                if(entity!=null)
                {
                    entity.Id = menuVM.Id;
                    entity.MenuName = menuVM.MenuName;
                    _menuService.Update(entity);
                    return RedirectToAction("Menu", "Manager", "Index");

                }
               
            }
            return View(menuVM);
        }
  
        
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _menuService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _menuService.Update(entity);
               return RedirectToAction("Menu", "Manager", "Index");

            }
            return View();
        }
    }
}
