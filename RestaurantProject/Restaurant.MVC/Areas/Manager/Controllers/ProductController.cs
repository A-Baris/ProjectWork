﻿using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IProductService productService,ICategoryService categoryService,IKitchenService kitchenService,IMenuService menuService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _kitchenService = kitchenService;
            _menuService = menuService;
        }
        public IActionResult Index()
        {
            string dish = "Dish";
            string drink = "Drink";
            string ingredient = "Ingredient";

            ViewBag.DishCategoryList = _categoryService.GetAll();
            ViewBag.KitchenList= _kitchenService.GetAll();
            var dishList=_productService.GetAll();
            ViewBag.DishList = _productService.GetSelectedProducts(dish);
            ViewBag.DrinkList = _productService.GetSelectedProducts(drink);
            ViewBag.IngredientList = _productService.GetSelectedProducts(ingredient);
            return View(dishList);
        }
        public IActionResult Create()
        {
            CategoryAndKitchenList();

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
                    Quantity = productVM.Quantity,
                    Description = productVM.Description,
                    CategoryId = productVM.CategoryId,
                    KitchenId = productVM.KitchenId,
                    MenuId = productVM.MenuId,
                };
                _productService.Create(product);
                return RedirectToAction("Product", "Manager","Index");
            }
            CategoryAndKitchenList();
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            CategoryAndKitchenList();
            var updated = await _productService.GetbyIdAsync(id);
            var productUpdate = new ProductVM()
            {
                ProductName = updated.ProductName,
                Price = updated.Price,
                Quantity = updated.Quantity,
                Description = updated.Description,
                CategoryId = updated.CategoryId,
                KitchenId = updated.KitchenId,
                MenuId= updated.MenuId,
            };
            
            return View(productUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductVM dishUpdate)
        {
            if (ModelState.IsValid)
            {
                var entity = await _productService.GetbyIdAsync(dishUpdate.Id);
                entity.ProductName = dishUpdate.ProductName;
                entity.Price = dishUpdate.Price;
                entity.Quantity = dishUpdate.Quantity;
                entity.Description = dishUpdate.Description;
                entity.CategoryId = dishUpdate.CategoryId;
                entity.KitchenId = dishUpdate.KitchenId;
                entity.MenuId = dishUpdate.MenuId;
                _productService.Update(entity);
                return RedirectToAction("Product", "Manager", "Index");

            }
            CategoryAndKitchenList();
            return View(dishUpdate);
        }
        public async Task<IActionResult> Remove(int id)

        {
            var dishEntity =  await _productService.GetbyIdAsync(id);
            if(dishEntity!=null)
            {
                dishEntity.BaseStatus= Entity.Enums.BaseStatus.Deleted;
                _productService.Update(dishEntity);
                return RedirectToAction("Product", "Manager", "Index");
            }
            return View();
        }

        void CategoryAndKitchenList() //liste metodu
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
        }

    }
}
