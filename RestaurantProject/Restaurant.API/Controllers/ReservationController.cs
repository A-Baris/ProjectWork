using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.AbstractServices;
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
        private readonly ProjectContext _context;

        public ReservationController(IReservationService reservationService,ICustomerService customerService,IMapper mapper,ProjectContext context)
        {
            _reservationService = reservationService;
            _customerService = customerService;
            _mapper = mapper;
           _context = context;
        }

        [HttpPost]
        public IActionResult PostReservation(ReservationDTO reservation,CustomerDTO customer)
        {
            var reservationEntity = _mapper.Map<Reservation>(reservation);
            var customerEntity = _mapper.Map<Customer>(customer);

            _customerService.Create(customerEntity);
            reservationEntity.CustomerId = customerEntity.Id;
            _reservationService.Create(reservationEntity);

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
