using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.Common;
using Restaurant.DAL.Context;

using Restaurant.Entity.DTOs;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Validators;

namespace Restaurant.API.Controllers
{
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        private readonly ITableOfRestaurantService _tableOfRestaurant;
        private readonly ProjectContext _context;
       

        public ReservationController(IReservationService reservationService, ICustomerService customerService, IMapper mapper, ITableOfRestaurantService tableOfRestaurant, ProjectContext context)
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
            if(reservationCustomerDTO.Reservation.ReservationDate == null || reservationCustomerDTO.Reservation.ReservationDate.DayOfYear<=DateTime.Today.AddDays(1).DayOfYear)
            {
                return StatusCode(400);
            }
            try
            {

                var reservationEntity = _mapper.Map<Reservation>(reservationCustomerDTO.Reservation);
                var customer = _customerService.GetAll().Where(x => x.Email == reservationCustomerDTO.Customer.Email).FirstOrDefault();
                if (customer.Adress == null || customer.Phone == null)
                {
                    customer.Adress = reservationCustomerDTO.Customer.Adress;
                    customer.Phone = reservationCustomerDTO.Customer.Phone;
                    _customerService.Update(customer);
                }
                var tableCounts = _tableOfRestaurant.GetAll().Count();
                var reservationCounts = _reservationService.GetAll().Where(x => x.ReservationDate.DayOfYear == reservationCustomerDTO.Reservation.ReservationDate.DayOfYear).Count();

                if (reservationCounts < tableCounts)
                {
                    reservationEntity.CustomerId = customer.Id;
                    _reservationService.Create(reservationEntity);
                    try
                    {


                        MailSender.SendEmail(customer.Email, @"Rezervasyon Bilgisi", $"Sayın {customer.Name} {customer.Surname}, rezervasyonunuz başarıyla oluşturulmuştur." +
                                                             $" \nRezervasyon Tarihi : {reservationCustomerDTO.Reservation.ReservationDate} \nMisafir Sayısı:{reservationCustomerDTO.Reservation.GuestNumber}  \nNot: {reservationCustomerDTO.Reservation.Description}");

                    }
                    catch
                    {
                        return Ok();
                    }
                    return Ok();
                }
                else
                {

                    return StatusCode(409); //409 kodu conflict
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Sunucu Kaynaklı Hata");
            }
        }


        [HttpGet]
        public async Task<IActionResult> UpdateReservation(int id)
        {
            var reservationQuery = from r in _context.Reservations
                              join c in _context.Customers on r.CustomerId equals c.Id
                              where r.Id == id
                              select new
                              {
                                  r.Id,
                                  r.ReservationDate,
                                  r.Description,
                                  r.GuestNumber,
                                  c.Adress,
                                  c.Phone,
                                  c.Email,

                              };
            var reservation = reservationQuery.FirstOrDefault();
                              
            if (reservation != null)
            {
                var timeSinceReservation =reservation.ReservationDate - DateTime.UtcNow ;
                if (timeSinceReservation.TotalHours < 24)
                {
                    return BadRequest();
                }

                return Ok(reservation);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut]
     
        public async Task<IActionResult> UpdateReservation(ReservationCustomerUpdateDTO rcDTO)
        {

            try
            {
                if (rcDTO.ReservationUpdateDTO.ReservationDate == null || rcDTO.ReservationUpdateDTO.ReservationDate.DayOfYear <= DateTime.Today.AddDays(1).DayOfYear)
                {
                    return StatusCode(400);
                }

                var reservation = await _reservationService.GetbyIdAsync(rcDTO.ReservationUpdateDTO.Id);
                if (reservation != null)
                {
                   
                    if (rcDTO.CustomerDTO.Email != null)
                    {
                        var customer = _customerService.GetAll().Where(x => x.Email == rcDTO.CustomerDTO.Email).FirstOrDefault();
                        var customerUpdate = _mapper.Map(rcDTO.CustomerDTO, customer);
                        _customerService.Update(customerUpdate);
                    }
                    var tableCounts = _tableOfRestaurant.GetAll().Count();
                    var reservationCounts = _reservationService.GetAll().Where(x => x.ReservationDate.DayOfYear == rcDTO.ReservationUpdateDTO.ReservationDate.DayOfYear).Count();

                    if (reservationCounts < tableCounts)
                    {
                        var reservationUpdate = _mapper.Map(rcDTO.ReservationUpdateDTO, reservation);
                        _reservationService.Update(reservationUpdate);

                        return Ok();
                    }
                    else
                    {

                        return StatusCode(409);
                    }

                 
                }


                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult GetReservationDate(DateTime date)
        {
            try
            {


                var reservationQuery = (from r in _context.Reservations
                                        join c in _context.Customers on r.CustomerId equals c.Id
                                        select new
                                        {
                                            r.ReservationDate,
                                            r.ReservationStatus,
                                            c.Name,
                                            c.Surname,
                                            r.GuestNumber,
                                            r.Description
                                        }).ToList();
                var reservationList = reservationQuery.Where(x => x.ReservationDate.DayOfYear == date.DayOfYear).ToList();


                return Ok(reservationList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }

        [HttpGet]
        public IActionResult GetReservationDates(DateTime date)
        {
            try
            {

                var query = from r in _context.Reservations
                            join c in _context.Customers on r.CustomerId equals c.Id
                            select new
                            {
                                r.ReservationDate,
                                r.Description,
                                r.GuestNumber,
                                r.ReservationStatus,
                                c.Name,
                                c.Surname
                            };
                var reservationList = query.Where(x => x.ReservationDate.DayOfYear == date.DayOfYear).ToList();

                return Ok(reservationList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
