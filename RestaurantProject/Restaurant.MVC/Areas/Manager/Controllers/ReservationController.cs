using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.Common;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ReservationController : AreaBaseController
    {
        private readonly IReservationService _reservationService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly ICustomerService _customerService;
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;
        private readonly IValidationService<ReservationCreateVM> _validationForCreate;

        public ReservationController(IReservationService reservationService, ITableOfRestaurantService tableOfRestaurantService, ICustomerService customerService, ProjectContext context, IMapper mapper, IValidationService<ReservationCreateVM> validationForCreate)
        {
            _reservationService = reservationService;
            _tableOfRestaurantService = tableOfRestaurantService;
            _customerService = customerService;
            _context = context;
            _mapper = mapper;
            _validationForCreate = validationForCreate;
        }
        [Authorize(Roles = "employee")]
        public IActionResult Index()
        {
            ViewBag.Tables = _tableOfRestaurantService.GetAll();
            ViewBag.Customers = _customerService.GetAll();

            var reservationList = _reservationService.GetAll().OrderBy(x => x.ReservationDate).ToList();
            return View(reservationList);

        }
        [Authorize(Roles = "employee")]
        public IActionResult ReservationDay(DateTime testDate)
        {
            TableAndCustomerSelect();



            var reservationList = _reservationService.GetAll().Where(x => x.ReservationDate.DayOfYear == testDate.DayOfYear).ToList();

            ViewBag.ReservationDay = reservationList;


            return View();
        }

        public IActionResult Create()
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "booker" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            TableAndCustomerSelect();
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(ReservationCreateVM createVM)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "booker" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
       
            ModelState.Clear();
            var errors = _validationForCreate.GetValidationErrors(createVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                TableAndCustomerSelect();
                return View(createVM);
            }
            var tables = _tableOfRestaurantService.GetAll().ToList();
            var reservations = _reservationService.GetAll().Where(x => x.ReservationDate.DayOfYear == createVM.ReservationDate.DayOfYear).ToList();



            if (tables.Count > reservations.Count)
            {



                var reservation = _mapper.Map<Reservation>(createVM);
                _reservationService.Create(reservation);
                if (reservation.CustomerId == null)
                {

                    var customer = _mapper.Map<Customer>(createVM.CustomerVM);
                    _customerService.Create(customer);
                    reservation.CustomerId = customer.Id;

                    _reservationService.Update(reservation);
                }
                if (createVM.CustomerVM.Email != null)
                {
                    MailSender.SendEmail(createVM.CustomerVM.Email, "Rezervasyon Bilgisi", $"Sayın {createVM.CustomerVM.Name} {createVM.CustomerVM.Surname},\nRezervasyonunuz başarıyla oluşturulmuştur." +
                                                 $" \nRezervasyon Tarihi : {createVM.ReservationDate} \nMisafir Sayısı:{createVM.GuestNumber}  \nNot: {createVM.Description} \nİyi günler dileriz..");
                }

                TempData.SetSuccessMessage();
                return RedirectToAction("index", "reservation", new { area = "manager" });

            }


            TempData["ErrorMessage"] = "Seçtiğiniz tarihteki rezervasyonlar dolu";
            return View(createVM);

        }

        public async Task<IActionResult> Update(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "booker" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
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

        public async Task<IActionResult> Update(int id, ReservationVM updatedVM)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "booker" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            if (ModelState.IsValid)
            {
                var entity = await _reservationService.GetbyIdAsync(id);
                _mapper.Map(updatedVM, entity);
                _reservationService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("Index", "Reservation", new { area = "manager" });

            }
            TableAndCustomerSelect();
            return View(updatedVM);


        }

        public async Task<IActionResult> Remove(int id)
        {
            if (!CheckAuthorization(new[] { "admin", "manager", "booker" }))
            {
                TempData.NoAuthorizationMessage();
                return RedirectToAction("Index", "Home", new { area = "manager" });
            }
            var entity = await _reservationService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                entity.ReservationStatus = Entity.Enums.ReservationStatus.Passive;
                _reservationService.Update(entity);
                TempData.SetSuccessMessage();
                return RedirectToAction("Index", "Reservation", new { area = "manager" });
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
