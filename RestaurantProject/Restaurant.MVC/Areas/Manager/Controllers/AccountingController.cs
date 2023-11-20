using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using System.Text.RegularExpressions;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class AccountingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderItemService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly IAccountingTransactionService _transactionService;
        private readonly ISupplierService _supplierService;
        private readonly ProjectContext _context;

        public AccountingController(IMapper mapper,IOrderService orderItemService,ITableOfRestaurantService tableOfRestaurantService,IAccountingTransactionService transactionService,ISupplierService supplierService,ProjectContext context)
        {
            _mapper = mapper;
            _orderItemService = orderItemService;
           _tableOfRestaurantService = tableOfRestaurantService;
            _transactionService = transactionService;
            _supplierService = supplierService;
            _context = context;
        }
        public IActionResult Index()
        {
            SelectSupplier();
          var transactions =  _transactionService.GetAll();
         return View(transactions);
        }

        public IActionResult CashTracking()
        {
            try
            {
                var cashList = _orderItemService.GetAllDeletedStatus()
                    .GroupBy(x => x.CreatedDate.Date)
                    .Select(group => new
                    {
                        Day = group.Key,
                        TotalPrice = group.Sum(x => x.TotalPrice),
                    });
                ViewBag.CashTracking = cashList.ToList();
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        public IActionResult ReportOfTurnover(int targetYear, int targetMonth)
        {
              // SELECT DATEPART(WEEKDAY, CreatedDate),SUM(TotalPrice)
             //FROM OrderItems where DATEPART(MONTH, CreatedDate) = DATEPART(MONTH, '2023-10-22') group by DATEPART(WEEKDAY, CreatedDate)


            var orderItemsForMonth = _orderItemService.GetAllDeletedStatus()
                .Where(item => item.CreatedDate.Year == targetYear && item.CreatedDate.Month == targetMonth)
                .ToList(); 

            var weeklyTotalPrices = orderItemsForMonth
                .GroupBy(item => item.CreatedDate.Date)
                .Select(group => new
                {
                    DayOfYear = group.Key,
                    TotalPriceSum = group.Sum(item => item.TotalPrice)
                })
                .ToList();

            ViewBag.Weekly = weeklyTotalPrices.ToList();


            return View(weeklyTotalPrices);
        }
     

        public IActionResult CreateTransaction()
        {
            SelectSupplier();
            
            return View();
        }
        [HttpPost]
        public IActionResult CreateTransaction(TransactionVM transactionVM)
        {
            if(ModelState.IsValid)
            {
                var transaction = _mapper.Map<AccountingTransaction>(transactionVM);
                _transactionService.Create(transaction);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "Accounting", new {area="manager"});
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            SelectSupplier();
            return View(transactionVM);
        }
     
        public async Task<IActionResult> UpdateTransaction(int id)
        {
            SelectSupplier();
            var entity = await _transactionService.GetbyIdAsync(id);
            if (entity != null)
            {
                var updated = _mapper.Map<TransactionVM>(entity);
                return View(updated);
            }
            TempData["ErrorMessage"] = $"{id} is Not found";
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTransaction(int id,TransactionVM transactionVM)
        {
            if(ModelState.IsValid)
            {
                var entity = await _transactionService.GetbyIdAsync(id);
                if(entity!=null)
                {
                    _mapper.Map(transactionVM, entity);
                    _transactionService.Update(entity);
                    TempData["Message"] = "Successful";
                    return RedirectToAction("index");
                }
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            return View(transactionVM);

        }
        public async Task<IActionResult> RemoveTransaction(int id)
        {
            var entity = await _transactionService.GetbyIdAsync(id);
            if(entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _transactionService.Update(entity);
                TempData["Message"] = "Successful";
                return RedirectToAction("index");
            }
            TempData["ErrorMessage"] = "Not Found";
            return View("Index");
        }
        public IActionResult Debit()
        {
            return View();
        }
       



        void SelectSupplier()
        {
            ViewBag.Supplier = _supplierService.GetAll().Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.Id.ToString()
            });
        }
    }
}
