using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.DAL.Context;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BillController : Controller
    {
        private readonly IBillService _billService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly IEmployeeService _employeeService;
        private readonly IProductService _productService;
        private readonly IOrderItemService _orderItemService;
        private readonly ProjectContext _context;

        public BillController(IBillService billService,ITableOfRestaurantService tableOfRestaurantService,IEmployeeService employeeService,IProductService productService,IOrderItemService orderItemService,ProjectContext context)
        {
          _billService = billService;
            _tableOfRestaurantService = tableOfRestaurantService;
           _employeeService = employeeService;
          _productService = productService;
            _orderItemService = orderItemService;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.EmployeeList = _employeeService.GetAll();
            var tableList = _tableOfRestaurantService.GetAll();
            return View(tableList);
        }
        public async Task<IActionResult> BillDetail(int id,BillDetailVM vM)
        {
            ViewBag.OrderItems = _orderItemService.GetAll();
            ViewBag.Product = _productService.GetAll();
            ViewBag.Tables = _tableOfRestaurantService.GetAll();

            //select t.TableName,p.ProductName,o.Quantity,p.Price,o.TotalPrice from TableOfRestaurants t
            //join OrderItems o on t.Id = o.TableofRestaurantId
            //join Products p on o.ProductId = p.Id
            //where t.Id = 2



            var query = from t in _context.TableOfRestaurants
                        join o in _context.OrderItems on t.Id equals o.TableofRestaurantId
                        join p in _context.Products on o.ProductId equals p.Id
                        where t.Id == id
                        select new BillDetailVM
                        {
                            TableName=t.TableName, 
                            ProductName=p.ProductName,
                            Quantity = o.Quantity,
                            Price = p.Price,
                            TotalPrice = o.TotalPrice
                        };
            var billDetails = query.ToList();

            //List<BillDetailVM> billDetails = new List<BillDetailVM>();


            ////for (int i = 1; i <= OrderItems.Count; i++)
            ////{
            //    foreach (var item in OrderItems)
            //    {
            //        var entity = new BillDetailVM
            //        {
            //            TableName = item.TableName,
            //            ProductName = item.ProductName,
            //            Quantity = item.Quantity,
            //            Price = item.Price,
            //            TotalPrice = item.TotalPrice
            //        };
            //        billDetails.Add(entity);
            //    }
            ////}
           
            return  View(billDetails);
        }
    }
    }

