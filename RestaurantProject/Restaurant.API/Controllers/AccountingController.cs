using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DAL.Context;

namespace Restaurant.API.Controllers
{
    //[Route("api/[accounting]")]
    //[ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly ProjectContext _context;

        public AccountingController(ProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetDebits()
        {
            var query = from a in _context.AccountingTransactions
                        join s in _context.Suppliers on a.SupplierId equals s.Id
                        group a by s.CompanyName into g
                        select new
                        {
                            CompanyName = g.Key,
                            Kalanborc = g.Sum(a => a.RemainingDebt)

                        };
            var debit = query.ToList();
            return Ok(debit);
        }
    }
}
