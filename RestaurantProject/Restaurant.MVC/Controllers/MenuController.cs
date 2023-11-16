using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;

namespace Restaurant.MVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMenuService _menuService;

        public MenuController(IProductService productService,ICategoryService categoryService,IMenuService menuService)
        {
            _productService = productService;
           _categoryService = categoryService;
           _menuService = menuService;
        }
        public IActionResult Index()
        {
            ViewBag.Category = _categoryService.GetAll();
            ViewBag.Menus = _menuService.GetAll();
            var products = _productService.GetAll();
            return View(products);
        }
    }
}
