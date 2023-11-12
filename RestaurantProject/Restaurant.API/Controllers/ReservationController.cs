using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.Common;
using Restaurant.DAL.Context;
using Restaurant.DAL.Data;
using Restaurant.Entity.DTOs;
using Restaurant.Entity.Entities;

namespace Restaurant.API.Controllers
{
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
   
        private readonly ITableOfRestaurantService _tableOfRestaurant;
        private readonly ProjectContext _context;

        public ReservationController(IReservationService reservationService,ICustomerService customerService,IMapper mapper,ITableOfRestaurantService tableOfRestaurant,ProjectContext context)
        {
            _reservationService = reservationService;
            _customerService = customerService;
            _mapper = mapper;
          
            _tableOfRestaurant = tableOfRestaurant;
          _context = context;
        }

       

        [HttpPost]
       
        public async Task<IActionResult> PostReservation(ReservationCustomerDTO reservationCustomerDTO)
        {

         
            var reservationEntity = _mapper.Map<Reservation>(reservationCustomerDTO.Reservation);
            var customer = _customerService.GetAll().Where(x => x.Email == reservationCustomerDTO.Customer.Email).First();
            if (customer.Adress == null || customer.Phone == null)
            {
                customer.Adress = reservationCustomerDTO.Customer.Adress;
                customer.Phone = reservationCustomerDTO.Customer.Phone;
                _customerService.Update(customer);
            }
        
            
            reservationEntity.CustomerId = customer.Id;
            _reservationService.Create(reservationEntity);

            MailSender.SendEmail(customer.Email, @"Rezervasyon Bilgisi", $"Sayın {customer.Name} {customer.Surname}, rezervasyonunuz başarıyla oluşturulmuştur." +
                                                 $" \nRezervasyon Tarihi : {reservationCustomerDTO.Reservation.ReservationDate}  \nNot: {reservationCustomerDTO.Reservation.Description}");

          
            return Ok();



        }

        [HttpPost]



        [HttpGet]
        public IActionResult GetReservationDate(DateTime date)
        {

            var reservationList = _reservationService.GetAll().Where(x => x.ReservationDate.DayOfYear == date.DayOfYear).ToList();

            return Ok(reservationList);
        }

        [HttpGet]
        public IActionResult GetReservationDates(DateTime date)
        {

            var query = from r in _context.Reservations
                        join c in _context.Customers on r.CustomerId equals c.Id
                        select new
                        {
                            r.ReservationDate,
                            r.Description,
                            r.ReservationStatus,
                            c.Name,
                            c.Surname
                        };
            var reservationList = query.Where(x=>x.ReservationDate.DayOfYear == date.DayOfYear).ToList();

            return Ok(reservationList);
        }
    }
}
