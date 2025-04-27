using Microsoft.AspNetCore.Mvc;
using Stream.Models;
using Stream.Repository.Game;
using System.Threading.Tasks;

namespace Stream.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepository _repository;

        public GameController(IGameRepository repository)
        {
            _repository = repository;
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
            var games = await _repository.GetAllAsync(searchQuery);
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

            await _repository.AddAsync(game);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var game = await _repository.GetByIdAsync(id);
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

            await _repository.UpdateAsync(game);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var game = await _repository.GetByIdAsync(id);
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
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}