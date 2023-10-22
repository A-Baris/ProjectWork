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

        public BillController(IBillService billService, ITableOfRestaurantService tableOfRestaurantService, IEmployeeService employeeService, IProductService productService, IOrderItemService orderItemService, ProjectContext context)
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
        public async Task<IActionResult> BillDetail(int id, BillDetailVM vM)
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
                        where t.Id == id && o.BaseStatus == 0
                        select new BillDetailVM
                        {
                            Id = t.Id,
                            TableName = t.TableName,
                            ProductName = p.ProductName,
                            Quantity = o.Quantity,
                            Price = p.Price,
                            TotalPrice = o.TotalPrice
                        };
            var billDetails = query.ToList();

         

            return View(billDetails);
        }

 
        public async Task<IActionResult> CompletePayment(string tableName)
        {

            //select o.Id from OrderItems o
            //join TableOfRestaurants t on o.TableofRestaurantId = t.Id
            //where t.TableName = 'k-2'


            var query = from o in _context.OrderItems
                        join t in _context.TableOfRestaurants on o.TableofRestaurantId equals t.Id
                        where t.TableName == tableName
                        select new BillCompleteVM
                        {
                            Id = o.Id,
                        };

            var orderItemIds = query.ToList();

            var tableEntity = _tableOfRestaurantService.GetAll().Where(x => x.TableName == tableName).FirstOrDefault();
            if (tableEntity != null)
            {
                tableEntity.Status = Entity.Enums.ReservationStatus.Passive;
                _tableOfRestaurantService.Update(tableEntity);

                foreach (var item in orderItemIds)
                {
                    var entity = await _orderItemService.GetbyIdAsync(item.Id);
                    if (entity != null)
                    {
                        entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                        _orderItemService.Update(entity);

                    }
                }
                //işlem mesajı
                return View();
            }
            return RedirectToAction("index", "bill", new { area = "manager" });
        }
    }
}

