using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IEmployeeService _employeeService;
        private readonly IKitchenService _kitchenService;
        private readonly IProductService _productService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly ProjectContext _context;

        public OrderController(IOrderService orderService, IEmployeeService employeeService, IKitchenService kitchenService, IProductService productService, ITableOfRestaurantService tableOfRestaurantService, ProjectContext context)
        {
            _orderService = orderService;
            _employeeService = employeeService;
            _kitchenService = kitchenService;
            _productService = productService;
            _tableOfRestaurantService = tableOfRestaurantService;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Dish = _productService.GetSelectedProducts("Dish");
            ViewBag.Drink = _productService.GetSelectedProducts("Drink");
            return View();
        }
        //public async Task<IActionResult> AddOrder(int id)
        //{
        //    Order2 ordersession;
        //    if(SessionHelper.GetProductFromJson<Order2>(HttpContext.Session,"siparis")==null)
        //    {
        //        ordersession = new Order2();
        //    }
        //    else
        //    {
        //        ordersession = SessionHelper.GetProductFromJson<Order2>(HttpContext.Session, "siparis");
        //    }
        //    var product = await _productService.GetbyIdAsync(id);
        //    if (product == null)
        //    {
        //        return RedirectToAction("Index", "order", new { area = "manager" });
        //    }
        //    else
        //    {
        //        OrderItem item = new OrderItem()
        //        {
        //            Id = id,
        //            Price = product.Price,
        //            ProductName = product.ProductName,

        //        };
        //       ordersession.AddItem(item);
        //        SessionHelper.SetJsonProduct(HttpContext.Session, "siparis", ordersession);
        //        TempData["OrderCount"] = ordersession._item.Count;
        //    }
        //    return RedirectToAction("Index", "order", new { area = "manager" });
        //}

        public IActionResult Create()
        {
            Select();
          

            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderCreateVM createVM)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    Description = createVM.Description,
                    StatusOfOrder = createVM.StatusOfOrder,
                    TableofRestaurantId = createVM.TableofRestaurantId,
                    EmployeeId = createVM.EmployeeId,
                    KitchenId = createVM.KitchenId,


                };
                _orderService.Create(order);
                int orderId = order.Id;

                createVM.Quantity.RemoveAll(item => item == 0);


                for (int i = 0; i < createVM.SelectedProductId.Count; i++)
                {
                  

                            OrderProduct orderProduct = new OrderProduct()
                            {
                                OrderId = orderId,
                                ProductId = createVM.SelectedProductId[i],
                                Quantity = createVM.Quantity[i]

                            };
                            _context.Set<OrderProduct>().Add(orderProduct);
                            _context.SaveChanges();
                          
                          

                        
                        
                    
                    
                }
             
               
                
              

              

                return RedirectToAction("index", "order", new { area = "Manager" });

            }

            Select();
            return View(createVM);
        }
        public IActionResult OrderTracking()
        {

            return View();
        }

        void Select()
        {
            ViewBag.EmployeeSelect = _employeeService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.KitchenSelect = _kitchenService.GetAll().Select(x => new SelectListItem
            {
                Text = x.KitchenName,
                Value = x.Id.ToString()
            });
            ViewBag.TableSelect = _tableOfRestaurantService.GetAll().Select(x => new SelectListItem
            {
                Text = x.TableName,
                Value = x.Id.ToString()
            });
            ViewBag.ProductSelect = _productService.GetAll().Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.Id.ToString()

            }).ToList();

        }

    }




}


