using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stream.Models;
using Stream.Services.Interfaces;
using System.Threading.Tasks;

namespace Stream.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
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
            var libraries = await _libraryService.GetAllAsync(searchQuery);
            ViewData["SearchQuery"] = searchQuery;

            return View(libraries);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Users"] = await _libraryService.GetUsersSelectListAsync();
            ViewData["Games"] = await _libraryService.GetGamesSelectListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Library library)
        {
            if (ModelState.IsValid)
            {
                await _libraryService.AddAsync(library);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Users"] = await _libraryService.GetUsersSelectListAsync(library.UserId);
            ViewData["Games"] = await _libraryService.GetGamesSelectListAsync(library.GameId);
            return View(library);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var library = await _libraryService.GetByIdAsync(id);
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

            await _libraryService.UpdateAsync(library);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var library = await _libraryService.GetByIdAsync(id);
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
            await _libraryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}