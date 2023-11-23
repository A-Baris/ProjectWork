using Microsoft.AspNetCore.Mvc;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    public class AreaBaseController : Controller
    {
        //Giriş yapan kullanıcının rolleri sayfa için yetkili olup olmadığı kontrol edilip true veya false dönerek kullanıcıyı yönlendirme yapmamıza yardımcı olacak
        protected bool  CheckAuthorization(string[] roles)
        {
            var user = HttpContext.User;

            foreach (var role in roles)
            {
                if (user.IsInRole(role))
                {
                    return true; 
                }
            }

            
            return false;
        }
    }
}
