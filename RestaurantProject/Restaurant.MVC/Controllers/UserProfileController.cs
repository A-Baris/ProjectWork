﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;

using Restaurant.Entity.Entities;
using Restaurant.MVC.Models;
using Restaurant.MVC.Models.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;
using System.Security.Claims;

namespace Restaurant.MVC.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ProjectContext _context;
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IValidationService<SecurityProfileVM> _validationService;
        private readonly IValidationService<ProfileVM> _validationProfileVM;
        private readonly PasswordHasher<AppUser> _passwordHasher;

        public UserProfileController(UserManager<AppUser> userManager, ProjectContext context, IReservationService reservationService, ICustomerService customerService, SignInManager<AppUser> signInManager, IValidationService<SecurityProfileVM> validationService, IValidationService<ProfileVM> validationProfileVM)
        {
            _userManager = userManager;

            _context = context;
            _reservationService = reservationService;
            _customerService = customerService;
            _signInManager = signInManager;
            _validationService = validationService;
            _validationProfileVM = validationProfileVM;
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
                //var data = _mapper.Map<ProfileVM>(user);
                var data = new ProfileVM()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    CustomerName = user.CustomerName,
                    CustomerSurname = user.CustomerSurname,


                };
                return View(data);
            }
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileVM profileVM)
        {

            ModelState.Clear();
            var errors = _validationProfileVM.GetValidationErrors(profileVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View("profiledetail", profileVM);
            }
            string UserName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);

            if (user != null)
            {
                var customer = _customerService.GetAll().Where(x => x.Email == user.Email).FirstOrDefault();
                if (customer == null)
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
                else
                {
                    customer.Name = profileVM.CustomerName;
                    customer.Surname = profileVM.CustomerSurname;
                    customer.Phone = profileVM.PhoneNumber;
                    customer.Email = profileVM.Email;
                    _customerService.Update(customer);

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
                if (result.Succeeded)
                {

                    HttpContext.Session.Remove("UserName");
                    HttpContext.Session.SetString("UserName", profileVM.UserName);
                    await _signInManager.RefreshSignInAsync(user);
                    TempData["Message"] = "Bilgileriniz başarılı şekilde güncellendi";
                    return RedirectToAction("profiledetail", "UserProfile");
                }
                else
                {
                    TempData["Message"] = "Bilgileriniz güncellerken hata oluştu. Lütfen tekrar deneyiniz";
                    return RedirectToAction("profiledetail", "UserProfile");

                }




            }
            else
            {
                TempData["Message"] = "Kullanıcı bilgilerine ulaşılamadı. Lütfen tekrar deneyiniz";
                return View("profiledetail");

            }

        }

        public async Task<IActionResult> SecurityProfile()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SecurityProfile(SecurityProfileVM securityVM)
        {
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(securityVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(securityVM);

            }


            string UserName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);
            var passwordHasher = new PasswordHasher<AppUser>();


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
                else
                {
                    TempData["ErrorMessage"] = "Şifreniz değiştirilemedi.Lütfen tekrar deneyin.";
                    return View(securityVM);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Güncel Şifreniz hatalı";
                return View(securityVM);
            }




        }
        public async Task<IActionResult> MyReservation()
        {
            string UserName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);

            var reservationQuery = from r in _context.Reservations
                                   join c in _context.Customers on r.CustomerId equals c.Id
                                   select new CustomerReservationVM()
                                   {
                                       Id = r.Id,
                                       ReservationDate = r.ReservationDate,
                                       GuestNumber = r.GuestNumber,
                                       Description = r.Description,
                                       ReservationStatus = r.ReservationStatus,
                                       Name = c.Name,
                                       Surname = c.Surname,
                                       Email = c.Email,
                                   };
            var reservation = reservationQuery.Where(x => x.Email == user.Email && x.ReservationStatus == Entity.Enums.ReservationStatus.Active).ToList();

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
                return RedirectToAction("myreservation", "userprofile");
            }
            return View("myreservation");

        }

    }
}
