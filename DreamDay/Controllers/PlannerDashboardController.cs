using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace DreamDay.Controllers
{
    public class PlannerDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PlannerDashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
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

            var assignedWeddings = await _context.Weddings
                .Where(w => w.PlannerId == user.Id)
                .ToListAsync();

            return View(assignedWeddings);
        }

        // GET: PlannerDashboard/WeddingDetails/5
        public async Task<IActionResult> WeddingDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wedding = await _context.Weddings
                .FirstOrDefaultAsync(w => w.WeddingId == id);

            if (wedding == null)
            {
                return NotFound();
            }

            var checklistItems = await _context.ChecklistItems
                .Where(c => c.WeddingId == wedding.WeddingId)
                .ToListAsync();

            var guests = await _context.Guests
                .Where(g => g.WeddingId == wedding.WeddingId)
                .ToListAsync();

            var budgetItems = await _context.BudgetItems
                .Where(b => b.WeddingId == wedding.WeddingId)
                .ToListAsync();

            var model = new WeddingDetailsViewModel
            {
                Wedding = wedding,
                ChecklistItems = checklistItems,
                Guests = guests,
                BudgetItems = budgetItems
            };

            return View(model);
        }

    }
}
