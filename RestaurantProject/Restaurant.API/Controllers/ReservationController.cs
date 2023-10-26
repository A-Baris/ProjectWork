using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.DTOs;
using Restaurant.Entity.Entities;

namespace Restaurant.API.Controllers
{
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService,ICustomerService customerService,IMapper mapper)
        {
            _reservationService = reservationService;
            _customerService = customerService;
            _mapper = mapper;
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
    }
}
