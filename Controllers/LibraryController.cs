using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stream.Models;
using Stream.Repository.Library;
using Stream.Repository.User;
using Stream.Repository.Game;
using System.Linq;
using System.Threading.Tasks;

namespace Stream.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;

        public LibraryController(ILibraryRepository libraryRepository, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _libraryRepository = libraryRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
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
            var libraries = await _libraryRepository.GetAllAsync(searchQuery);
            ViewData["SearchQuery"] = searchQuery;

            return View(libraries);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Users"] = new SelectList(await _userRepository.GetAllAsync(), "Id", "Username");
            ViewData["Games"] = new SelectList(await _gameRepository.GetAllAsync(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Library library)
        {
            if (ModelState.IsValid)
            {
                await _libraryRepository.AddAsync(library);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Users"] = new SelectList(await _userRepository.GetAllAsync(), "Id", "Username", library.UserId);
            ViewData["Games"] = new SelectList(await _gameRepository.GetAllAsync(), "Id", "Title", library.GameId);
            return View(library);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var library = await _libraryRepository.GetByIdAsync(id);
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

            await _libraryRepository.UpdateAsync(library);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var library = await _libraryRepository.GetByIdAsync(id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _libraryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}