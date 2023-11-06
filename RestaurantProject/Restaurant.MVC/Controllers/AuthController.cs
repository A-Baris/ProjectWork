using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.MVC.Data;
using Restaurant.MVC.Models.ViewModels;
using System.Security.Claims;

namespace Restaurant.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Register()
        {
            var externalLogins = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(externalLogins.ToList());
         
        }

        [HttpPost]
        public IActionResult GoogleLogin(string provider)
        {
            var redirectUrl = Url.Action("GoogleLoginCallback", "auth");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> GoogleLoginCallback()
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                return RedirectToAction("Login","home");
            }
            var email = externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value;
           
            
            if (email == null)
            {
                return RedirectToAction("Login","home");
            }
            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                //
                return RedirectToAction("GoogleRegister", new { email });

            }
            else
            {
                return RedirectToAction("Login","Home");    
            }
        
        }
        public IActionResult GoogleRegister(string email)
        {
            GoogleRegisterVM vM = new GoogleRegisterVM();
            vM.Email = email;
         
            return View(vM);
        }
        
        [HttpPost]
        public async Task<IActionResult> GoogleRegister(GoogleRegisterVM registerVM)
        {
            //var user = new AppUser { Email = registerVM.Email };

            //if (user.UserName == null)
            //{
            //    user.UserName = user.Email;
            //}
            //else
            //{
            //    user.UserName = registerVM.UserName;
            //}
           var user = new AppUser { Email = registerVM.Email};
            var result = await _signInManager.UserManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
