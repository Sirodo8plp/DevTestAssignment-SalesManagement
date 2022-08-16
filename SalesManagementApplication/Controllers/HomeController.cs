using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using SalesManagementApplication.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SalesManagementApplication.Data;

namespace SalesManagementApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SalesManagementApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, SalesManagementApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sellersCount = _context.Seller.Count();
            decimal salesSum = 0m;
            if(_context.Sale != null)
            {
                await _context.Sale.ForEachAsync(sale => salesSum += sale.Price);
            }
            
            string sellersCountToString = sellersCount.ToString("N");
            ViewBag.sellersCount = sellersCountToString.Remove(sellersCountToString.Length - 3, 3);
            ViewBag.salesSum = salesSum.ToString("C");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}