using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    
    public class InvoiceController : AreaBaseController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;
        private readonly IValidationService<InvoiceVM> _validationService;

        public InvoiceController(IInvoiceService invoiceService, IMapper mapper, IValidationService<InvoiceVM> validationService)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
            _validationService = validationService;
        }
        public IActionResult Index()
        {
            if (!CheckAuthorization(new[] { "admin", "manager","accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var invoices = _invoiceService.GetAll();
            return View(invoices);
        }

        public IActionResult Create()
        {

            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(InvoiceVM invoiceVM)
        {

            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(invoiceVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(invoiceVM);
            }

            var invoice = _mapper.Map<Invoice>(invoiceVM);
            _invoiceService.Create(invoice);
            TempData.SetSuccessMessage();
            return RedirectToAction("Index");



        }
        public async Task<IActionResult> Update(int id)
        {

            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var invoice = await _invoiceService.GetbyIdAsync(id);
            if (invoice != null)
            {
                var invoiceUpdated = _mapper.Map<InvoiceVM>(invoice);
                return View(invoiceUpdated);
            }
            TempData.NotFoundId();
            return View("index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, InvoiceVM invoiceVM)
        {

            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            ModelState.Clear();
            var errros = _validationService.GetValidationErrors(invoiceVM);
            if (errros.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errros);
                TempData.SetErrorMessage();
                return View(invoiceVM);
            }


            var invoice = await _invoiceService.GetbyIdAsync(id);
            if (invoice != null)
            {
                _mapper.Map(invoiceVM, invoice);
                _invoiceService.Update(invoice);
                TempData.SetSuccessMessage();
                return RedirectToAction("index");
            }
            else
            {
                TempData.NotFoundId();
                return RedirectToAction("index");

            }

        }
        public async Task<IActionResult> Remove(int id)
        {

            if (!CheckAuthorization(new[] { "admin", "manager", "accountant" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var invoice = await _invoiceService.GetbyIdAsync(id);
            if (invoice != null)
            {
                invoice.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _invoiceService.Update(invoice);
                TempData.SetSuccessMessage();
                return RedirectToAction("Index");
            }
            TempData.NotFoundId();
            return RedirectToAction("index");

        }
    }

}
