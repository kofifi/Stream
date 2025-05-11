using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stream.Models;
using Stream.Services.Interfaces;
using Stream.ViewModels.Dto;
using Stream.ViewModels.ViewModels;

namespace Stream.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly IMapper _mapper;

        public LibraryController(ILibraryService libraryService, IMapper mapper)
        {
            _libraryService = libraryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var libraries = await _libraryService.GetAllAsync(searchQuery, pageNumber, pageSize);
            var totalLibraries = await _libraryService.GetTotalCountAsync(searchQuery);

            var viewModel = new LibraryViewModel
            {
                Libraries = _mapper.Map<IEnumerable<LibraryDto>>(libraries),
                SearchQuery = searchQuery,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalLibraries = totalLibraries ?? 0
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new LibraryViewModel
            {
                Libraries = new List<LibraryDto>(),
                SearchQuery = null,
                CurrentPage = 1,
                PageSize = 10,
                TotalLibraries = 0
            };

            ViewData["Users"] = await _libraryService.GetUsersSelectListAsync();
            ViewData["Games"] = await _libraryService.GetGamesSelectListAsync();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibraryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Users"] = await _libraryService.GetUsersSelectListAsync(viewModel.Libraries.FirstOrDefault()?.UserId);
                ViewData["Games"] = await _libraryService.GetGamesSelectListAsync(viewModel.Libraries.FirstOrDefault()?.GameId);
                return View(viewModel);
            }

            var libraryDto = viewModel.Libraries.FirstOrDefault();
            if (libraryDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var library = _mapper.Map<Stream.Models.Library>(libraryDto);
            await _libraryService.AddAsync(library);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var library = await _libraryService.GetByIdAsync(id);
            if (library == null)
            {
                return NotFound();
            }

            var libraryDto = _mapper.Map<LibraryDto>(library);

            var viewModel = new LibraryViewModel
            {
                Libraries = new List<LibraryDto> { libraryDto }
            };

            ViewBag.Status = new SelectList(Enum.GetValues(typeof(Status)).Cast<Status>(), libraryDto.Status);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LibraryViewModel viewModel)
        {
            var libraryDto = viewModel.Libraries.FirstOrDefault();
            if (libraryDto == null || id != libraryDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Status = new SelectList(Enum.GetValues(typeof(Status)).Cast<Status>(), libraryDto.Status);
                return View(viewModel);
            }

            var library = _mapper.Map<Stream.Models.Library>(libraryDto);
            await _libraryService.UpdateAsync(library);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var library = await _libraryService.GetByIdAsync(id);
            if (library == null)
            {
                return NotFound();
            }

            var libraryDto = _mapper.Map<LibraryDto>(library);

            var viewModel = new LibraryViewModel
            {
                Libraries = new List<LibraryDto> { libraryDto }
            };

            return View(viewModel);
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