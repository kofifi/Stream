using Microsoft.AspNetCore.Mvc;
using Stream.Models;
using Stream.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Stream.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            var users = _context.Users.AsQueryable();

            // If a search query is provided, filter the users (case-insensitive search)
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();

                // Perform the search in a way that SQL can handle
                users = users.Where(u => 
                    (u.Username != null && u.Username.ToLower().Contains(lowerSearchQuery)) || 
                    (u.Email != null && u.Email.ToLower().Contains(lowerSearchQuery))
                );
            }

            // Execute the query and return the filtered list to the view
            ViewData["SearchQuery"] = searchQuery;

            // If the request is an AJAX request, return only the table content
            if (IsAjaxRequest())
            {
                return PartialView("_UserTablePartial", await users.ToListAsync());
            }

            // Otherwise, return the full view
            return View(await users.ToListAsync());
        }

        public bool IsAjaxRequest()
        {
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }


        public IActionResult ResetSearch()
        {
            // Simply redirect back to the Index view with no search query (resetting the search)
            return RedirectToAction(nameof(Index), new { searchQuery = string.Empty });
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.Id == user.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // Search GET method
        public IActionResult Search()
        {
            return View();
        }

        // Search POST method
        [HttpPost]
        public async Task<IActionResult> Search(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return RedirectToAction(nameof(Index)); // If no query, just return all users
            }

            var users = await _context.Users
                .Where(u => u.Username.Contains(searchQuery) || u.Email.Contains(searchQuery))
                .ToListAsync();
            return RedirectToAction(nameof(Index), new { searchQuery = searchQuery }); // Pass search query to Index view
        }
    }
}
