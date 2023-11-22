using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class RecipeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;
        private readonly ProjectContext _context;
        private readonly IValidationService<RecipeVM> _validationService;

        public RecipeController(IProductService productService, IIngredientService ingredientService, ProjectContext context, IValidationService<RecipeVM> validationService)
        {
            _productService = productService;
            _ingredientService = ingredientService;
            _context = context;
            _validationService = validationService;
        }
        public IActionResult Index()
        {
            SelectProductAndIngredient();
            ViewBag.ProductIngredient = _context.ProductIngredients.ToList();
            return View();

        }
        public IActionResult Create()
        {
            SelectProductAndIngredient();
            ViewBag.ProductIngredient = _context.ProductIngredients.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(RecipeVM createVM)
        {
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(createVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                SelectProductAndIngredient();
                ViewBag.ProductIngredient = _context.ProductIngredients.ToList();
                return View(createVM);
            }

            ProductIngredient pi = new ProductIngredient()
            {
                ProductId = createVM.ProductId,
                IngredientId = createVM.IngredientId,
                Quantity = createVM.Quantity,
            };
            var checkEntity = _context.ProductIngredients.Where(x => x.ProductId == pi.ProductId && x.IngredientId == pi.IngredientId).FirstOrDefault();
            if (checkEntity != null)
            {
                TempData["ErrorMessage"] = "Ürün sisteme kayıtlı,dilerseniz güncelleme yapabilirsiniz";
                return RedirectToAction("Create", "Recipe", new { area = "manager" });
            }


            _context.Set<ProductIngredient>().Add(pi);
            _context.SaveChanges();
            TempData.SetSuccessMessage();
            SelectProductAndIngredient();
            ViewBag.ProductIngredient = _context.ProductIngredients.ToList();
            return RedirectToAction("Create", "Recipe", new { area = "manager" });
        }

        public IActionResult Update(int productId, int ingredientId)
        {
            SelectProductAndIngredient();

            var entityFinding = _context.ProductIngredients.Where(x => x.ProductId == productId && x.IngredientId == ingredientId).FirstOrDefault();
            ViewData["x"] = entityFinding;

            if (entityFinding != null)
            {

                var updated = new RecipeVM
                {
                    ProductId = productId,
                    IngredientId = ingredientId,
                    Quantity = entityFinding.Quantity,
                };


                _context.ProductIngredients.Remove(entityFinding);
                _context.SaveChanges();

                return View(updated);
            }
            else
            {
                TempData.NotFoundId();
                return RedirectToAction("Create", "Recipe", new { area = "manager" });
            }

        }
        [HttpPost]
        public IActionResult Update(RecipeVM updated)
        {

            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(updated);
            if (errors != null)
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                SelectProductAndIngredient();
                TempData.SetErrorMessage();
                return View(updated);
            }


            ProductIngredient pi = new ProductIngredient()
            {
                ProductId = updated.ProductId,
                IngredientId = updated.IngredientId,
                Quantity = updated.Quantity,
            };
            var checkEntity = _context.ProductIngredients.Where(x => x.ProductId == pi.ProductId && x.IngredientId == pi.IngredientId).FirstOrDefault();
            if (checkEntity != null)
            {
                TempData["ErrorMessage"] = "Ürün sisteme kayıtlı,dilerseniz güncelleme yapabilirsiniz";
                return RedirectToAction("Create", "Recipe", new { area = "manager" });
            }

            _context.ProductIngredients.Add(pi);
            _context.SaveChanges();
            TempData.SetSuccessMessage();
            return RedirectToAction("Create", "Recipe", new { area = "manager" });


        }

        public IActionResult Remove(int productId, int ingredientId)
        {

            var entityFinding = _context.ProductIngredients.Where(x => x.ProductId == productId && x.IngredientId == ingredientId).FirstOrDefault();
            if (entityFinding != null)
            {
                _context.ProductIngredients.Remove(entityFinding);
                _context.SaveChanges();
                TempData.SetSuccessMessage();
                return RedirectToAction("Create", "Recipe", new { area = "manager" });
            }
            else
            {
                TempData.NotFoundId();
                return RedirectToAction("Create", "Recipe", new { area = "manager" });
            }
            return View();
        }

        void SelectProductAndIngredient()
        {
            ViewBag.ProductSelect = _productService.GetAll().Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.Id.ToString(),
            });

            ViewBag.IngredientSelect = _ingredientService.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
        }
    }
}
