using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Planner")]
    public class PlannerReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlannerReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Popular Venues
            var popularVenues = await _context.Weddings
                .GroupBy(w => w.Venue)
                .Where(g => g.Key != null)
                .Select(g => new
                {
                    Venue = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .ToListAsync();

            // Vendor Assignments
            var vendorAssignments = await _context.VendorAssignments
                .Include(va => va.Vendor)
                .GroupBy(va => va.Vendor.Name)
                .Select(g => new
                {
                    VendorName = g.Key,
                    Assignments = g.Count()
                })
                .OrderByDescending(g => g.Assignments)
                .ToListAsync();

            ViewBag.PopularVenues = popularVenues;
            ViewBag.VendorAssignments = vendorAssignments;

            return View();
        }
    }
}
