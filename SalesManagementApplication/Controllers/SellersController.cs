using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalesManagementApplication.Data;
using SalesManagementApplication.Models;

namespace SalesManagementApplication.Controllers
{
    public class SellersController : Controller
    {
        private readonly SalesManagementApplicationContext _context;

        public SellersController(SalesManagementApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Seller != null ? 
                          View(await _context.Seller.Include(seller => seller.Sales).ToListAsync()) :
                          Problem("Entity set 'SalesManagementApplicationContext.Seller'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller
                .Include(seller => seller.Sales)
                .FirstOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        public async Task<ContentResult> MonthlySales(int? id)
        {
            var salesByMonth = await _context.Sale!.
                Where(sale => sale.SellerId == id).
                Where(sale => sale.CreatedDate.Year == DateTime.Now.Year).
                GroupBy(sale => sale.CreatedDate.Month).
                Select(saleGroup => new
                {
                    Month = saleGroup.Key,
                    TotalSales = saleGroup.Sum(s => s.Price)
                }).ToListAsync();

            var salesWithCommissions = salesByMonth.Select(sales => new
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(sales.Month),
                TotalSales = sales.TotalSales,
                Commission = sales.TotalSales * 0.10m
            });
            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(salesWithCommissions, _jsonSetting), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerId,Name,LastName")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SellerId,Name,LastName")] Seller seller)
        {
            if (id != seller.SellerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.SellerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller
                .FirstOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seller == null)
            {
                return Problem("Entity set 'SalesManagementApplicationContext.Seller'  is null.");
            }
            var seller = await _context.Seller.FindAsync(id);
            if (seller != null)
            {
                _context.Seller.Remove(seller);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
          return (_context.Seller?.Any(e => e.SellerId == id)).GetValueOrDefault();
        }
    }
}
