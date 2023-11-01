using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceService invoiceService,IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var invoices = _invoiceService.GetAll();
            return View(invoices);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(InvoiceVM invoiceVM)
        {
            if(ModelState.IsValid)
            {
                var invoice = _mapper.Map<Invoice>(invoiceVM);
                _invoiceService.Create(invoice);
                TempData["Message"] = "Creating is successful";
                return RedirectToAction("Index");


            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            return View(invoiceVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            var invoice = await _invoiceService.GetbyIdAsync(id);
            if(invoice!=null)
            {
                var invoiceUpdated = _mapper.Map<InvoiceVM>(invoice);
                return View(invoiceUpdated);
            }
            TempData["ErrorMessage"] = "Id is not found";
            return View("index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,InvoiceVM invoiceVM)
        {
            if(ModelState.IsValid)
            {
                var invoice = await _invoiceService.GetbyIdAsync(id);
                _mapper.Map(invoiceVM, invoice);
                _invoiceService.Update(invoice);
                TempData["Message"] = "Update is successful";
                return RedirectToAction("index");
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            return View(invoiceVM);
        }
        public async Task<IActionResult> Remove(int id)
        {
            var invoice = await _invoiceService.GetbyIdAsync(id);
            if(invoice!=null)
            {
                invoice.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _invoiceService.Update(invoice);
                TempData["Message"] = "Deleting is succcessful";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Id is not found";
            return View("index");
            
        }
    }

}
