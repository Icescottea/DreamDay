using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models;
using System.Threading.Tasks;
using System.Linq;

namespace DreamDay.Controllers
{
    public class ChecklistManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChecklistManagementController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var wedding = await _context.Weddings
                .FirstOrDefaultAsync(w => w.CoupleId == user.Id);

            if (wedding == null)
            {
                return View(Enumerable.Empty<ChecklistItem>());
            }

            var checklistItems = await _context.ChecklistItems
                .Include(c => c.Vendor)
                .Where(c => c.WeddingId == wedding.WeddingId)
                .ToListAsync();

            return View("~/Views/ChecklistManagement/Index.cshtml", checklistItems);
        }

        public async Task<IActionResult> Create(int? weddingId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            { return RedirectToAction("Login", "Account"); }

            Wedding wedding = null;

            if (weddingId.HasValue)
            {
                wedding = await _context.Weddings.FirstOrDefaultAsync(w => w.WeddingId == weddingId.Value);
            }
            else
            {
                wedding = await _context.Weddings.FirstOrDefaultAsync(w => w.CoupleId == user.Id);
            }

            if (wedding == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.WeddingId = wedding.WeddingId;

            ViewBag.VendorCategories = _context.Vendors
                .Select(v => v.Category)
                .Distinct()
                .ToList();

            return View();
        }



        // POST: ChecklistManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChecklistItem checklistItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checklistItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate Vendor Categories for dropdown
            ViewBag.VendorCategories = _context.Vendors
                .Select(v => v.Category)
                .Distinct()
                .ToList();

            // Extra: If VendorCategory already selected, you can reload vendors matching that
            if (!string.IsNullOrEmpty(checklistItem.VendorCategory))
            {
                ViewBag.InitialVendors = await _context.Vendors
                    .Where(v => v.Category == checklistItem.VendorCategory)
                    .ToListAsync();
            }
            else
            {
                ViewBag.InitialVendors = new List<Vendor>();
            }

            return View(checklistItem);
        }



        // GET: ChecklistManagement/MarkComplete/5
        public async Task<IActionResult> MarkComplete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ChecklistItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.IsCompleted = true;
            _context.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ChecklistManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ChecklistItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            ViewBag.VendorCategories = await _context.Vendors
                .Select(v => v.Category)
                .Distinct()
                .ToListAsync();

            ViewBag.InitialVendors = await _context.Vendors
                .Where(v => v.Category == item.VendorCategory)
                .ToListAsync();

            return View(item);
        }


        // POST: ChecklistManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChecklistItem item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ChecklistItems.Any(e => e.ItemId == item.ItemId))
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
            return View(item);
        }

        // GET: ChecklistManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ChecklistItems.FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: ChecklistManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.ChecklistItems.FindAsync(id);
            _context.ChecklistItems.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetVendorsByCategory(string category)
        {
            var vendors = await _context.Vendors
                .Where(v => v.Category == category)
                .Select(v => new { v.VendorId, v.Name })
                .ToListAsync();

            return Json(vendors);
        }

    }
}
