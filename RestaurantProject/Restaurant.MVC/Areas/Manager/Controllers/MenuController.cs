using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;
using System.Text;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class MenuController : Controller
    {
        //dish ve drink controllerda menu işlemleri unutma düzeltme gerçekleştir
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        private readonly IValidationService<MenuVM> _validationService;

        public MenuController(IMenuService menuService, IMapper mapper, IValidationService<MenuVM> validationService)
        {
            _menuService = menuService;
            _mapper = mapper;
            _validationService = validationService;
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
            ModelState.Clear();
            var errros = _validationService.GetValidationErrors(menuVM);
            if (errros.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errros);
                TempData.SetErrorMessage();
                return View(menuVM);
            }


            var menu = _mapper.Map<Menu>(menuVM);
            _menuService.Create(menu);
            TempData.SetSuccessMessage();
            return RedirectToAction("index", "menu", new { area = "Manager" });

        }
        public async Task<IActionResult> Update(int id)
        {
            var menuEntity = await _menuService.GetbyIdAsync(id);
            if (menuEntity != null)
            {
                var updated = _mapper.Map<MenuVM>(menuEntity);
                return View(updated);
            }
            TempData.NotFoundId();
            return RedirectToAction("index", "menu", new { area = "Manager" });

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, MenuVM menuVM)
        {
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(menuVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(menuVM);
            }
            var entity = await _menuService.GetbyIdAsync(id);
            if (entity != null)
            {

                var updated = _mapper.Map(menuVM, entity);
                _menuService.Update(updated);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "menu", new { area = "Manager" });



            }
            TempData.NotFoundId();
            return RedirectToAction("index", "menu", new { area = "Manager" });
        }


        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _menuService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _menuService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "menu", new { area = "Manager" });

            }
            TempData.NotFoundId();
            return RedirectToAction("index", "menu", new { area = "Manager" });
        }
    }
}
