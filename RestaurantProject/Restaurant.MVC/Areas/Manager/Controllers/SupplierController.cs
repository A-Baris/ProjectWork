﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly IValidationService<SupplierVM> _validationService;

        public SupplierController(ISupplierService supplierService, IMapper mapper, IValidationService<SupplierVM> validationService)
        {
            _supplierService = supplierService;
            _mapper = mapper;
            _validationService = validationService;
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
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(supplierVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(supplierVM);
            }

            var supplier = _mapper.Map<Supplier>(supplierVM);
            _supplierService.Create(supplier);
            TempData.SetSuccessMessage();
            return RedirectToAction("Index", "supplier", new { area = "manager" });


        }
        public async Task<IActionResult> Update(int Id)
        {
            var entity = await _supplierService.GetbyIdAsync(Id);
            if (entity != null)
            {
                var updated = _mapper.Map<SupplierVM>(entity);
                return View(updated);
            }
            TempData.NotFoundId();
            return RedirectToAction("Index", "supplier", new { area = "manager" });

        }
        [HttpPost]
        public async Task<IActionResult> Update(int Id, SupplierVM supplierVM)
        {
            var entity = await _supplierService.GetbyIdAsync(Id);
            if (entity == null)
            {
                TempData.NotFoundId();
                return RedirectToAction("index");
            }

            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(supplierVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(supplierVM);
            }



            _mapper.Map(supplierVM, entity);
            _supplierService.Update(entity);

            TempData.SetSuccessMessage();
            return RedirectToAction("Index", "supplier", new { area = "manager" });



        }
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _supplierService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _supplierService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("Index", "supplier", new { area = "manager" });
            }
            return RedirectToAction("Index", "supplier", new { area = "manager" });

        }
    }
}