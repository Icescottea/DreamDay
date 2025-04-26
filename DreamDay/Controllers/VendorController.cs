using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Controllers
{
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string category)
        {
            var vendorsQuery = _context.Vendors.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                vendorsQuery = vendorsQuery.Where(v => v.Category == category);
            }

            var vendors = await vendorsQuery.ToListAsync();
            var categories = await _context.Vendors
                                .Select(v => v.Category)
                                .Distinct()
                                .ToListAsync();

            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = category;
            return View(vendors);
        }


    }
}
