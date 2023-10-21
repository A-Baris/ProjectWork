using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

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

//            select* from Reservations r
//join Customers c on r.CustomerId = c.Id
//join TableOfRestaurants t on r.TableOfRestaurantId = t.Id
//where DATEPART(DAYOFYEAR, r.ReservationDate) = DATEPART(DAYOFYEAR, '2023-10-21');


            ViewBag.Tables = _tableOfRestaurantService.GetAll();
            ViewBag.Customers = _customerService.GetAll();
            var reservationQuery = (from r in _context.Reservations
                                   join t in _context.TableOfRestaurants on r.TableOfRestaurantId equals t.Id
                                   join c in _context.Customers on r.CustomerId equals c.Id
                                   select new ReservationDayVM
                                   {
                                       ReservationId = r.Id,
                                       ReservationDate = r.ReservationDate,
                                       TableName = t.TableName,
                                       CustomerName = $"{c.Name} {c.Surname}",                                      
                                       ReservationStatus = r.ReservationStatus,
                                       Description = r.Description,
                                   }).ToList();

            var reservationList = reservationQuery.Where(x=>x.ReservationDate.DayOfYear==testDate.DayOfYear).ToList();

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
            if(ModelState.IsValid)
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
