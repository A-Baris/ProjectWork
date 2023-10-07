using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ITableOfRestaurantService _tableOfRestaurantService;

        public CustomerController(ICustomerService customerService,ITableOfRestaurantService tableOfRestaurantService)
        {
            _customerService = customerService;
            _tableOfRestaurantService = tableOfRestaurantService;
        }
        public IActionResult Index()
        {
            ViewBag.TableList = _tableOfRestaurantService.GetAll();
            var customerList=_customerService.GetAll();
            return View(customerList);
        }

        public IActionResult Create()
        {
            TableSelect();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CustomerVM customerVM)
        {
            
            if(ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    Name = customerVM.Name,
                    Surname = customerVM.Surname,
                    Adress = customerVM.Adress,
                    Phone = customerVM.Phone,
                    ReservationDate = customerVM.ReservationDate,
                    ReservationDescription = customerVM.ReservationDescription,
                    TableOfRestaurantId = customerVM.TableOfRestaurantId,
                };
                _customerService.Create(customer);
                return RedirectToAction("customer", "manager", "index");
            }
            TableSelect();
            return View(customerVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            TableSelect();
            var customerEntity = await _customerService.GetbyIdAsync(id);
            if (customerEntity != null)
            {
                var updated = new CustomerVM()
                {
                    Name = customerEntity.Name,
                    Surname = customerEntity.Surname,
                    Adress = customerEntity.Adress,
                    Phone = customerEntity.Phone,
                    ReservationDate = customerEntity.ReservationDate,
                    ReservationDescription = customerEntity.ReservationDescription,
                    TableOfRestaurantId = customerEntity.TableOfRestaurantId,
                };
                return View(updated);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CustomerVM customerVM)
        {
            if(ModelState.IsValid)
            {
                var entity = await _customerService.GetbyIdAsync(customerVM.Id);
                entity.Name = customerVM.Name;
                entity.Surname = customerVM.Surname;
                entity.Adress = customerVM.Adress;
                entity.Phone = customerVM.Phone;
                entity.ReservationDate = customerVM.ReservationDate;
                entity.ReservationDescription = customerVM.ReservationDescription;
                entity.TableOfRestaurantId=customerVM.TableOfRestaurantId;
                _customerService.Update(entity);
                return RedirectToAction("customer", "manager", "index");
            }
            return View(customerVM);
        }

        public async Task<IActionResult>Remove(int id)
        {
            var entity=await _customerService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _customerService.Update(entity);
                return RedirectToAction("customer", "manager", "index");

            }
            return View();
        }
        void TableSelect()
        {
            ViewBag.TableSelect = _tableOfRestaurantService.GetAll().Where(t=>t.Status==Entity.Enums.ReservationStatus.Pasif).Select(t => new SelectListItem
            {
                Text = $"{t.TableName} ({t.TableCapacity})",
                Value = t.Id.ToString(),
            });
        }


    }
}
