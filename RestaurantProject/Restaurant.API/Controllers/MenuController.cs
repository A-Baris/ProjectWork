using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;

namespace Restaurant.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
           _menuService = menuService;
        }

        [HttpGet]
        public IActionResult GetAllMenus()
        {
            var menus = _menuService.GetAll();
            return Ok(menus);
        }
    }
}
