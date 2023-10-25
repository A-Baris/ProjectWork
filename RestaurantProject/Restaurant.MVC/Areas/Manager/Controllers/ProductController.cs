using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IKitchenService _kitchenService;
        private readonly IMenuService _menuService;
        private readonly ISupplierService _supplierService;

        public ProductController(IProductService productService,ICategoryService categoryService,IKitchenService kitchenService,IMenuService menuService,ISupplierService supplierService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _kitchenService = kitchenService;
            _menuService = menuService;
            _supplierService = supplierService;
        }
        public IActionResult Index()
        {
            string dish = "Dish";
            string drink = "Drink";

            SelectOptionList();


            var dishList=_productService.GetAll();
            ViewBag.DishList = _productService.GetSelectedProducts(dish);
            ViewBag.DrinkList = _productService.GetSelectedProducts(drink);
           
            return View(dishList);
        }
        public IActionResult Create()
        {
            SelectOptionList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                Product product = new Product()
                {
                    ProductName = productVM.ProductName,
                    Price = productVM.Price,
                    SupplierId = productVM.SupplierId,
                    Description = productVM.Description,
                    CategoryId = productVM.CategoryId,
                    KitchenId = productVM.KitchenId,
                    MenuId = productVM.MenuId,
                };
                _productService.Create(product);
                TempData["Message"] = "Successful";
                return RedirectToAction("index", "product", new { area = "Manager" });
            }
            SelectOptionList();
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            SelectOptionList();
            var updated = await _productService.GetbyIdAsync(id);
            var productUpdate = new ProductVM()
            {
                ProductName = updated.ProductName,
                Price = updated.Price,
                SupplierId = updated.SupplierId,
                Description = updated.Description,
                CategoryId = updated.CategoryId,
                KitchenId = updated.KitchenId,
                MenuId= updated.MenuId,
            };
            
            return View(productUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductVM productUpdate)
        {
            if (ModelState.IsValid)
            {
                var entity = await _productService.GetbyIdAsync(productUpdate.Id);
                entity.ProductName = productUpdate.ProductName;
                entity.Price = productUpdate.Price;
                entity.SupplierId = productUpdate.SupplierId;
                entity.Description = productUpdate.Description;
                entity.CategoryId = productUpdate.CategoryId;
                entity.KitchenId = productUpdate.KitchenId;
                entity.MenuId = productUpdate.MenuId;
                _productService.Update(entity);
                TempData["Message"] = "Successful";
                return RedirectToAction("index", "product", new { area = "Manager" });

            }
            SelectOptionList();
            return View(productUpdate);
        }
        public async Task<IActionResult> Remove(int id)

        {
            var dishEntity =  await _productService.GetbyIdAsync(id);
            if(dishEntity!=null)
            {
                dishEntity.BaseStatus= Entity.Enums.BaseStatus.Deleted;
                _productService.Update(dishEntity);
                TempData["Message"] = "Successful";
                return RedirectToAction("index", "product", new { area = "Manager" });
            }
            return View();
        }

        void SelectOptionList() //liste metodu
        {
            ViewBag.DishCategoryList = _categoryService.GetAll().Select(d => new SelectListItem
            {
                Text = d.CategoryName,
                Value = d.Id.ToString(),

            });
            ViewBag.KitchenList = _kitchenService.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitchenName,
                Value = k.Id.ToString(),
            });
            ViewBag.MenuList=_menuService.GetAll().Select(m => new SelectListItem
            {
                Text = m.MenuName,
                Value = m.Id.ToString(),
            });
            ViewBag.SupplierList = _supplierService.GetAll().Select(m => new SelectListItem
            {
                Text = m.CompanyName,
                Value = m.Id.ToString(),
            });
        }

    }
}
