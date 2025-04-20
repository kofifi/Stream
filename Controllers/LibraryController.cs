using Microsoft.AspNetCore.Mvc;
using Stream.Models;
using Stream.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stream.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return RedirectToAction(nameof(Index));
            }

            var libraries = await _context.Libraries
                .Include(l => l.User)
                .Include(l => l.Game)
                .Where(l => 
                    (l.User != null && l.User.Username.Contains(searchQuery)) || 
                    (l.Game != null && l.Game.Title.Contains(searchQuery)) || 
                    l.Status.ToString().Contains(searchQuery))
                .ToListAsync();

            return RedirectToAction(nameof(Index), new { searchQuery = searchQuery });
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            var libraries = _context.Libraries
                .Include(l => l.User)
                .Include(l => l.Game)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();
                libraries = libraries.Where(l => 
                    (l.User != null && l.User.Username != null && l.User.Username.ToLower().Contains(lowerSearchQuery)) || 
                    (l.Game != null && l.Game.Title != null && l.Game.Title.ToLower().Contains(lowerSearchQuery)) || 
                    l.Status.ToString().ToLower().Contains(lowerSearchQuery));
            }

            ViewData["SearchQuery"] = searchQuery;

            if (IsAjaxRequest())
            {
                return PartialView("_LibraryTablePartial", await libraries.ToListAsync());
            }

            return View(await libraries.ToListAsync());
        }

        public bool IsAjaxRequest()
        {
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_context.Users, "Id", "Username");
            ViewData["Games"] = new SelectList(_context.Games, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Library library)
        {
            if (ModelState.IsValid)
            {
                _context.Add(library);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Users"] = new SelectList(_context.Users, "Id", "Username", library.UserId);
            ViewData["Games"] = new SelectList(_context.Games, "Id", "Title", library.GameId);
            return View(library);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var library = await _context.Libraries
                .Include(l => l.User)
                .Include(l => l.Game)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (library == null)
            {
                return NotFound();
            }

            ViewBag.Status = new SelectList(Enum.GetValues(typeof(Status)).Cast<Status>());
            return View(library);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Library library)
        {
            if (id != library.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Status = new SelectList(Enum.GetValues(typeof(Status)).Cast<Status>());
                return View(library);
            }

            try
            {
                var existingLibrary = await _context.Libraries.FindAsync(id);
                if (existingLibrary == null)
                {
                    return NotFound();
                }

                // Aktualizuj tylko pole Status
                existingLibrary.Status = library.Status;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Libraries.Any(l => l.Id == library.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var library = await _context.Libraries
                .Include(l => l.User)
                .Include(l => l.Game)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (library == null)
            {
                return NotFound();
            }

            return View(library); // Wyświetla widok Delete.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var library = await _context.Libraries.FindAsync(id);
            if (library != null)
            {
                _context.Libraries.Remove(library);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var library = await _context.Libraries
                .Include(l => l.User)
                .Include(l => l.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library == null)
            {
                return NotFound();
            }
            return View(library);
        }
    }
}