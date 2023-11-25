using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
using Restaurant.MVC.Validators;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]

    public class ProductController : AreaBaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IKitchenService _kitchenService;
        private readonly IMenuService _menuService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly IValidationService<ProductVM> _validationforProductVM;
        private readonly IValidationService<ProductUpdateVM> _validationforUpdateVM;


        public ProductController(IProductService productService, ICategoryService categoryService, IKitchenService kitchenService, IMenuService menuService, ISupplierService supplierService, IMapper mapper, IValidationService<ProductVM> validationforProductVM, IValidationService<ProductUpdateVM> validationforUpdateVM)
        {
            _productService = productService;
            _categoryService = categoryService;
            _kitchenService = kitchenService;
            _menuService = menuService;
            _supplierService = supplierService;
            _mapper = mapper;
            _validationforProductVM = validationforProductVM;
            _validationforUpdateVM = validationforUpdateVM;

        }

        public IActionResult Index()
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }


            SelectOptionList();
            ViewBag.MenuList = _menuService.GetAll();
            var dishList = _productService.GetAll();
            return View(dishList);
        }

        public IActionResult Create()
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            SelectOptionList();

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(ProductVM productVM, IFormFile? productImage)
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
               
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationforProductVM.GetValidationErrors(productVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                SelectOptionList();
                return View(productVM);
            }


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

        public async Task<IActionResult> Update(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
               
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
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
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
              
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationforUpdateVM.GetValidationErrors(UpdateVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                SelectOptionList();
                return View(UpdateVM);
            }

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
            if (UpdateVM.ImageUrl== null)
            {
                UpdateVM.ImageUrl =entity.ImageUrl;
            }
            _mapper.Map(UpdateVM, entity);
            _productService.Update(entity);
            TempData.SetSuccessMessage();
            return RedirectToAction("index", "product", new { area = "Manager" });



        }

        public async Task<IActionResult> Remove(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var dishEntity = await _productService.GetbyIdAsync(id);
            if (dishEntity != null)
            {
                dishEntity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _productService.Update(dishEntity);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "product", new { area = "Manager" });
            }
            TempData.NotFoundId();
            return View();
        }
        [Authorize(Roles = "employee")]
        public IActionResult ProductList()
        {

            ViewBag.Category = _categoryService.GetAll();
            ViewBag.Menus = _menuService.GetAll();
            var products = _productService.GetAll();
            return View(products);
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
            ViewBag.MenuList = _menuService.GetAll().Select(m => new SelectListItem
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
