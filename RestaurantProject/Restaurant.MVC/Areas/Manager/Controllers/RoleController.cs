using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Data;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
           _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {

           var RoleList= _roleManager.Roles.Select(x => new RoleVM
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return View(RoleList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new AppRole()
                {
                    Name = roleVM.Name,
                });
                return RedirectToAction("Index","User", new { area = "manager" });
            }
            return View(roleVM);
        }
        public async Task<IActionResult> Update(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role!=null)
            {
                var roleUpdated = new RoleVM()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return View(roleUpdated);
            }
               
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoleVM roleVM)
        {
            if(ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(roleVM.Id);
                if(role!=null)
                {
                    role.Id=roleVM.Id;
                    role.Name=roleVM.Name;
                    var updatedEntity = await _roleManager.UpdateAsync(role);
                    return RedirectToAction("Index", "Role", new { area = "manager" });
                }
               
            }
            return View(roleVM);
        }
        public async Task<IActionResult> Remove(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
             if(role!=null)
            {
                _roleManager.DeleteAsync(role);
                return RedirectToAction("Index", "Role", new { area = "manager" });

            }
            return View(id);
        }
        public async Task<IActionResult> AssignRole(string id)
        {
            var selectedUser = await _userManager.FindByIdAsync(id);
            ViewBag.UserId = id;
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(selectedUser);
            var roleViewModel = new List<RoleAssignVM>();
          
            

            foreach (var role in roles)
            {
                var assignRoleVM = new RoleAssignVM()
                {
                    Id = role.Id,
                    Name = role.Name
                };

                if(userRoles.Contains(role.Name))
                {
                    assignRoleVM.Exist = true;
                }
                roleViewModel.Add(assignRoleVM);
            }
            return View(roleViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> AssignRole(string userId, List<RoleAssignVM> requestList)
        {
            var userToRole = await _userManager.FindByIdAsync(userId);

            if (userToRole == null)
            {
                
                return RedirectToAction("index", "role", new { area = "Manager" });
            }

            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToRole, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToRole, role.Name);
                }
            }

            return RedirectToAction("index", "user", new { area = "Manager" });
        }


    }
}
