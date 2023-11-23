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
using Restaurant.MVC.Utility.TempDataHelpers;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    
    public class OrderItemController : AreaBaseController
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

            if (!CheckAuthorization(new[] { "admin", "manager","waiter" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ViewBag.Category = _categoryService.GetAll();
            ViewBag.Menus = _menuService.GetAll();
          
            TempData["TableId"] = id;
            var products = _productService.GetAll();
            return View(products);
        }

        
        public IActionResult CreateOrder()
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "waiter" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            Select();
            return View();
        }



        [HttpPost]
       
        public async Task<IActionResult> CreateOrder(int tableId, int Id, OrderItemCreateVM createVM)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "waiter" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }

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
                       
                     
                        TempData.SetSuccessMessage();
                        //TempData["TableId"] = tableId;
                        ViewBag.Dish = _productService.GetSelectedProducts("Dish");
                        return RedirectToAction("selectproduct", "orderitem", new { area = "manager",id=tableId });
                    }
                    TempData["ErrorMessage"] = "ModelState is invalid";
                    return View(createVM);

                }


            }

            //TempData["TableId"] = tableId;
            TempData.SetSuccessMessage();
            return RedirectToAction("selectproduct", "orderitem", new { area = "manager", id = tableId });

        }



        
        public IActionResult Index()
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "waiter" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var tables = _tableOfRestaurantService.GetAll().OrderBy(x=>x.TableName).ToList();
            return View(tables);
        }
       
        public async Task<IActionResult> Update(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "waiter" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }

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
            if (!CheckAuthorization(new[] { "admin", "manager", "waiter" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
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
            if (!CheckAuthorization(new[] { "admin", "manager", "waiter" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var entity = await _orderService.GetbyIdAsync(id);
            if (entity != null)
            {
                _orderService.Remove(entity);
                TempData["Message"] = "Order is deleted";
                return RedirectToAction("orderlist", "tableofrestaurant", new { area = "Manager", id = entity.TableofRestaurantId });
            }
            return RedirectToAction("orderlist", "tableofrestaurant", new { area = "Manager", id = entity.TableofRestaurantId });

        }

 
        public IActionResult OrderTracking()
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "chef" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            //signalR araştır!
            ViewBag.Tables = _tableOfRestaurantService.GetAll();
            ViewBag.Products = _productService.GetAll();
            var orderList = _orderService.GetAll();

            return View(orderList);
        }
        
        public async Task<IActionResult> OrderReady(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "chef" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var entity = await _orderService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.StatusOfOrder = Restaurant.Entity.Enums.OrderStatus.Ready;
                _orderService.Update(entity);
            }
            TempData["Message"] = "Successful";
            return RedirectToAction("ordertracking", "Orderitem", "manager");
        }
        
        public async Task<IActionResult> OrderDelivered(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "chef" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var entity = await _orderService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.StatusOfOrder = Restaurant.Entity.Enums.OrderStatus.Delivered;
                _orderService.Update(entity);
            }
            TempData["Message"] = "Successful";
            return RedirectToAction("ordertracking", "Orderitem", "manager");
        }
       
        public async Task<IActionResult> OrderPreparing(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "chef" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            // ürün hazırlanmaya başladığında o ürünü oluşturmak için gerekli malzeme miktarı ürün adetine göre stoktaki malzeme miktarlarından düşülecek
            var products = _productService.GetAll();
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
                            if (ingredient.Quantity >= 0)
                            {
                                _ingredient.Update(ingredient);
                            }
                            else
                            {
                                TempData["ErrorMessage"] = $"Ingredient is not enough for {entity.Product.ProductName}"; ;
                                return RedirectToAction("ordertracking", "orderitem", "manager");
                            }

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
