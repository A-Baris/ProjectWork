using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly ICustomerService _customerService;
        private readonly ProjectContext _context;

        public ReservationController(IReservationService reservationService,ITableOfRestaurantService tableOfRestaurantService,ICustomerService customerService,ProjectContext context)
        {
           _reservationService = reservationService;
            _tableOfRestaurantService = tableOfRestaurantService;
            _customerService = customerService;
            _context = context;
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
        public IActionResult Create(ReservationCreateVM createVM)
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
                        };
                        _customerService.Create(customer);
                        reservation.CustomerId = customer.Id;
                        _reservationService.Update(reservation);
                    }
                    TempData["Message"] = "Successful";
                    return RedirectToAction("index", "reservation", new { area = "manager" });

                }
                TableAndCustomerSelect();
                TempData["ErrorMessage"] = "ModelState is invalid";
                return View(createVM);
            }
            TempData["ErrorMessage"] = "Reservation is full in the day";
            return View(createVM);

        }

        public async Task<IActionResult> Update(int id)
        {
            TableAndCustomerSelect();
            var reservation = await _reservationService.GetbyIdAsync(id);
            if (reservation != null)
            {
                var updated = new ReservationUpdateVM()
                {
                    Id = id,
                    ReservationDate = reservation.ReservationDate,
                    TableOfRestaurantId = reservation.TableOfRestaurantId,
                    CustomerId = reservation.CustomerId,
                    Description = reservation.Description,
                    ReservationStatus = reservation.ReservationStatus,
                };
              
               return View(updated);
            }
            return RedirectToAction("index", "reservation", new { area = "manager" });

        }
           
        [HttpPost]
        public async Task<IActionResult> Update(ReservationUpdateVM updatedVM)
        {
            if(ModelState.IsValid)
            {
                var entity = await _reservationService.GetbyIdAsync(updatedVM.Id);
                entity.ReservationDate = updatedVM.ReservationDate;
                entity.CustomerId = updatedVM.CustomerId;
                entity.TableOfRestaurantId=updatedVM.TableOfRestaurantId;
                entity.Description = updatedVM.Description;
                entity.ReservationStatus = updatedVM.ReservationStatus;
               _reservationService.Update(entity);
                TempData["Message"] = "Successful";
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
                _reservationService.Update(entity);
                TempData["Message"] = "Successful";
                return RedirectToAction("Index", "Reservation", new {area="manager"});
            }
            return View();
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
