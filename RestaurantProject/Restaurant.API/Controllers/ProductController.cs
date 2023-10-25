using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService,IMapper mapper)
        {
          _productService = productService;
           _mapper = mapper;
        }

        public IActionResult GetProducts()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }
       
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetbyIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult PostProduct(ProductVM productVM ) 
        {
            var product = _mapper.Map<Product>(productVM);
            _productService.Create(product);
            return Ok(product);
            
        }
    }
}
