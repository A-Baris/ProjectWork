using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Services;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;


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

        public TableOfRestaurantController(ITableOfRestaurantService tableOfRestaurant, IEmployeeService employee, IProductService productService, IOrderService orderItem, IMapper mapper)
        {


            _tableOfRestaurant = tableOfRestaurant;
            _employee = employee;
            _productService = productService;
            _orderItem = orderItem;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.EmployeeList = _employee.GetAll();
            var tableList = _tableOfRestaurant.GetAll();
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
            if (ModelState.IsValid)
            {
                var table = _mapper.Map<TableOfRestaurant>(tofVM);
                _tableOfRestaurant.Create(table);
                TempData["Message"] = "Başarılı şekilde oluşturuldu";
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
            }
            TempData["ErrorMessage"] = "ModelState is invalid";
            EmployeeList();
            return View(tofVM);
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
            TempData["ErrorMessage"] = "Id bulunamadı";
            return RedirectToAction("index", "tableofrestaurant", new {area="manager"});
           
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,TableOfRestaurantVM updated)
        {
            if (ModelState.IsValid)
            {
                var table = await _tableOfRestaurant.GetbyIdAsync(id);
                var tableUpdate = _mapper.Map(updated, table);
                _tableOfRestaurant.Update(tableUpdate);
                TempData["Message"] = "Güncelleme başarılı";
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
            }
            EmployeeList();
            TempData["ErrorMessage"] = "Modelstate is invalid";
            return View(updated);

        }
        public async Task<IActionResult> Remove(int id)
        {
            var table = await _tableOfRestaurant.GetbyIdAsync(id);
            if (table != null)
            {
                table.BaseStatus = Entity.Enums.BaseStatus.Deleted;
                _tableOfRestaurant.Update(table);
                TempData["Message"] = "Successful";
                return RedirectToAction("index", "tableofrestaurant", new { area = "Manager" });
            }
            return View();
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
      
        void EmployeeList()
        {
            ViewBag.Employees = _employee.GetAll().Select(w => new SelectListItem
            {
                Text = w.Name + " " + w.Surname,
                Value = w.Id.ToString()
            });
        }

    }
}
