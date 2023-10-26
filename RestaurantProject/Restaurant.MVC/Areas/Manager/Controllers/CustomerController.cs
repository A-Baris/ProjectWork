using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;


namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
     
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService,ProjectContext context,IMapper mapper)
        {
            _customerService = customerService;
     
           _context = context;
            _mapper = mapper;
        }
        //public IActionResult Test1()
        //{
        //    return View();
        //}
        //public IActionResult Test2(DateTime testdate)
        //{
           
        //    var reservationQuery = (from c in _context.Customers
        //                            join t in _context.TableOfRestaurants on c.TableOfRestaurantId equals t.Id
        //                            select new RezervationVM
        //                            {
        //                                TableOfRestaurantId = t.Id,
        //                                TableName=t.TableName,
        //                                TableLocation=t.TableLocation,
        //                                ReserveStatus=t.Status.ToString(),
                                  
        //                            }).ToList();
        //    //Girilen güne  göre rezervasyonları listeler.Müşteri aradığında istediği gündeki boş saate rezerve yapılarak çakışmalar önlenecek 
        //    var data = reservationQuery.Where(x => x.ReservationDate.DayOfWeek == testdate.DayOfWeek && x.ReserveStatus == "Passive").ToList();
        //    //var data = reservationQuery.Where(x => x.ReservationDate.DayOfWeek == testdate.DayOfWeek && x.ReservationDate.Hour == testdate.Hour).ToList();    //gün ve saat           
        //    ViewBag.ReservationDay = data;
        //    return View();
        //}
        public IActionResult Index()
        {
           
            var customerList=_customerService.GetAll();
            return View(customerList);
        }

        public IActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerVM customerVM)
        {
            
            if(ModelState.IsValid)
            {
                
             var customer =_mapper.Map<Customer>(customerVM);
                _customerService.Create(customer);
                TempData["Message"] = "Is created successfully";

                return RedirectToAction("index", "customer", new { area = "manager" });
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
         
            return View(customerVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            
            var customerEntity = await _customerService.GetbyIdAsync(id);
            if (customerEntity != null)
            {
               var updated = _mapper.Map<CustomerVM>(customerEntity);
                return View(updated);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CustomerVM customerVM)
        {
            if(ModelState.IsValid)
            {
                var customerUpdated = _mapper.Map<Customer>(customerVM);
                _customerService.Update(customerUpdated);
                TempData["Message"] = "Updated is succesful";
                return RedirectToAction("index", "customer", new {area="manager"});
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            return View(customerVM);
        }

        public async Task<IActionResult>Remove(int id)
        {
            var entity=await _customerService.GetbyIdAsync(id);
            if(entity!=null)
            {
                entity.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _customerService.Update(entity);
                TempData["Message"] = "Deleted";
                return RedirectToAction("index", "customer", new { area = "manager" });

            }
            TempData["ErrorMessage"] = "Failed";
            return View();
        }
        


    }
}
