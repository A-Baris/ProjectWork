using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;

using Restaurant.Common.ImageUploader;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;

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
        private readonly IMapper _mapper;
        private readonly IValidator<ProductVM> _validator;
        private readonly IValidator<ProductUpdateVM> _validatorUpdateVM;

        public ProductController(IProductService productService,ICategoryService categoryService,IKitchenService kitchenService,IMenuService menuService,ISupplierService supplierService,IMapper mapper,IValidator<ProductVM> validator,IValidator<ProductUpdateVM> validatorUpdateVM)
        {
            _productService = productService;
            _categoryService = categoryService;
            _kitchenService = kitchenService;
            _menuService = menuService;
            _supplierService = supplierService;
            _mapper = mapper;
            _validator = validator;
          _validatorUpdateVM = validatorUpdateVM;
        }
        public IActionResult Index()
        {
            string dish = "Yemek";
            string drink = "İçecek";
            string salad = "Salata";
            string dessert = "Tatlı";

            SelectOptionList();
          

            var dishList=_productService.GetAll();
            ViewBag.DishList = _productService.GetSelectedProducts(dish);
            ViewBag.DrinkList = _productService.GetSelectedProducts(drink);
            ViewBag.SaladList = _productService.GetSelectedProducts(salad);
            ViewBag.DessertList = _productService.GetSelectedProducts(dessert);
           
            return View(dishList);
        }
        public IActionResult Create()
        {
            SelectOptionList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM,IFormFile? productImage)
        {
            ModelState.Clear();
            var result = _validator.Validate(productVM);
       
            if(result.IsValid)
            {
                string path = "";
                var imageResult = "";
              
                
                if (productImage != null)
                {

                    imageResult = ImageUploader.ImageChangeName(productImage.FileName);
                }

                if (imageResult != "" && imageResult != "0")
                {
                    
                   productVM.ImageUrl = imageResult;

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", imageResult);



                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                       productImage.CopyToAsync(stream);
                    }
                }
              
                var product = _mapper.Map<Product>(productVM);
                _productService.Create(product);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "product", new { area = "Manager" });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            //ModelStateHelper.AddErrorsToModelState(ModelState, result.Errors);
            TempData.SetErrorMessage();
            SelectOptionList();
            return View(productVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            SelectOptionList();
            var updated = await _productService.GetbyIdAsync(id);
            if (updated != null)
            {

                var productUpdate = _mapper.Map<ProductUpdateVM>(updated);

                return View(productUpdate);
            }
            else
            {
                TempData.NotFoundId();
                return RedirectToAction("index", "product", new { area = "manager" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM UpdateVM, IFormFile? productImage)
        {
            ModelState.Clear();
            var result = _validatorUpdateVM.Validate(UpdateVM);
            if (result.IsValid)
            {
                string path = "";
                var imageResult = "";


                if (productImage != null)
                {

                    imageResult = ImageUploader.ImageChangeName(productImage.FileName);
                }

                if (imageResult != "" && imageResult != "0")
                {

                    UpdateVM.ImageUrl = imageResult;

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", imageResult);



                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        productImage.CopyToAsync(stream);
                    }
                }




                var entity = await _productService.GetbyIdAsync(UpdateVM.Id);
                _mapper.Map(UpdateVM,entity);
                _productService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "product", new { area = "Manager" });

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            SelectOptionList();
           TempData.SetErrorMessage();
            return View(UpdateVM);
        }
        public async Task<IActionResult> Remove(int id)

        {
            var dishEntity =  await _productService.GetbyIdAsync(id);
            if(dishEntity!=null)
            {
                dishEntity.BaseStatus= Entity.Enums.BaseStatus.Deleted;
                _productService.Update(dishEntity);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "product", new { area = "Manager" });
            }
            TempData.NotFoundId();
            return View();
        }

        void SelectOptionList() //liste metodu
        {
            ViewBag.CategoryList = _categoryService.GetAll().Select(d => new SelectListItem
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
