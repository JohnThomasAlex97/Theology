using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Th_Dpt.Data;

namespace Th_Dpt.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        //public async Task<IActionResult> Class()
        //{
        //    var classes = await _context.Class
        //                              .OrderByDescending(x => x.Id)
        //                              .ToListAsync();

        //    return View(classes);
        //}

    }
}
