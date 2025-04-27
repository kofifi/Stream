using Microsoft.AspNetCore.Mvc;
using Stream.Models;
using Stream.Services.Interfaces;
using System.Threading.Tasks;

namespace Stream.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
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

            return RedirectToAction(nameof(Index), new { searchQuery });
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            var games = await _gameService.GetAllAsync(searchQuery);
            ViewData["SearchQuery"] = searchQuery;

            if (IsAjaxRequest())
            {
                return PartialView("_GameTablePartial", games);
            }

            return View(games);
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

            await _gameService.AddAsync(game);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
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

            await _gameService.UpdateAsync(game);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
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
            await _gameService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}