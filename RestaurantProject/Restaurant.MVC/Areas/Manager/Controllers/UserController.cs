using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Data;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{ // USer için güncelleme ve remove action eklenecek
    [Area("Manager")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var UserList = _userManager.Users.Select(x => new UserVM
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
            }).ToList();
            return View(UserList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateVM userVM)
        {
            if(ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                    PhoneNumber = userVM.PhoneNumber,

                };
                var result = await _userManager.CreateAsync(appUser, userVM.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "user",new {area="Manager"});   
                }
            }
            return View();
        }


    }
}
