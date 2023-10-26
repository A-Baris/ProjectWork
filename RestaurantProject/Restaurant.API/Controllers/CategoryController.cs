using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;

namespace Restaurant.API.Controllers
{
   
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
           _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_categoryService.GetAll());
        }
    }
}
