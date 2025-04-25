using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models;
using System.Threading.Tasks;
using System.Linq;

namespace DreamDay.Controllers
{
    public class GuestManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GuestManagementController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
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
                return View(Enumerable.Empty<Guest>());
            }

            var guests = await _context.Guests
                .Where(g => g.WeddingId == wedding.WeddingId)
                .ToListAsync();

            return View(guests);
        }

        // GET: GuestManagement/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var wedding = await _context.Weddings.FirstOrDefaultAsync(w => w.CoupleId == user.Id);

            if (wedding == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.WeddingId = wedding.WeddingId;
            return View();
        }

        // POST: GuestManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        // GET: GuestManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: GuestManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Guest guest)
        {
            if (id != guest.GuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Guests.Any(e => e.GuestId == guest.GuestId))
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
            return View(guest);
        }

        // GET: GuestManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .FirstOrDefaultAsync(m => m.GuestId == id);

            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: GuestManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
