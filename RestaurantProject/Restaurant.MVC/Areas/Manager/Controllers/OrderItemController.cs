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
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class OrderItemController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IEmployeeService _employeeService;
        private readonly IProductService _productService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly ProjectContext _context;
        private readonly IOrderItemService _orderItem;
        private readonly IIngredientService _ingredient;

        public OrderItemController(IOrderService orderService, IEmployeeService employeeService, IProductService productService, ITableOfRestaurantService tableOfRestaurantService, ProjectContext context,IOrderItemService orderItem,IIngredientService ingredient)
        {
            _orderService = orderService;
            _employeeService = employeeService;
            _productService = productService;
            _tableOfRestaurantService = tableOfRestaurantService;
            _context = context;
            _orderItem = orderItem;
          _ingredient = ingredient;
        }

        public IActionResult Index()
        {
            ViewBag.Dish = _productService.GetSelectedProducts("Dish");
            ViewBag.Drink = _productService.GetSelectedProducts("Drink");
            return View();
        }
   
        public IActionResult CreateOrder()
        {
            Select();
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateOrder(int Id, OrderItemCreateVM createVM)
        {
            
            createVM.ProductId= Id;
            //Order orderSession;
            //if(SessionHelper.GetProductFromJson<Order>(HttpContext.Session,"siparis")==null)
            //{
            //    orderSession=new Order();
            //}
            //else
            //{
            //    orderSession = SessionHelper.GetProductFromJson<Order>(HttpContext.Session, "siparis");
            //}

         
            var product = await _productService.GetbyIdAsync(Id);
            if(product==null)
            {
                return View("Index");
            }
            else
            {
                var od = _orderItem.GetAll();
                if (od.Any(x => x.TableofRestaurantId == createVM.TableofRestaurantId && x.ProductId == createVM.ProductId))
                {
                    var update = _orderItem.GetAll().Where(x => x.TableofRestaurantId == createVM.TableofRestaurantId && x.ProductId == createVM.ProductId).FirstOrDefault();
                    update.Quantity += createVM.Quantity;
                    update.TotalPrice *= update.Quantity;
                    _orderItem.Update(update);
                }


                else
                {

                    OrderItem orderItem = new OrderItem()
                    {
                        ProductId = product.Id,
                        Quantity = createVM.Quantity,
                        TotalPrice = product.Price * createVM.Quantity,
                        TableofRestaurantId = createVM.TableofRestaurantId,
                        EmployeeId = 1,
                        Description = createVM.Description,
                    };
                    //orderSession.AddItem(orderItem);
                    _orderItem.Create(orderItem);


                    //SessionHelper.SetJsonProduct(HttpContext.Session, "siparis", orderSession);

                }


            }
            return RedirectToAction("index", "orderitem", new { arrea = "manager" });
           
        }
                  
        public IActionResult SelectTable()
        {

            return View();
        }

        [Authorize(Roles = "Chief")]
        public IActionResult OrderTracking()
        {
            ViewBag.Tables = _tableOfRestaurantService.GetAll();
            ViewBag.Products = _productService.GetAll();
            var orderList=_orderItem.GetAll();
            return View(orderList);
        }
        public async Task<IActionResult> OrderReady(int id)
        {
            var entity = await _orderItem.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.StatusOfOrder = Restaurant.Entity.Enums.OrderStatus.Ready;
                _orderItem.Update(entity);
            }
            return RedirectToAction("ListOrder", "Orderitem", "manager");
        }
        public async Task<IActionResult> OrderDelivered(int id)
        {
            var entity = await _orderItem.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.StatusOfOrder = Restaurant.Entity.Enums.OrderStatus.Delivered;
                _orderItem.Update(entity);
            }
            return RedirectToAction("ListOrder", "Orderitem", "manager");
        }
        public async Task<IActionResult> OrderPreparing(int id) 
        {
            // ürün hazırlanmaya başladığında o ürünü oluştumrak için gerekli malzeme miktarı stoktaki malzeme miktarlarından düşülecek
            var entity = await _orderItem.GetbyIdAsync(id);
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
                            ingredient.Quantity -= item.Quantity;
                            _ingredient.Update(ingredient);
                        }
                    }
                }
                entity.StatusOfOrder = Restaurant.Entity.Enums.OrderStatus.Preparing;
                _orderItem.Update(entity);
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
