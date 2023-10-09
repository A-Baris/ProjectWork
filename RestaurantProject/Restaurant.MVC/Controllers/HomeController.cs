using Restaurant.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.MVC.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Restaurant.MVC.Data;
using Restaurant.MVC.Models.ViewModels;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using Restaurant.Common;
using System.Web;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
       

        public HomeController(ILogger<HomeController> logger,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _logger = logger;
          _userManager = userManager;
            _signInManager = signInManager;
          
        }

        public IActionResult Index()
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
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = registerVM.UserName,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                var result = await _userManager.CreateAsync(user, registerVM.Password);
                if(result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodeToken = HttpUtility.UrlEncode(token.ToString());
                    string confirmationLink = Url.Action("Confirmation", "Home", new {id=user.Id,token=encodeToken},Request.Scheme);
                    
                    MailSender.SendEmail(registerVM.Email, "Üyelik AKtivasyon", $"Kayıt işlemi başarılı.\n Aramıza Hoş Geldin {registerVM.UserName} \n {confirmationLink} ");
                    return RedirectToAction("index", "home");
                }
            }
            return View(registerVM);
        }
        public async  Task<IActionResult> Confirmation(string? id,string? token)
        {
            var user =await _userManager.FindByIdAsync(id);
            if(user!=null)
            {
                var decodetoken = HttpUtility.UrlDecode(token);
                var result = await _userManager.ConfirmEmailAsync(user, decodetoken);
                if(result.Succeeded)
                {
                    return View("Index", "Home");
                }
            }
            return View("Index","Home");
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
                        return RedirectToAction("Index","Home");
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
