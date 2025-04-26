using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DreamDay.Controllers
{
    public class TimelineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TimelineController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var wedding = await _context.Weddings.FirstOrDefaultAsync(w => w.CoupleId == user.Id);

            if (wedding == null) return RedirectToAction("Index", "Home");

            var events = await _context.TimelineEvents
                .Where(e => e.WeddingId == wedding.WeddingId)
                .OrderBy(e => e.EventTime)
                .ToListAsync();

            ViewBag.WeddingId = wedding.WeddingId;
            return View(events);
        }

        public IActionResult Create(int weddingId)
        {
            ViewBag.WeddingId = weddingId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimelineEvent timelineEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timelineEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timelineEvent);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var timelineEvent = await _context.TimelineEvents.FindAsync(id);
            if (timelineEvent == null) return NotFound();

            return View(timelineEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TimelineEvent timelineEvent)
        {
            if (id != timelineEvent.EventId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(timelineEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timelineEvent);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var timelineEvent = await _context.TimelineEvents.FindAsync(id);
            if (timelineEvent == null) return NotFound();

            return View(timelineEvent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timelineEvent = await _context.TimelineEvents.FindAsync(id);
            if (timelineEvent != null)
            {
                _context.TimelineEvents.Remove(timelineEvent);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
