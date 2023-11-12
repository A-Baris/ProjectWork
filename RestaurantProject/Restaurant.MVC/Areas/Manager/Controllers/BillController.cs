using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.DAL.Context;
using System.Text;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BillController : Controller
    {
    
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly IEmployeeService _employeeService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderItemService;
        private readonly ProjectContext _context;

        public BillController(ITableOfRestaurantService tableOfRestaurantService, IEmployeeService employeeService, IProductService productService, IOrderService orderItemService, ProjectContext context)
        {
            
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
                        join o in _context.Orders on t.Id equals o.TableofRestaurantId
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

            //select o.Id from Order o
            //join TableOfRestaurants t on o.TableofRestaurantId = t.Id
            //where t.TableName = 'k-2'


            var query = from o in _context.Orders
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
                tableEntity.BillRequest = Entity.Enums.BillRequest.Passive;
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
                TempData["Message"] = "Successful";
                return RedirectToAction("billdetail", "bill", new { area = "manager",id=tableEntity.Id });
            }
            return RedirectToAction("index", "bill", new { area = "manager" });
        }

        public async Task<IActionResult> BillRequestReady(int id)
        {
            var table = await _tableOfRestaurantService.GetbyIdAsync(id);
            if(table!=null)
            {
                table.BillRequest = Entity.Enums.BillRequest.Ready;
                _tableOfRestaurantService.Update(table);
                TempData["Message"] = "İşlem başarılı";
                return RedirectToAction("index", "bill", new { area = "manager" });
            }
            return RedirectToAction("index", "bill", new { area = "manager" });

        }
        //public async Task<IActionResult> PartOfPayment(string tableName,decimal payment)
        //{
        //    var table = _tableOfRestaurantService.GetAll().Where(x => x.TableName == tableName).FirstOrDefault();
        //    var bill = _orderItemService.GetAll().Where(x=>x.TableofRestaurantId==table.Id && x.BaseStatus == Entity.Enums.BaseStatus.Active).FirstOrDefault();
        //    if (bill != null)
        //    {
        //        bill.TotalPrice -= payment;
        //        _orderItemService.Update(bill);
        //        TempData["Message"] = "Successful";
        //        return RedirectToAction("billdetail", "bill", new { area = "manager", id = table.Id });
        //    }

        //   return RedirectToAction("billdetail", "bill", new {area="manager",id=table.Id});
        //}
        //[HttpPost]
     
        //public IActionResult SaveToFile(string data)
        //{
        //    // Generate the content for the file based on your requirements
        //    StringBuilder content = new StringBuilder();

        //    foreach (var item in data)
        //    {
        //        //content.AppendLine($"Table: {item.TableOfRestaurant.TableName}, Product: {item.Product.ProductName}, Quantity: {item.Quantity}, Price: {item.Product.Price}, TotalPrice: {item.TotalPrice}");
        //        content.AppendLine(data);
        //    }

        //    // Save the content to a file
        //    System.IO.File.WriteAllText("D:/Hesap/bill_details.txt", content.ToString());

        //    // Redirect back to the BillDetail action
        //    return RedirectToAction("BillDetail");
        //}
    }
}


