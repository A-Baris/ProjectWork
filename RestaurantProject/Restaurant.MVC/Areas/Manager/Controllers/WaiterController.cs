using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class WaiterController : Controller
    {
        private readonly IWaiterService _waiterService;

        public WaiterController(IWaiterService waiterService)
        {
            _waiterService = waiterService;
        }
        public IActionResult Index()
        {
            ViewBag.waiterList = _waiterService.GetAll();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(WaiterVM waiterWM)
        {
            if (ModelState.IsValid)
            {
                Waiter waiter = new Waiter()
                {
                    Name = waiterWM.Name,
                    Surname = waiterWM.Surname,
                    Phone = waiterWM.Phone,
                    TcNo = waiterWM.TcNo
                };
                _waiterService.Create(waiter);
                return RedirectToAction("Index", "Manager");

            }
            return View(waiterWM);
        }
        public async Task<IActionResult> Update(int id)
        {
            var waiter= await _waiterService.GetbyIdAsync(id);
            var updated = new WaiterVM
            {
                Id = id,
                Name = waiter.Name,
                Surname = waiter.Surname,
                Phone = waiter.Phone,
                TcNo = waiter.TcNo
            };
            return View(updated);
        }
        [HttpPost]
        public async  Task<IActionResult> Update(WaiterVM updated)
        {
            if (ModelState.IsValid)
            {
                var waiterUpdate = await _waiterService.GetbyIdAsync(updated.Id);
                waiterUpdate.Name = updated.Name;
                waiterUpdate.Surname = updated.Surname;
                waiterUpdate.Phone = updated.Phone;
                waiterUpdate.TcNo = updated.TcNo;
                  
                _waiterService.Update(waiterUpdate);
                TempData["UpdateMessage"] = "Updated is achieved";
                return RedirectToAction("Index","Manager");

            }
            else
            {
                return View(updated);
            }
            
        }

        public async Task<IActionResult> Remove(int id)
        {
            var waiter = await _waiterService.GetbyIdAsync(id);
            if(waiter != null)
            {
                waiter.BaseStatus=Entity.Enums.BaseStatus.Deleted;
                _waiterService.Update(waiter);
                return RedirectToAction("Index", "Manager");

            }
            return View();
        }


        
    }
}
