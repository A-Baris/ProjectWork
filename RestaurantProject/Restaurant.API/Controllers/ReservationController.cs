using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.Common;
using Restaurant.DAL.Context;
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

        public ReservationController(IReservationService reservationService,ICustomerService customerService,IMapper mapper,ITableOfRestaurantService tableOfRestaurant)
        {
            _reservationService = reservationService;
            _customerService = customerService;
            _mapper = mapper;
          
            _tableOfRestaurant = tableOfRestaurant;
        }

       

        [HttpPost]
       
        public IActionResult PostReservation(ReservationCustomerDTO reservationCustomerDTO)
        {
        

            var reservationEntity = _mapper.Map<Reservation>(reservationCustomerDTO.Reservation);
            var customerEntity = _mapper.Map<Customer>(reservationCustomerDTO.Customer);

            _customerService.Create(customerEntity);
            reservationEntity.CustomerId = customerEntity.Id;
            _reservationService.Create(reservationEntity);

            MailSender.SendEmail(reservationCustomerDTO.Customer.Email, @"Rezervasyon Bilgisi", $"Sayın {reservationCustomerDTO.Customer.Name} {reservationCustomerDTO.Customer.Surname}, rezervasyonunuz başarıyla oluşturulmuştur." +
                                                 $" \nRezervasyon Tarihi : {reservationCustomerDTO.Reservation.ReservationDate}  \nNot: {reservationCustomerDTO.Reservation.Description}");

          
            return Ok();



        }
        [HttpGet]
        public IActionResult GetReservationDate(DateTime date)
        {

            var reservationList = _reservationService.GetAll().Where(x => x.ReservationDate.DayOfYear == date.DayOfYear).ToList();

            return Ok(reservationList);
        }
    }
}
