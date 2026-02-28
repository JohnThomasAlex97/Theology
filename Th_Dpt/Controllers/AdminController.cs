using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Th_Dpt.Data;
using Th_Dpt.Models;

namespace Th_Dpt.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Class()
        //{
        //    var users = await _context.Class
        //                              .OrderByDescending(x => x.Id)
        //                              .ToListAsync();

        //    return View(users);
        //}
        public IActionResult Evangelism()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetEvangelismData()
        {
            try
            {
                var data = await _context.Students_Evang
                .OrderByDescending(x => x.id)
                .Select(x => new
                {
                    x.id,
                    x.ClassName,
                    x.Instructor,
                    x.CenterStudentName,
                    x.ProspectName,
                    x.PhoneNumber,
                    x.City
                })
                .ToListAsync();
                return Json(data);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEvangelism(Evangelism model)
        {
            if (ModelState.IsValid)
            {
                _context.Students_Evang.Add(model);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> GetClassList()
        {
            var data = await _context.Class
                .Select(x => new
                {
                    x.id,
                    x.ClassName,
                    x.Instructor,
                })
                .ToListAsync();
            return Json(data);
        }
    }
}
