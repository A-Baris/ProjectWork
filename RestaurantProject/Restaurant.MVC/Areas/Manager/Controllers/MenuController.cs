using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using System.Text;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class MenuController : Controller
    {
        //dish ve drink controllerda menu işlemleri unutma düzeltme gerçekleştir
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
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
            if (ModelState.IsValid)
            {

                var menu = _mapper.Map<Menu>(menuVM);
                _menuService.Create(menu);
                TempData["Message"] = "Successful";
                return RedirectToAction("index", "menu", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            return View(menuVM);

        }
        public async Task<IActionResult> Update(int id)
        {
            var menuEntity = await _menuService.GetbyIdAsync(id);
            if (menuEntity != null)
            {
                var updated = _mapper.Map<MenuVM>(menuEntity);
                return View(updated);
            }
            TempData["ErrorMessage"] = "Id is not found";
            return RedirectToAction("index", "menu", new { area = "Manager" });

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, MenuVM menuVM)
        {

            var entity = await _menuService.GetbyIdAsync(id);
            if (entity != null)
            {
                if (ModelState.IsValid)
                {
                    var updated = _mapper.Map(menuVM, entity);
                    _menuService.Update(updated);
                    TempData["Message"] = "Updated";
                    return RedirectToAction("index", "menu", new { area = "Manager" });

                }
                TempData["ErrorMessage"] = "ModelState is invalid";
                return View(menuVM);
            }
            TempData["ErrorMessage"] = "Id is not found";
            return RedirectToAction("index", "menu", new { area = "Manager" });
    }


    public async Task<IActionResult> Remove(int id)
    {
        var entity = await _menuService.GetbyIdAsync(id);
        if (entity != null)
        {
            entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
            _menuService.Update(entity);
            TempData["Message"] = "Deleted";
            return RedirectToAction("index", "menu", new { area = "Manager" });

        }
            TempData["ErrorMessage"] = "Id is not found";
            return RedirectToAction("index", "menu", new { area = "Manager" });
        }
}
}
