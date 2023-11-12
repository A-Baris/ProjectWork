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
using Restaurant.DAL.Data;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICustomerService _customerService;

        public HomeController(ILogger<HomeController> logger,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ICustomerService customerService)
        {
            _logger = logger;
          _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
        }

        
        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{

            //    if (User.Claims.Any(c => c.Type == ClaimTypes.Role))
            //    {
                   
            //        return RedirectToAction("Index", "home", new { area = "manager" });
            //    }
            //    else
            //    {
                 
            //        return RedirectToAction("Index", "home");
            //    }
            //}

            return View();
        }
        [Authorize]
        public async Task<IActionResult> Reservation()
        {
            string UserName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);
            ViewBag.UserEmail = user.Email;
            return View();
        }
   

        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var mail = await _userManager.FindByEmailAsync(registerVM.Email);

            if (mail != null)
            {
                TempData["ErrorMessage"] = "Mail is already existing";
                return View(registerVM);
            }
            else
            {
                var username = await _userManager.FindByNameAsync(registerVM.UserName);
                if (username != null)
                {
                    TempData["ErrorMessage"] = "User is already existing";
                    return View(registerVM);
                }
            }

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    CustomerName=registerVM.CustomerName,
                    CustomerSurname=registerVM.CustomerSurname,
                    UserName = registerVM.UserName,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                


                var result = await _userManager.CreateAsync(user, registerVM.Password);
                if(result.Succeeded)
                {

                    Customer customer = new Customer()
                    {
                        Name = registerVM.CustomerName,
                        Surname = registerVM.CustomerSurname,
                        Email = registerVM.Email,
                        Phone = registerVM.PhoneNumber,
                       
                    };
                    _customerService.Create(customer);

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var encodeToken = HttpUtility.UrlEncode(token.ToString());
                        string confirmationLink = Url.Action("Confirmation", "Home", new { id = user.Id, token = encodeToken }, Request.Scheme);

                        MailSender.SendEmail(registerVM.Email, "Üyelik AKtivasyon", $"Kayıt işlemi başarılı.\n Aramıza Hoş Geldin {registerVM.UserName} \n {confirmationLink} ");
                    TempData["Message"] = "Tebrikler üyelik başarılı şekilde oluşturuldu \n  Bilgilerinizle giriş yapabilirsiniz";
                        return RedirectToAction("login", "home");
                  
                }
               
            }
            return View(registerVM);
        }

       
       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(ModelState.IsValid)
            {

                AppUser user = await _userManager.FindByEmailAsync(loginVM.Email);
                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("Email",user.Email);
                        HttpContext.Session.SetString("Id",user.Id);
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetString("Phone", user.PhoneNumber);
                        HttpContext.Session.SetString("Password", user.PasswordHash);
                        return RedirectToAction("checkauth", "home", new {area="manager"});

                    }
                  
                }
             
            }
            return View(loginVM);
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public IActionResult GoogleLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("Response", "Home", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }
        public async Task<IActionResult> Response(string ReturnUrl="/")
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if(info==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                if (result.Succeeded)
                {
                    TempData["Message"] = $"Hoş Geldin {info.Principal.Identity.Name}";
                    return RedirectToAction("index","home");
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
                        IdentityResult loginResult = await _userManager.AddLoginAsync(user, info);
                        if(loginResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, true);
                            HttpContext.Session.SetString("UserName", user.UserName);
                            TempData["Message"] = $"Hoş Geldin {user.UserName}";
                            return RedirectToAction("index","home");
                        }
                    }
                    }
            }
            return View("Login");
        }
     
        public IActionResult Privacy()
        {
          

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
