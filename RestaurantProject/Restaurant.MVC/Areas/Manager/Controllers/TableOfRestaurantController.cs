using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Utility.ModelStateHelper;
using Restaurant.MVC.Utility.TempDataHelpers;
using Restaurant.MVC.Validators;

namespace Restaurant.MVC.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class TableOfRestaurantController : Controller
    {

        private readonly ITableOfRestaurantService _tableOfRestaurant;
        private readonly IEmployeeService _employee;
        private readonly IProductService _productService;
        private readonly IOrderService _orderItem;
        private readonly IMapper _mapper;
        private readonly IValidationService<TableOfRestaurantVM> _validationService;

        public TableOfRestaurantController(ITableOfRestaurantService tableOfRestaurant, IEmployeeService employee, IProductService productService, IOrderService orderItem, IMapper mapper,IValidationService<TableOfRestaurantVM> validationService)
        {


            _tableOfRestaurant = tableOfRestaurant;
            _employee = employee;
            _productService = productService;
            _orderItem = orderItem;
            _mapper = mapper;
           _validationService = validationService;
        }
        public IActionResult Index()
        {
            ViewBag.EmployeeList = _employee.GetAll();
            var tableList = _tableOfRestaurant.GetAll().OrderBy(t => t.TableName).ToList();
            //List<string> location = new List<string>();
            //foreach (var item in tableList)
            //{
            //    location.Add(item.TableLocation);
            //}
            //ViewBag.TableLocations = location;
            return View(tableList);
        }

        public IActionResult Create()
        {
            EmployeeList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(TableOfRestaurantVM tofVM)
        {
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(tofVM);
            if(errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState, errors);
                TempData.SetErrorMessage();
                EmployeeList();
                return View(tofVM);
            }
          
                var table = _mapper.Map<TableOfRestaurant>(tofVM);
                table.Status = Entity.Enums.ReservationStatus.Passive;
                _tableOfRestaurant.Create(table);
            TempData.SetSuccessMessage();
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
            
          
        }
        public async Task<IActionResult> Update(int id)
        {
            EmployeeList();


            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            if (table != null)
            {
                var updated = _mapper.Map<TableOfRestaurantVM>(table);
                return View(updated);

            }
            TempData.NotFoundId();
            return RedirectToAction("index", "tableofrestaurant", new {area="manager"});
           
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,TableOfRestaurantVM updated)
        {
            ModelState.Clear();
            var errors = _validationService.GetValidationErrors(updated);
            if(errors.Any())
            {
                ModelStateHelper.AddErrorsToModelState(ModelState,errors);
                TempData.SetErrorMessage();
                EmployeeList();
                return View(updated);
            }
            
                var table = await _tableOfRestaurant.GetbyIdAsync(id);
                var tableUpdate = _mapper.Map(updated, table);
                _tableOfRestaurant.Update(tableUpdate);
            TempData.SetSuccessMessage();
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
            
           
          

        }
        public async Task<IActionResult> Remove(int id)
        {
            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            if (table != null)
            {
                table.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _tableOfRestaurant.Update(table);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
            }
            TempData.NotFoundId();
            return RedirectToAction("index");
        }
        public IActionResult OrderList(int id)
        {
            ViewBag.Tables = _tableOfRestaurant.GetAll();
            ViewBag.Products = _productService.GetAll();
            var orderList = _orderItem.GetAll().Where(x => x.TableofRestaurantId == id).ToList();
            return View(orderList);
        }
        public async Task<IActionResult> BillRequest(int id)
        {
            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            if(table !=null)
            {
                table.BillRequest = Entity.Enums.BillRequest.Requested;
                _tableOfRestaurant.Update(table);
                TempData["Message"] = "Hesap talep edildi";
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "Id bulunamadı";
            return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
        }
        public async Task<IActionResult> BillRequestCancel(int id)
        {
            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            if (table != null)
            {
                table.BillRequest = Entity.Enums.BillRequest.Passive;
                _tableOfRestaurant.Update(table);
                TempData.SetSuccessMessage();
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
            }
            TempData.NotFoundId();
            return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
        }

        void EmployeeList()
        {
            ViewBag.Employees = _employee.GetAll().Where(x=>x.Title=="Garson").Select(w => new SelectListItem
            {
                Text = w.Name + " " + w.Surname,
                Value = w.Id.ToString()
            });
        }

    }
}
