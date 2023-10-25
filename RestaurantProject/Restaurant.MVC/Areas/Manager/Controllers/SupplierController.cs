using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var suppliers = _supplierService.GetAll();
            return View(suppliers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SupplierVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                var supplier = _mapper.Map<Supplier>(supplierVM);
                _supplierService.Create(supplier);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "supplier", new { area = "manager" });
            }
            return View(supplierVM);
        }
        public async Task<IActionResult> Update(int Id)
        {
            var entity = await _supplierService.GetbyIdAsync(Id);
            if (entity != null)
            {
                var updated = _mapper.Map<SupplierVM>(entity);
                return View(updated);
            }
            return RedirectToAction("Index", "supplier", new { area = "manager" });

        }
        [HttpPost]
        public async Task<IActionResult> Update(int Id, SupplierVM supplierVM)
        {

            if (ModelState.IsValid)
            {
                var entity = await _supplierService.GetbyIdAsync(Id);
                if (entity != null)
                {
                    _mapper.Map(supplierVM, entity); 
                    _supplierService.Update(entity);
                }
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "supplier", new { area = "manager" });

            }
            return View(supplierVM);

        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _supplierService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _supplierService.Update(entity);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "supplier", new { area = "manager" });
            }
            return RedirectToAction("Index", "supplier", new { area = "manager" });

        }
    }
}