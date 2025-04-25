using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DreamDay.Data;
using DreamDay.Models;
using DreamDay.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DreamDay.Controllers
{
    public class CoupleDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CoupleDashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
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
                return View(new CoupleDashboardViewModel()); // Empty model
            }

            var totalTasks = await _context.ChecklistItems.CountAsync(c => c.WeddingId == wedding.WeddingId);
            var completedTasks = await _context.ChecklistItems.CountAsync(c => c.WeddingId == wedding.WeddingId && c.IsCompleted);
            var totalGuests = await _context.Guests.CountAsync(g => g.WeddingId == wedding.WeddingId);
            var acceptedGuests = await _context.Guests.CountAsync(g => g.WeddingId == wedding.WeddingId && g.RSVPStatus == "Accepted");
            var estimatedBudget = await _context.BudgetItems.Where(b => b.WeddingId == wedding.WeddingId).SumAsync(b => (decimal?)b.EstimatedCost) ?? 0;
            var actualBudget = await _context.BudgetItems.Where(b => b.WeddingId == wedding.WeddingId).SumAsync(b => (decimal?)b.ActualCost) ?? 0;

            var model = new CoupleDashboardViewModel
            {
                Wedding = wedding,
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                TotalGuests = totalGuests,
                AcceptedGuests = acceptedGuests,
                EstimatedBudget = estimatedBudget,
                ActualBudget = actualBudget
            };

            return View(model);
        }
    }
}
