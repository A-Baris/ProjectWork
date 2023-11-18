using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.DAL.Data;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Models.ViewModels;
using System.Security.Claims;

namespace Restaurant.MVC.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ProjectContext _context;
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly PasswordHasher<AppUser> _passwordHasher;

        public UserProfileController(UserManager<AppUser> userManager,IMapper mapper,ProjectContext context,IReservationService reservationService,ICustomerService customerService,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _reservationService = reservationService;
            _customerService = customerService;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ProfileDetail()
        {
            //var userNewName = HttpContext.Session.GetString("UserName");
            //var userName = userNewName;
            //string UserName = User.Identity.Name;
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                var data = _mapper.Map<ProfileVM>(user);
                return View(data);
            }
            return View();

        }
    
      
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileVM profileVM)
        {
            string UserName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);
            if (ModelState.IsValid)
            {

             
                if (user != null)
                {
                    var customer = _customerService.GetAll().Where(x => x.Email == user.Email);
                    if(customer==null)
                    {
                        Customer newCustomer = new Customer()
                        {
                            Name = profileVM.CustomerName,
                            Surname = profileVM.CustomerSurname,
                            Phone = profileVM.PhoneNumber,
                            Email = profileVM.Email,
                        };
                        _customerService.Create(newCustomer);
                    }
                    user.Email = profileVM.Email;
                    user.PhoneNumber = profileVM.PhoneNumber; 
                    user.UserName = profileVM.UserName;
                    user.CustomerName = profileVM.CustomerName;
                    user.CustomerSurname = profileVM.CustomerSurname;
                    //if (profileVM.PasswordHash != null || profileVM.PasswordConfirmed != null)
                    //{
                    //    await _userManager.RemovePasswordAsync(user);
                    //    await _userManager.AddPasswordAsync(user, profileVM.PasswordHash);
                    //}
                    var result = await _userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {

                        HttpContext.Session.Remove("UserName");
                        HttpContext.Session.SetString("UserName", profileVM.UserName);
                        await _signInManager.RefreshSignInAsync(user);
                        TempData["Message"] = "Bilgileriniz başarılı şekilde güncellendi";
                        return RedirectToAction("index", "UserProfile");
                    }
                 
                }
            }
            return View("profiledetail", "userprofile");
        }

        public async Task<IActionResult> SecurityProfile()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SecurityProfile(SecurityProfileVM securityVM)
        {
            string UserName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);
            var passwordHasher = new PasswordHasher<AppUser>();

            if (ModelState.IsValid)
            {
              //passwordhasher yardımıyla göndermiş olduğumuz PasswordNow değeri veritabanındaki güncel parolayla uyuşma sonucuna göre değer dönüyor.
              //Böylelikle başarılı sonuçla beraber şifre değiştirme işlemine devam edebiliyoruz.
             
                var resultPassword = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, securityVM.PasswordNow);
                if (resultPassword == PasswordVerificationResult.Success)
                {
                    
                
                await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, securityVM.PasswordHash);
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["Message"] = "Şifreniz başarılı şekilde değiştirildi";
                        return RedirectToAction("index", "UserProfile");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Güncel Şifreniz hatalı";
                    return View();
                }

            }
            TempData["ErrorMessage"] = "Eksik veya hatalı değerler var";
            return View();
        }
        public async Task<IActionResult> MyReservation()
        {
            string UserName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);

            var reservationQuery = from r in _context.Reservations
                                   join c in _context.Customers on r.CustomerId equals c.Id
                                   select new CustomerReservationVM()
                                   {
                                       Id=r.Id,
                                       ReservationDate = r.ReservationDate,
                                       GuestNumber = r.GuestNumber,
                                       Description = r.Description,
                                       ReservationStatus = r.ReservationStatus,
                                       Name = c.Name,
                                       Surname = c.Surname,
                                       Email = c.Email,
                                   };
            var reservation = reservationQuery.Where(x=>x.Email == user.Email && x.ReservationStatus == Entity.Enums.ReservationStatus.Active).ToList();

            //{ r.ReservationDate, r.Description, r.ReservationStatus, c.Name, c.Surname,c.Email };
            return View(reservation);
        }

        public async Task<IActionResult> CancelReservation(int id)
        {
            var entity = await _reservationService.GetbyIdAsync(id);
            if (entity != null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                entity.ReservationStatus = Entity.Enums.ReservationStatus.Passive;
                _reservationService.Update(entity);
                TempData["Message"] = "Rezervasyon iptal edildi";
                return RedirectToAction("myreservation","userprofile");
            }
            return View("myreservation");
         
        }

    }
}
