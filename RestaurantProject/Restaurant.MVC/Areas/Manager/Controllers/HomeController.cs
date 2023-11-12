using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using System.Security.Claims;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {

 
        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult CheckAuth()  
        {
            // Checkauth action siteye giriş yapan kullanıcı durumunu kontrol etmek için köprü görevi görür. Eğer kullanıcı herhangi bir rolü yoksa default index e yönlendirilier.
            // Eğer rolü varsa yönetim panelindeki index e yönlendirilir.

            if (User.Identity.IsAuthenticated)
            {

                if (User.Claims.Any(c => c.Type == ClaimTypes.Role))
                {

                    return RedirectToAction("Index", "home", new { area = "manager" });
                }
                else
                {
                    return RedirectToAction("Index", "home",new { area = "" });

                }
            }
            return RedirectToAction("Index", "home", new { area = "" });
        }
        
    }
}
