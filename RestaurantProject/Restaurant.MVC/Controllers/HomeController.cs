using Restaurant.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.MVC.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

using Restaurant.MVC.Models.ViewModels;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using Restaurant.Common;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using Restaurant.BLL.Services;
using Restaurant.MVC.Validators;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using System.Net;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICustomerService _customerService;
        private readonly IValidationService<RegisterVM> _validationServiceForRegisterVM;
        private readonly IValidationService<ResetPasswordVM> _validationServiceForResetPasswordVM;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ICustomerService customerService, IValidationService<RegisterVM> validationServiceForRegisterVM,IValidationService<ResetPasswordVM> validationServiceForResetPasswordVM)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
            _validationServiceForRegisterVM = validationServiceForRegisterVM;
            _validationServiceForResetPasswordVM = validationServiceForResetPasswordVM;
        }


        public IActionResult Index()
        {
            

            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            ModelState.Clear();
            var errors = _validationServiceForRegisterVM.GetValidationErrors(registerVM);
            if (errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(registerVM);
            }


            var mail = await _userManager.FindByEmailAsync(registerVM.Email);

            if (mail != null)
            {
                TempData["ErrorMessage"] = "Email kullanılmaktadır";
                return View(registerVM);
            }
            else
            {
                var username = await _userManager.FindByNameAsync(registerVM.UserName);
                if (username != null)
                {
                    TempData["ErrorMessage"] = "Kullanıcı Adı kullanılmaktadır";
                    return View(registerVM);
                }
            }
            AppUser user = new AppUser()
            {
                CustomerName = registerVM.CustomerName,
                CustomerSurname = registerVM.CustomerSurname,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                UserRight = registerVM.UserRight,
            };



            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {

                Customer customer = new Customer()
                {
                    Name = registerVM.CustomerName,
                    Surname = registerVM.CustomerSurname,
                    Email = registerVM.Email,
                    Phone = registerVM.PhoneNumber,

                };
                _customerService.Create(customer);
                try
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodeToken = HttpUtility.UrlEncode(token);
                    string confirmationLink = Url.Action("Confirmation", "Home", new { id = user.Id, token = encodeToken }, Request.Scheme);

                    MailSender.SendEmail(registerVM.Email, "Üyelik AKtivasyon", $"Kayıt işlemi başarılı.\n Aramıza Hoş Geldin {registerVM.UserName} \n {confirmationLink} ");
                    TempData["Message"] = "Tebrikler üyelik başarılı şekilde oluşturuldu \n  Bilgilerinizle giriş yapabilirsiniz";
                    return RedirectToAction("login", "home");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("login", "home");
                }
            }
            else
            {
                TempData["ErrrorMessage"] = "Kullanıcı oluşturulurken bir hatayla karşılaşıldı.\nLütfen Tekrar Deneyiniz.";
                return View(registerVM);
            }



        }
        public async Task<IActionResult> Confirmation(string id, string token)
        {
            if (id == null || token == null)
            {
                TempData["ErrorMessage"] = "Geçersiz erişim isteğiyle karşılaşıldı";
                return RedirectToAction("index", "Home");

            }
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Geçersiz erişim isteğiyle karşılaşıldı";
                return RedirectToAction("index", "Home");
            }

            var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(token));

            if (result.Succeeded)
            {
                
                TempData["Message"] = "Üyelik aktivasyonu başarıyla tamamlandı. Artık giriş yapabilirsiniz.";
                return RedirectToAction("Login", "Home"); 
            }
            else
            {
              
                TempData["ErrorMessage"] = "Üyelik aktivasyonu sırasında bir hata oluştu. Lütfen tekrar deneyiniz.";
                return RedirectToAction("index", "Home");
            }
        }



        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {

                AppUser user = await _userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {

                        HttpContext.Session.SetString("Email", user.Email);
                        HttpContext.Session.SetString("Id", user.Id);
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetString("Phone", user.PhoneNumber);
                        HttpContext.Session.SetString("Password", user.PasswordHash);
                        return RedirectToAction("checkauth", "home", new { area = "manager" });

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Email veya Şifre yanlış ";
                    }

                }
                TempData["ErrorMessage"] = "Email veya Şifre yanlış ";

            }
            return View(loginVM);
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GoogleLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("Response", "Home", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }
        public async Task<IActionResult> Response(string ReturnUrl = "/")
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                if (result.Succeeded)
                {
                    TempData["Message"] = $"Hoş Geldin {info.Principal.Identity.Name}";

                    return RedirectToAction("index", "home");
                }
                else
                {
                    AppUser user = new AppUser();
                    user.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    string ExternalUserId = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name))
                    {
                        string userName = info.Principal.FindFirst(ClaimTypes.Name).Value;
                        userName = userName.Replace(' ', '-').ToLower() + ExternalUserId.Substring(0, 5);
                        user.UserName = userName;
                    }
                    else
                    {
                        user.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    }

                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    if (createResult.Succeeded)
                    {
                        // Farklı kayıt türlerini kullanan müşterilerinin ad,soyad,email bilgilerini kendi customer tablomuza kaydederek rezervasyonda yaşancak olası hataların önüne geçiyoruz.
                        //Müşteriler profil sayfasında kişisel bilgilerini güncelleyebilecekler
                        Customer customer = new Customer() { Name = user.UserName, Surname = "Surname", Email = user.Email };
                        _customerService.Create(customer);
                        IdentityResult loginResult = await _userManager.AddLoginAsync(user, info);
                        if (loginResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, true);

                            HttpContext.Session.SetString("UserName", user.UserName);

                            TempData["Message"] = $"Hoş Geldin {user.UserName}";
                            return RedirectToAction("index", "home");
                        }
                    }
                }
            }
            return View("Login");
        }
        public IActionResult ForgottenPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgottenPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null )
            {
                TempData["ErrorMessage"] = $"{email} adresi ile kayıtlı kullanıcı bulunamadı";
                return RedirectToAction("forgottenpassword", "Home");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);

            string resetLink = Url.Action("ResetPassword", "Home", new { userId = user.Id, token = encodedToken }, Request.Scheme);

            MailSender.SendEmail(email, "Şifremi Unuttum", $"Şifre değiştirmek için linke tıklayınız:\n {resetLink}");

            TempData["Message"] = $"{email} adresine şifre yenileme linki başarıyla gönderilmiştir";
            return RedirectToAction("forgottenpassword", "home");

        }
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            
            var resetVM = new ResetPasswordVM
            {
                UserId = userId,
                Token = token
            };

            return View(resetVM);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetVM)
        {

            var user = await _userManager.FindByIdAsync(resetVM.UserId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Hatalı veya eksik erişim isteğiyle karşılaşıldı";
                return RedirectToAction("index", "Home");
            }
            ModelState.Clear();
            var errors = _validationServiceForResetPasswordVM.GetValidationErrors(resetVM);
            if(errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                return View(resetVM);
            }           
        
            var decodedToken = WebUtility.UrlDecode(resetVM.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetVM.Password);

            if (result.Succeeded)
            {
                TempData["Message"] = "Şifre yenileme başarıyla gerçekleşmiştir.\n Bilgilerinizle tekrar giriş yapabilirsiniz";
                return RedirectToAction("login", "Home");
            }
            else
            {

                TempData["ErrorMessage"] = "Beklenmedik bir hatayla karşılaşıldı lütfen tekrar deneyiniz veya yeniden şifre yenileme isteği gerçekleştiriniz";
                return View(resetVM);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
