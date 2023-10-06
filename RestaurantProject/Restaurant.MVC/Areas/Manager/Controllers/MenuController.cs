using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class MenuController : Controller
    {
        //dish ve drink controllerda menu işlemleri unutma
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
    }
}
