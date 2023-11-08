using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DAL.Data;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<AppUser> _passwordHasher;

        public UserProfileController(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ProfileDetail()
        {
            var userNewName = HttpContext.Session.GetString("UserName");
            var userName = userNewName;
            var user = await _userManager.FindByNameAsync(userName);
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
                    user.Email = profileVM.Email;
                    user.PhoneNumber = profileVM.PhoneNumber; 
                    user.UserName = profileVM.UserName;
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
        public async Task<IActionResult> SecurityProfile(ProfileSecurityVM securityVM)
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

    }
}
