using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Data;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{ // USer için güncelleme ve remove action eklenecek unutulmamalı
    [Area("Manager")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public UserController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
           _passwordHasher = passwordHasher;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<UserVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userVM = new UserVM
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                };

                userList.Add(userVM);
            }

            return View(userList);
            //var UserList = await _userManager.Users.Select(x => new UserVM
            //{
            //    Id = x.Id,
            //    UserName = x.UserName,
            //    Email = x.Email,              


            //}).ToListAsync();

            //return View(UserList);
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
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var updatedUser = new UserUpdateVM
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                
            };
            return View(updatedUser);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateVM updateVM)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(updateVM.Id);
                if(user != null)
                {
                    user.UserName = updateVM.UserName;
                    user.Email = updateVM.Email;
                    user.PhoneNumber = updateVM.PhoneNumber;

                    if (!string.IsNullOrEmpty(updateVM.Password))
                    {
                        var newPasswordHash = _passwordHasher.HashPassword(user, updateVM.Password);
                        user.PasswordHash = newPasswordHash;
                    }
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "user", new { area = "Manager" });
                }
            }
            return View(updateVM);
        }
        public async Task<IActionResult> Remove(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user!=null)
            {
                await _userManager.DeleteAsync(user);
                return RedirectToAction("Index", "user", new { area = "Manager" });
            }
            return View();
        }


    }
}
