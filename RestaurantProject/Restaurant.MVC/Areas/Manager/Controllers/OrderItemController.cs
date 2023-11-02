using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Experimental.ProjectCache;
using Microsoft.Identity.Client;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class OrderItemController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IEmployeeService _employeeService;
        private readonly IProductService _productService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly ProjectContext _context;
        private readonly IOrderService _orderService;
        private readonly IIngredientService _ingredient;
        private readonly ICategoryService _categoryService;
        private readonly IMenuService _menuService;

        public OrderItemController(IMapper mapper, IOrderService orderService, IEmployeeService employeeService, IProductService productService, ITableOfRestaurantService tableOfRestaurantService, ProjectContext context, IIngredientService ingredient,ICategoryService categoryService,IMenuService menuService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _employeeService = employeeService;
            _productService = productService;
            _tableOfRestaurantService = tableOfRestaurantService;
            _context = context;

            _ingredient = ingredient;
            _categoryService = categoryService;
            _menuService = menuService;
        }

        public IActionResult Selectproduct(int id)
        {
            ViewBag.Category = _categoryService.GetAll();
            ViewBag.Menus = _menuService.GetAll();
            TempData["TableId"] = id;
            var products = _productService.GetAll();
            return View(products);
        }


        public IActionResult CreateOrder()
        {
            Select();
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateOrder(int tableId, int Id, OrderItemCreateVM createVM)
        {

            createVM.ProductId = Id;


            var product = await _productService.GetbyIdAsync(Id);
            if (product == null)
            {
                return RedirectToAction("selectproduct", "orderitem", new { area = "manager" });
            }
            else
            {
                var orderitem = _orderService.GetAll();
                if (orderitem.Any(x => x.TableofRestaurantId == tableId && x.ProductId == Id))
                {
                    var update = _orderService.GetAll().Where(x => x.TableofRestaurantId == tableId && x.ProductId == Id).FirstOrDefault();
                    update.Quantity += createVM.Quantity;
                    update.TotalPrice = update.Quantity * update.Product.Price;
                    _orderService.Update(update);
                }


                else
                {

                    //Order orderItem = new Order()
                    //{
                    //    ProductId = product.Id,
                    //    Quantity = createVM.Quantity,
                    //    TotalPrice = product.Price * createVM.Quantity, //employeeId düzenle
                    //    TableofRestaurantId = tableId,
                    //    EmployeeId = 1,
                    //    Description = createVM.Description,
                    //};
                    if (ModelState.IsValid)
                    {
                        var table = await _tableOfRestaurantService.GetbyIdAsync(tableId);
                        if (table != null)
                        {
                            var orderItem = _mapper.Map<Order>(createVM);
                            orderItem.TableofRestaurantId = tableId;
                            orderItem.EmployeeId = table.EmployeeId;
                            orderItem.TotalPrice = product.Price * createVM.Quantity;
                            _orderService.Create(orderItem);
                            table.Status = Entity.Enums.ReservationStatus.Active;
                            _tableOfRestaurantService.Update(table);

                        }
                       
                     
                        TempData["Message"] = "Successful";
                        TempData["TableId"] = tableId;
                        ViewBag.Dish = _productService.GetSelectedProducts("Dish");
                        return RedirectToAction("selectproduct", "orderitem", new { area = "manager",id=tableId });
                    }
                    TempData["ErrorMessage"] = "ModelState is invalid";
                    return View(createVM);

                }


            }

            TempData["TableId"] = tableId;
            ViewBag.Dish = _productService.GetSelectedProducts("Dish");
            return View("selectproduct");

        }




        public IActionResult Index()
        {
            var tables = _tableOfRestaurantService.GetAll();
            return View(tables);
        }

        public async Task<IActionResult> Update(int id)
        {

            var orderItem = await _orderService.GetbyIdAsync(id);
            if (orderItem != null)
            {
                var updated = _mapper.Map<OrderVM>(orderItem);
                return View(updated);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, OrderVM orderVM)
        {
            var product = await _productService.GetbyIdAsync(orderVM.ProductId);
            var entity = await _orderService.GetbyIdAsync(id);
            if (ModelState.IsValid)
            {
                _mapper.Map(orderVM, entity);
                entity.TotalPrice = orderVM.Quantity * product.Price;
                _orderService.Update(entity);
                TempData["Message"] = "Updated is succesful";
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });   //orderlist/tableID ile dönüş yapmayı unutma
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            return View(orderVM);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _orderService.GetbyIdAsync(id);
            if (entity != null)
            {
                _orderService.Remove(entity);
                TempData["Message"] = "Order is deleted";
                return RedirectToAction("orderlist", "tableofrestaurant", new { area = "Manager", id = entity.TableofRestaurantId });
            }
            return RedirectToAction("orderlist", "tableofrestaurant", new { area = "Manager", id = entity.TableofRestaurantId });

        }

        [Authorize(Roles = "chief,admin,waiter")]
        public IActionResult OrderTracking()
        {
            ViewBag.Tables = _tableOfRestaurantService.GetAll();
            ViewBag.Products = _productService.GetAll();
            var orderList = _orderService.GetAll();

            return View(orderList);
        }
        [Authorize(Roles = "chief,admin")]
        public async Task<IActionResult> OrderReady(int id)
        {
            var entity = await _orderService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.StatusOfOrder = Restaurant.Entity.Enums.OrderStatus.Ready;
                _orderService.Update(entity);
            }
            TempData["Message"] = "Successful";
            return RedirectToAction("ordertracking", "Orderitem", "manager");
        }
        [Authorize(Roles = "chief,admin")]
        public async Task<IActionResult> OrderDelivered(int id)
        {
            var entity = await _orderService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.StatusOfOrder = Restaurant.Entity.Enums.OrderStatus.Delivered;
                _orderService.Update(entity);
            }
            TempData["Message"] = "Successful";
            return RedirectToAction("ordertracking", "Orderitem", "manager");
        }
        [Authorize(Roles = "chief,admin")]
        public async Task<IActionResult> OrderPreparing(int id)
        {
            // ürün hazırlanmaya başladığında o ürünü oluşturmak için gerekli malzeme miktarı ürün adetine göre stoktaki malzeme miktarlarından düşülecek
            var entity = await _orderService.GetbyIdAsync(id);
            if (entity != null)
            {


                var productId = entity.ProductId;
                var ingredients = _ingredient.GetAll();
                var productingredient = _context.ProductIngredients.Where(x => x.ProductId == productId).ToList();

                foreach (var item in productingredient)
                {

                    if (item.ProductId == productId)
                    {
                        var ingredient = ingredients.FirstOrDefault(x => x.Id == item.IngredientId);
                        if (ingredient != null)
                        {
                            ingredient.Quantity -= item.Quantity * entity.Quantity;
                            _ingredient.Update(ingredient);
                        }
                    }
                }
                entity.StatusOfOrder = Restaurant.Entity.Enums.OrderStatus.Preparing;
                _orderService.Update(entity);
                TempData["Message"] = "Successful"; ;
                return RedirectToAction("ordertracking", "orderitem", "manager");
            }

            return RedirectToAction("ordertracking", "orderitem", "manager");


        }


        void Select()
        {

            ViewBag.TableSelect = _tableOfRestaurantService.GetAll().Select(x => new SelectListItem
            {
                Text = x.TableName,
                Value = x.Id.ToString()
            });


        }


    }
}
