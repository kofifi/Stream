using Microsoft.AspNetCore.Mvc;
using Stream.Models;
using Stream.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Stream.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedAt = DateTime.UtcNow;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Implementacja logowania (np. ustawienie sesji)
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Nieprawidłowy adres e-mail lub hasło.");
            }
            return View(model);
        }
    }
}