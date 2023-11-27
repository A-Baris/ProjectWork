using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;
using System.Text.RegularExpressions;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class AccountingController : AreaBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderItemService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly IAccountingTransactionService _transactionService;
        private readonly ISupplierService _supplierService;
        private readonly ProjectContext _context;
        private readonly IValidationService<TransactionVM> _validationService;

        public AccountingController(IMapper mapper, IOrderService orderItemService, ITableOfRestaurantService tableOfRestaurantService, IAccountingTransactionService transactionService, ISupplierService supplierService, ProjectContext context, IValidationService<TransactionVM> validationService)
        {
            _mapper = mapper;
            _orderItemService = orderItemService;
            _tableOfRestaurantService = tableOfRestaurantService;
            _transactionService = transactionService;
            _supplierService = supplierService;
            _context = context;
            _validationService = validationService;
        }
       
        public IActionResult Index()
        {
            //if (!User.IsInRole("accountant"))
            //{

            //    TempData["ErrorMessage"] = "Bu sayfaya erişme yetkiniz yoktur";
            //    return RedirectToAction("index", "Home", new {area="manager"});
            //}
            if (!CheckAuthorization(new[] { "admin", "manager","accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            SelectSupplier();
            var transactions = _transactionService.GetAll();
            return View(transactions);
        }
    
        public IActionResult CashTracking()
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
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

            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
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
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            SelectSupplier();

            return View();
        }
        [HttpPost]
        public IActionResult CreateTransaction(TransactionVM transactionVM)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(transactionVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                SelectSupplier();
                return View(transactionVM);
            }

            var transaction = _mapper.Map<AccountingTransaction>(transactionVM);
            _transactionService.Create(transaction);
            TempData.SetSuccessMessage();
            return RedirectToAction("Index", "Accounting", new { area = "manager" });


        }

        public async Task<IActionResult> UpdateTransaction(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            SelectSupplier();
            var entity = await _transactionService.GetbyIdAsync(id);
            if (entity != null)
            {
                var updated = _mapper.Map<TransactionVM>(entity);
                return View(updated);
            }
            TempData.NotFoundId();
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionVM transactionVM)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(transactionVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                SelectSupplier();
                return View(transactionVM);
            }

            var entity = await _transactionService.GetbyIdAsync(id);
            if (entity != null)
            {
                _mapper.Map(transactionVM, entity);
                _transactionService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("index");
            }
            else
            {
                TempData.NotFoundId();
                return RedirectToAction("Index");
            }



        }
        public async Task<IActionResult> RemoveTransaction(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData["ErrorMessage"] = "Bu Sayfa için yetkiniz yok";
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var entity = await _transactionService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _transactionService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("index");
            }
            TempData.NotFoundId();
            return View("Index");
        }
        public IActionResult Debit()
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData["ErrorMessage"] = "Bu Sayfa için yetkiniz yok";
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
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
