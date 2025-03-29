using Microsoft.AspNetCore.Mvc;
using Stream.Models;
using Stream.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Stream.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return RedirectToAction(nameof(Index));
            }

            var games = await _context.Games
                .Where(g => g.Title.Contains(searchQuery) || g.Genre.Contains(searchQuery))
                .ToListAsync();
            return RedirectToAction(nameof(Index), new { searchQuery = searchQuery });
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            var games = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();
                games = games.Where(g => 
                    (g.Title != null && g.Title.ToLower().Contains(lowerSearchQuery)) || 
                    (g.Genre != null && g.Genre.ToLower().Contains(lowerSearchQuery))
                );
            }

            ViewData["SearchQuery"] = searchQuery;

            if (IsAjaxRequest())
            {
                return PartialView("_GameTablePartial", await games.ToListAsync());
            }

            return View(await games.ToListAsync());
        }

        public bool IsAjaxRequest()
        {
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public IActionResult ResetSearch()
        {
            return RedirectToAction(nameof(Index), new { searchQuery = string.Empty });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Game game)
        {
            if (!ModelState.IsValid)
            {
                return View(game);
            }

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(game);
            }

            try
            {
                _context.Update(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Games.Any(g => g.Id == game.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
