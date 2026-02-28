using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Th_Dpt.Data;
using Th_Dpt.Models;
using System.Security.Cryptography;
using System.Text;

namespace Th_Dpt.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User/Create
        public async Task<IActionResult> Create()
        {
            var users = await _context.MasterUsers
                                      .OrderByDescending(x => x.Id)
                                      .ToListAsync();

            return View(users);
        }

        // POST: User/Create
        [HttpPost]
        public async Task<IActionResult> Create(MasterUser model)
        {
            //if (ModelState.IsValid)
            //{
                // Fix DateTime issue
                model.DateOfBirth = DateTime.SpecifyKind(model.DateOfBirth, DateTimeKind.Utc);
                model.CreatedDate = DateTime.UtcNow;

                _context.MasterUsers.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create");
            //}

            return View(await _context.MasterUsers.ToListAsync());
        }



        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

    }
}
