using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using System.Security.Claims;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {

        [Authorize(Roles ="employee")] // her görevli bir çalışandır böylelikle çalışan dışındakiler sayfaya ulaşamazlar
        public IActionResult Index()
        {
            //            select p.ProductName,Count(o.ProductId) as 'Toplam Sipariş' from orders o
            //join Products p on o.ProductId = p.Id
            //group by p.ProductName
            //En çok sipariş edilen ürünleri listelemeliyim
            return View();

           
        }
        public IActionResult CheckAuth()
        {
            // Checkauth action siteye giriş yapan kullanıcı durumunu kontrol etmek için köprü görevi görür. Eğer kullanıcı herhangi bir rolü yoksa default index e yönlendirilir.
            // Eğer rolü varsa yönetim panelindeki index e yönlendirilir.

            if (User.Identity.IsAuthenticated)
            {

                if (User.Claims.Any(c => c.Type == ClaimTypes.Role))
                {

                    return RedirectToAction("Index", "home", new { area = "manager" });
                }
                else
                {
                    return RedirectToAction("Index", "home", new { area = "" });

                }
            }
            return RedirectToAction("Index", "home", new { area = "" });
        }

    }
}
