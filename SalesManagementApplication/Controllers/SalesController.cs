using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesManagementApplication.Data;
using SalesManagementApplication.Models;

namespace SalesManagementApplication.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesManagementApplicationContext _context;

        public SalesController(SalesManagementApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sale.
                Include(s => s.Seller).
                OrderByDescending(sale => sale.CreatedDate).
                ToListAsync();
            return View(sales);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sale == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        public IActionResult Create()
        {
            ViewData["SellerId"] = new SelectList(_context.Seller, "SellerId", "SellerId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleId,Price,CreatedDate,SellerId")] Sale sale)
        {
            Console.Clear();
            Console.WriteLine($"SALE PRICE: {sale.Price}");
            var seller = _context.Seller.Where(sel => sel.SellerId == sale.SellerId).First();
            sale.Seller = seller;
            ModelState.ClearValidationState(nameof(seller));
            if (TryValidateModel(seller,nameof(seller)))
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SellerId"] = new SelectList(_context.Seller, "SellerId", "SellerId", sale.SellerId);
            return View(sale);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sale == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["SellerId"] = new SelectList(_context.Seller, "SellerId", "SellerId", sale.SellerId);
            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,Price,CreatedDate,SellerId")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }
            Console.Clear();
            Console.WriteLine($"SALE PRICE {sale.Price}");
            var seller = await _context.Seller.Where(seller => seller.SellerId == sale.SellerId).FirstOrDefaultAsync();
            sale.Seller = seller!;
            ModelState.ClearValidationState(nameof(seller));

            if (TryValidateModel(seller!, nameof(seller)))
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SaleId))
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
            ViewData["SellerId"] = new SelectList(_context.Seller, "SellerId", "LastName", sale.SellerId);
            return View(sale);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sale == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sale == null)
            {
                return Problem("Entity set 'SalesManagementApplicationContext.Sale'  is null.");
            }
            var sale = await _context.Sale.FindAsync(id);
            if (sale != null)
            {
                _context.Sale.Remove(sale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
          return (_context.Sale?.Any(e => e.SaleId == id)).GetValueOrDefault();
        }
    }
}
