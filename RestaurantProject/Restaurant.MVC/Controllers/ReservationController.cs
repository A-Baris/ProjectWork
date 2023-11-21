using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Data;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(IReservationService reservationService, ITableOfRestaurantService tableOfRestaurantService,UserManager<AppUser> userManager)
        {
            _reservationService = reservationService;
            _tableOfRestaurantService = tableOfRestaurantService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> UserReservation()
        {
            string UserName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);
            ViewBag.UserEmail = user.Email;
            
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UserReservationUpdate(int id)
        {

       
            string apiUrl = "https://localhost:7219/api/reservation/UpdateReservation?id=" + id;

           
            string jsonData;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    jsonData = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    
                    return View("Error");
                }
            }

            ReservationModel reservation = JsonConvert.DeserializeObject<ReservationModel>(jsonData);

           

            return View(reservation);
        }
   

        //public IActionResult ReservationDateCheck(DateTime checkDate)
        //{
        //   var tableCounts = _tableOfRestaurantService.GetAll().Count();
        //   var reservarionCountsInDate = _reservationService.GetAll().Where(x=>x.ReservationDate.DayOfYear == checkDate.DayOfYear).Count();
        //    if(reservarionCountsInDate<tableCounts)
        //    {
        //        return View("UserReservation");
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = $"{checkDate} tarihindeki rezervasyonlar doludur!";
        //        return RedirectToAction("UserReservation", "Reservation");
        //    }

        //}
    }
}
