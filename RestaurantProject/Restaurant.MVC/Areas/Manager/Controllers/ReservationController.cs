﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.Common;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.TempDataHelpers;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly ICustomerService _customerService;
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService,ITableOfRestaurantService tableOfRestaurantService,ICustomerService customerService,ProjectContext context,IMapper mapper)
        {
           _reservationService = reservationService;
            _tableOfRestaurantService = tableOfRestaurantService;
            _customerService = customerService;
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.Tables = _tableOfRestaurantService.GetAll();
            ViewBag.Customers = _customerService.GetAll();

            var reservationList = _reservationService.GetAll();
            return View(reservationList);
           
        }
        public IActionResult ReservationDay(DateTime testDate)
        {
            TableAndCustomerSelect();
         


            var reservationList = _reservationService.GetAll().Where(x => x.ReservationDate.DayOfYear == testDate.DayOfYear).ToList();

            ViewBag.ReservationDay = reservationList;


            return View();
        }

        public IActionResult Create()
        {
            TableAndCustomerSelect();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateVM createVM)
        {
            var tables = _tableOfRestaurantService.GetAll().ToList();
            var reservations = _reservationService.GetAll().Where(x => x.ReservationDate.DayOfYear == createVM.ReservationDate.DayOfYear).ToList();
          
          

            if (tables.Count > reservations.Count)
            {
                

                if (ModelState.IsValid)
                {
                    Reservation reservation = new Reservation()
                    {
                        ReservationDate = createVM.ReservationDate,
                        TableOfRestaurantId = createVM.TableOfRestaurantId,
                        GuestNumber = createVM.GuestNumber,
                        CustomerId = createVM.CustomerId,
                        Description = createVM.Description,
                       

                    };
                    _reservationService.Create(reservation);
                    if (reservation.CustomerId == null)
                    {

                        Customer customer = new Customer()
                        {
                            Name = createVM.Name,
                            Surname = createVM.Surname,
                            Phone = createVM.Phone,
                            Adress = createVM.Adress,
                            Email = createVM.Email,
                        };
                        _customerService.Create(customer);
                        reservation.CustomerId = customer.Id;
                      
                        _reservationService.Update(reservation);
                    }
                    if (createVM.Email != null)
                    {
                        MailSender.SendEmail(createVM.Email, "Rezervasyon Bilgisi", $"Sayın {createVM.Name} {createVM.Surname},\nRezervasyonunuz başarıyla oluşturulmuştur." +
                                                     $" \nRezervasyon Tarihi : {createVM.ReservationDate}  \nNot: {createVM.Description} \nİyi günler dileriz..");
                    }
                    //else
                    //{
                    //    MailSender.SendEmail(createVM.Email, "Rezervasyon Bilgisi", $"Sayın {createVM.Name} {createVM.Surname},\nRezervasyonunuz başarıyla oluşturulmuştur." +
                    //                                 $" \nRezervasyon Tarihi : {createVM.ReservationDate}  \nNot: {createVM.Description} \nİyi günler dileriz..");
                    //}
                    TempData["Message"] = "İşlem başarılı";
                    return RedirectToAction("index", "reservation", new { area = "manager" });

                }
                TableAndCustomerSelect();
                TempData["ErrorMessage"] = "ModelState is invalid";
                return View(createVM);
            }
            TempData["ErrorMessage"] = "Seçtiğiniz tarihteki rezervasyonlar dolu";
            return View(createVM);

        }

        public async Task<IActionResult> Update(int id)
        {
            TableAndCustomerSelect();
            var reservation = await _reservationService.GetbyIdAsync(id);
            if (reservation != null)
            {
               
              var updated = _mapper.Map<ReservationVM>(reservation);
               return View(updated);
            }
            return RedirectToAction("index", "reservation", new { area = "manager" });

        }
           
        [HttpPost]
        public async Task<IActionResult> Update(int id,ReservationVM updatedVM)
        {
            if(ModelState.IsValid)
            {
                var entity = await _reservationService.GetbyIdAsync(id);
                _mapper.Map(updatedVM, entity);
               _reservationService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("Index", "Reservation", new {area="manager"});

            }
            TableAndCustomerSelect();
            return View(updatedVM);
        
            
        }

        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _reservationService.GetbyIdAsync(id);
            if(entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                entity.ReservationStatus = Entity.Enums.ReservationStatus.Passive;
                _reservationService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("Index", "Reservation", new {area="manager"});
            }
            TempData.NotFoundId();
            return RedirectToAction("Index");
        }

    

        void TableAndCustomerSelect()
        {
            ViewBag.Table = _tableOfRestaurantService.GetAll().Select(t => new SelectListItem
            {
                Text = t.TableName,
                Value = t.Id.ToString(),
            });
            ViewBag.Customer = _customerService.GetAll().Select(c => new SelectListItem
            {
                Text = $"{c.Name} {c.Surname}",
                Value = c.Id.ToString(),
            });

        }
    }
}
